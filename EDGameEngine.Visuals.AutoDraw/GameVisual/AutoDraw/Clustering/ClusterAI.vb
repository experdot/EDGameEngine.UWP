Imports System.Numerics
Imports Windows.UI
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
        Debug.WriteLine(pixels.Colors.Length)
        For i = maxRank - 1 To 0 Step -1
            If Hierarchies(i).Clusters.Count > 0 Then
                Lines.AddRange(GenerateLines(Hierarchies(i)))
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

    Private Function GenerateLines(hierarchy As IHierarchy) As List(Of ILine)
        Dim result As New List(Of ILine)
        'hierarchy.Clusters.Sort(New ClusterLengthCompare)
        For Each SubCluster In hierarchy.Clusters
            GenerateLines(result, SubCluster, hierarchy.Rank)
        Next
        Return result
    End Function

    Private Sub GenerateLines(lines As List(Of ILine), parent As Cluster, depth As Integer)
        'Debug.WriteLine($"{depth},{parent.Children.Count}")
        If parent.Children.Count > 0 Then
            'parent.Children.Sort(New ClusterLengthCompare)
            For Each SubCluster In parent.Children
                Dim line As New Line
                For Each SubLeaf In SubCluster.Leaves
                    Dim c As Color = SubCluster.Color
                    Dim p As Color = c 'Color.FromArgb(CByte(c.A / (depth * depth + 1.0F)), c.R, c.G, c.B)
                    line.Points.Add(New VertexWithLayer With {.Color = SubCluster.Color, .Position = SubLeaf.Position, .Size = depth * 0F + 1.0F, .LayerIndex = MaxRank - depth})
                    line.Points.Last.UserColor = line.Points.Last.Color
                    line.Points.Last.UserSize = line.Points.Last.Size
                Next
                'line.CalcLength(2 * depth + 1)
                line.CalcLength(1)
                lines.Add(line)
                GenerateLines(lines, SubCluster, depth - 1)
            Next
        Else
            Dim line As New Line
            For Each SubLeaf In parent.Leaves
                Dim c As Color = parent.Color
                Dim p As Color = c
                line.Points.Add(New VertexWithLayer With {.Color = parent.Color, .Position = SubLeaf.Position, .Size = depth + 1.0F, .LayerIndex = MaxRank - depth})
                line.Points.Last.UserColor = line.Points.Last.Color
                line.Points.Last.UserSize = line.Points.Last.Size
            Next
            line.CalcLength(3 * depth + 1)
            lines.Add(line)
        End If
    End Sub

End Class
