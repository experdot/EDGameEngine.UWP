Imports System.Numerics
''' <summary>
''' 由组表示的层
''' </summary>
Public Class GroupHierarchy
    Inherits HierarchyBase
    ''' <summary>
    ''' 最大邻居数量
    ''' </summary>
    Public Property MaxNeighboursCount As Integer = 8

    ''' <summary>
    ''' 由指定的<see cref="PixelData"/>对象创建一个实例
    ''' </summary>
    Public Shared Function CreateFromPixels(pixels As PixelData) As GroupHierarchy
        Dim result As New GroupHierarchy()
        For i = 0 To pixels.Width - 1
            For j = 0 To pixels.Height - 1
                Dim cluster As New Cluster With
                {
                    .Position = New Vector2(i, j),
                    .Color = pixels.Colors(pixels.Width * j + i)
                }
                result.Clusters.Add(cluster)
                'result.AddCluster(cluster)'该方法存在性能问题
            Next
        Next
        Return result
    End Function

    Public Overrides Function Generate() As IHierarchy
        Dim result As New GroupHierarchy With {.Rank = Me.Rank + 1}
        For Each cluster In Clusters
            Dim similar As Cluster = cluster.GetMostSimilarCluster(GetNeighbours(cluster)).First
            If similar IsNot Nothing Then
                result.AddCluster(Cluster.Combine(cluster, similar))
            End If
            Debug.WriteLine($"{CSng((Clusters.IndexOf(cluster) + 1) / Clusters.Count)}")
        Next
        Return result
    End Function

    ''' <summary>
    ''' 返回当前层中与指定簇相邻的簇集
    ''' </summary>
    Private Function GetNeighbours(cluster As Cluster) As List(Of Cluster)
        Dim result As New List(Of Cluster)
        Dim array As New List(Of Cluster)
        array.AddRange(Clusters)
        array.Sort(New ClusterPositionCompare(cluster))
        result.AddRange(array.Take(MaxNeighboursCount))
        Return result
    End Function
End Class
