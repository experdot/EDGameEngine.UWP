Imports EDGameEngine.Core.Utilities
Imports EDGameEngine.Visuals.AutoDraw
Imports Windows.UI
''' <summary>
''' 基于深度优先搜索的转换器
''' </summary>
Public Class LineDFSConverter
    Implements IHierarchyToLinesConverter

    Public Property MaxRank As Integer

    Public Sub New(maxRank As Integer)
        Me.MaxRank = maxRank
    End Sub

    Public Function Convert(hierarchy As IHierarchy) As List(Of ILine) Implements IHierarchyToLinesConverter.Convert
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
End Class
