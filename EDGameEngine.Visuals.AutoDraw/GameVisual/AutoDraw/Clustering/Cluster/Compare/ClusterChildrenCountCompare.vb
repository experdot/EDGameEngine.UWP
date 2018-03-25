Imports EDGameEngine.Visuals
''' <summary>
''' 簇的子簇数量比较器
''' </summary>
Public Class ClusterChildrenCountCompare
    Implements IComparer(Of Cluster)

    Public Function Compare(x As Cluster, y As Cluster) As Integer Implements IComparer(Of Cluster).Compare
        If x.Children.Count > y.Children.Count Then
            Return -1
        Else
            Return 1
        End If
    End Function
End Class
