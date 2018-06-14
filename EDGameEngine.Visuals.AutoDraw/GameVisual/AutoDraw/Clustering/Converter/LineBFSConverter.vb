Imports EDGameEngine.Visuals.AutoDraw
Imports Windows.UI
''' <summary>
''' 基于广度优先搜索的线条转换器
''' </summary>
Public Class LineBFSConverter
    Implements IHierarchyToLinesConverter

    Public Property MaxRank As Integer

    Public Sub New(maxRank As Integer)
        Me.MaxRank = maxRank
    End Sub

    Public Function Convert(hierarchy As IHierarchy) As List(Of ILine) Implements IHierarchyToLinesConverter.Convert
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
                LineHelper.Resize(line, CInt(1.2 * depth) + 1)
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
