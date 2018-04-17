Imports System.Numerics
''' <summary>
''' 聚类公共模块类
''' </summary>
Public Class ClusterUtilities
    ''' <summary>
    ''' 返回聚类最小凸包
    ''' </summary>
    ''' <param name="cluster"></param>
    Public Shared Sub GetConvexHullOfCluster(cluster As Cluster)
        Dim positions = cluster.Leaves.Select(Of Vector2)(Function(c) c.Position).ToList()

    End Sub

    Public Shared Function CombinedAllCluster(clusters As IEnumerable(Of Cluster)) As Cluster
        Dim first = clusters.First
        For Each cluster In clusters
            Cluster.Combine(first, cluster)
        Next
        Return first.Parent
    End Function
End Class
