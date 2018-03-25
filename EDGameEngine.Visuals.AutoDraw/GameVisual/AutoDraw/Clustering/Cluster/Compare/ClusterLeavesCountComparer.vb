Imports EDGameEngine.Visuals
''' <summary>
''' 簇叶子数量比较器
''' </summary>
Public Class ClusterLeavesCountComparer
    Implements IComparer(Of Cluster)

    Public Function Compare(x As Cluster, y As Cluster) As Integer Implements IComparer(Of Cluster).Compare
        If x.Leaves.Count > y.Leaves.Count Then
            Return -1
        Else
            Return 1
        End If
    End Function
End Class