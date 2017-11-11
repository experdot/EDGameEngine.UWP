Imports EDGameEngine.Visuals
''' <summary>
''' 簇长度比较器
''' </summary>
Public Class ClusterLengthCompare
    Implements IComparer(Of Cluster)

    Public Function Compare(x As Cluster, y As Cluster) As Integer Implements IComparer(Of Cluster).Compare
        If x.Children.Count > y.Children.Count Then
            Return -1
        Else
            Return 1
        End If
    End Function
End Class
