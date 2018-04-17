Imports EDGameEngine.Core.Utilities
''' <summary>
''' 颜色比较器
''' </summary>
Public Class ClusterColorComparer
    Implements IComparer(Of Cluster)
    Public Property Target As Cluster
    Public Sub New(target As Cluster)
        Me.Target = target
    End Sub
    Public Function Compare(x As Cluster, y As Cluster) As Integer Implements IComparer(Of Cluster).Compare
        If ColorUtilities.GetColorSimilarity(Target.Color, x.Color) < ColorUtilities.GetColorSimilarity(Target.Color, y.Color) Then
            Return 1
        Else
            Return -1
        End If
    End Function
End Class
