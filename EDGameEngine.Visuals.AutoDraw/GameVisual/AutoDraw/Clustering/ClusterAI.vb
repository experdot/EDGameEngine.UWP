Imports System.Numerics
Imports Windows.UI
Imports EDGameEngine.Core.Utilities
''' <summary>
''' 聚类AI
''' </summary>
Public Class ClusterAI
    ''' <summary>
    ''' 线条集
    ''' </summary>
    Public Property Lines As New List(Of ILine)
    ''' <summary>
    ''' 层集
    ''' </summary>
    Public Property Hierarchies As New List(Of IHierarchy)
    ''' <summary>
    ''' 是否结束
    ''' </summary>
    Public Property IsOver As Boolean
    ''' <summary>
    ''' 最大层数
    ''' </summary>
    Public Property MaxRank As Integer

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(pixels As PixelData, Optional maxRank As Integer = 10)
        Me.MaxRank = maxRank
        Dim start As DateTime = DateTime.Now
        Debug.WriteLine("Initialize Start")
        Hierarchies.Add(GridHierarchy.CreateFromPixels(pixels))
        Debug.WriteLine($"Initialize Over,TimeConsuming:{DateTime.Now - start}")
        Debug.WriteLine($"Generation Start")
        For i = 0 To maxRank - 1
            start = DateTime.Now
            Hierarchies.Add(Hierarchies.Last.Generate())
            Debug.WriteLine($"Total:{maxRank},Current:{i + 1},Time Consuming:{DateTime.Now - start},Hierarchy[{Hierarchies.Last.ToString()}]")
        Next
        Debug.WriteLine($"Pixels:{pixels.Colors.Length}")

        '快速涂抹
        Dim current As Integer = maxRank - 1
        Dim index As Integer = maxRank - 3
        For i = maxRank - 1 To index Step -1
            If Hierarchies(i).Clusters.Count > 0 Then
                Lines.AddRange(GenerateLinesFast(Hierarchies(i)))
            End If
            current = i
        Next

        '迭代细节
        For i = current To 0 Step -1
            If Hierarchies(i).Clusters.Count > 0 Then
                Lines.AddRange(GenerateLinesQuality(Hierarchies(i)))
                Exit For
            End If
        Next
    End Sub

    Public Function NextPoint() As VertexWithLayer
        Static LineIndex As Integer = 0
        Static PointIndex As Integer = -1
        If LineIndex < Lines.Count Then
            For i = LineIndex To Lines.Count - 1
                PointIndex += 1
                If PointIndex < Lines(i).Points.Count Then
                    Return Lines(i).Points(PointIndex)
                Else
                    PointIndex = -1
                    LineIndex += 1
                End If
            Next
        End If
        IsOver = True
        Return Nothing
    End Function

    Private Function GenerateLinesFast(hierarchy As IHierarchy) As List(Of ILine)
        Dim result As New List(Of ILine)
        Dim count As Integer
        hierarchy.Clusters.Sort(New ClusterLeavesCountComparer)
        For Each cluster In hierarchy.Clusters
            Dim line As New Line
            Dim ranc As Color = ColorUtilities.GetRandomRGB()
            For Each leaf In cluster.Leaves
                Dim raw As Color = cluster.Color
                Dim real As Color = Color.FromArgb(CByte(raw.A / (hierarchy.Rank * 1.6 + 1.0F)), raw.R, raw.G, raw.B)
                line.Points.Add(New VertexWithLayer With {
                    .Color = cluster.Color,
                    .Position = leaf.Position,
                    .Size = CSng(1 + 3.0F * hierarchy.Rank),
                    .LayerIndex = MaxRank - hierarchy.Rank
                })
                line.Points.Last.UserColor = real
                'line.Points.Last.UserSize = line.Points.Last.Size
                'line.Points.Last.UserColor = ranc
                line.Points.Last.UserSize = 1.0F
            Next
            line.CalcLength(hierarchy.Rank)
            result.Add(line)
            count += line.Points.Count
        Next
        Debug.WriteLine($"Count:{count}")
        Return result
    End Function

    Private Function GenerateLinesQuality(hierarchy As IHierarchy) As List(Of ILine)
        Dim result As New List(Of ILine)
        hierarchy.Clusters.Sort(New ClusterChildrenCountCompare)
        Debug.WriteLine($"GenerateLinesQuality hierarchy count:{hierarchy.Clusters.Count}")
        Dim cluster As Cluster = ClusterUtilities.CombinedAllCluster(hierarchy.Clusters)
        GenerateByBreadthFirstSearch(result, cluster, hierarchy.Rank + 1)
        Return result
    End Function

    Private Sub GenerateByBreadthFirstSearch(lines As List(Of ILine), root As Cluster, depth As Integer)
        Dim current As Integer = depth
        Dim clusterQueue As New Queue(Of Cluster)
        Dim clusterCache As New List(Of Cluster)
        clusterQueue.Enqueue(root)
        While clusterQueue.Count > 0
            While clusterQueue.Count > 0
                Dim cluster = clusterQueue.Dequeue()
                If cluster.Children.Count = 0 Then
                    Return
                End If
                clusterCache.Add(cluster)
            End While
            current -= 1
            For Each cluster In clusterCache
                For Each child In cluster.Children
                    clusterQueue.Enqueue(child)
                    GenerateByClusterAndDepth(lines, child, current)
                Next
            Next
            clusterCache.Clear()
        End While
    End Sub

    Private Sub GenerateByClusterAndDepth(lines As List(Of ILine), parent As Cluster, depth As Integer)
        If parent.Children.Count > 0 Then
            parent.Children.Sort(New ClusterLeavesCountComparer)
            For Each cluster In parent.Children
                Dim line As New Line
                For Each leaf In cluster.Leaves
                    Dim distance As Single = (leaf.Position - cluster.Position).Length
                    Dim ratio = 1 'Math.Log10(10 + distance)
                    Dim raw As Color = cluster.Color
                    Dim real As Color = Color.FromArgb(CByte(raw.A / (depth + 1.0F)), raw.R, raw.G, raw.B)

                    line.Points.Add(New VertexWithLayer With {
                        .Color = cluster.Color,
                        .Position = leaf.Position,
                        .Size = CSng(1 + 2.0F * depth / ratio),
                        .LayerIndex = MaxRank - depth
                    })
                    line.Points.Last.UserColor = real
                    line.Points.Last.UserSize = line.Points.Last.Size
                Next
                line.CalcLength(CInt(1.2 * depth) + 1)
                lines.Add(line)
            Next
        Else
            '绘制最后一层的原始像素
            Dim line As New Line
            For Each leaf In parent.Leaves
                line.Points.Add(New VertexWithLayer With {.Color = parent.Color, .Position = leaf.Position, .Size = depth + 1.0F, .LayerIndex = MaxRank - depth})
                line.Points.Last.UserColor = line.Points.Last.Color
                line.Points.Last.UserSize = line.Points.Last.Size
            Next
            lines.Add(line)
        End If
    End Sub

End Class
