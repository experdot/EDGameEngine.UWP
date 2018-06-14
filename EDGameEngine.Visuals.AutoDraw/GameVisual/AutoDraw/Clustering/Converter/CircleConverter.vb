Imports EDGameEngine.Visuals.AutoDraw

Public Class CircleConverter
    Implements IHierarchyToLinesConverter

    Public Property MaxRank As Integer

    Public Sub New(maxRank As Integer)
        Me.MaxRank = maxRank
    End Sub

    Public Function Convert(hierarchy As IHierarchy) As List(Of ILine) Implements IHierarchyToLinesConverter.Convert
        Dim result As New List(Of ILine)
        Dim line As New Line
        'hierarchy.Clusters.Sort(New ClusterLeavesCountComparer) '集合数量过大时存在严重性能问题
        For Each cluster In hierarchy.Clusters
            line.Points.Add(New VertexWithLayer With {
                .Color = cluster.Color,
                .Position = cluster.Position,
                .Size = CSng(Math.Sqrt(cluster.Leaves.Count / Math.PI)),
                .LayerIndex = MaxRank - hierarchy.Rank
            })
            line.Points.Last.UserSize = line.Points.Last.Size
            Dim c = line.Points.Last.Color
            c.A = CByte(c.A / (hierarchy.Rank / 5 + 1.0F))
            line.Points.Last.UserColor = c
        Next
        result.Add(line)
        Return result
    End Function
End Class
