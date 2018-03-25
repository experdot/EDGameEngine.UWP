Imports System.Numerics
Imports Windows.UI
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Utilities
''' <summary>
''' 簇
''' </summary>
Public Class Cluster
    ''' <summary>
    ''' 父簇
    ''' </summary>
    Public Property Parent As Cluster
    ''' <summary>
    ''' 子簇
    ''' </summary>
    Public Property Children As New List(Of Cluster)
    ''' <summary>
    ''' 位置
    ''' </summary>
    Public Property Position As Vector2
    ''' <summary>
    ''' 颜色
    ''' </summary>
    Public Property Color As Color
    ''' <summary>
    ''' 叶子子簇
    ''' </summary>
    Public ReadOnly Property Leaves As List(Of Cluster)
        Get
            If Children.Count = 0 Then
                Return New List(Of Cluster) From {Me}
            Else
                Return GetLeavesOfChildren()
            End If
        End Get
    End Property

    ''' <summary>
    ''' 合并簇
    ''' </summary>
    Public Shared Function Combine(cluster1 As Cluster, cluster2 As Cluster) As Cluster
        Dim result As Cluster
        If cluster1.Parent IsNot Nothing Then
            result = cluster1.Parent
        ElseIf cluster2.Parent IsNot Nothing Then
            result = cluster2.Parent
        Else
            result = New Cluster
        End If

        result.AddChild(cluster1, False)
        result.AddChild(cluster2, False)

        Return result
    End Function
    ''' <summary>
    ''' 添加子簇
    ''' </summary>
    Public Sub AddChild(child As Cluster, Optional repeat As Boolean = False)
        If repeat OrElse Not Children.Contains(child) Then
            Children.Add(child)
            child.Parent = Me
        End If
    End Sub
    ''' <summary>
    ''' 返回指定集合中与当前簇最相似的簇集
    ''' </summary>
    Public Function GetMostSimilarCluster(clusters As List(Of Cluster)) As Queue(Of Cluster)
        Dim result As New Queue(Of Cluster)
        Dim maxValue As Single = Single.MinValue
        Dim temp As Cluster = Nothing
        If clusters.Count > 0 Then
            For i = 0 To clusters.Count - 1
                Dim cluster = clusters(i)
                If cluster IsNot Me Then
                    Dim value As Single = CompareTwoCluster(Me, cluster)
                    If value > maxValue Then
                        maxValue = value
                        temp = cluster
                    End If
                End If
            Next
        End If
        result.Enqueue(temp)
        Return result
    End Function
    ''' <summary>
    ''' 返回子簇的平均位置
    ''' </summary>
    Public Function GetAveragePosition() As Vector2
        If Children.Count = 0 Then
            Return Position
        Else
            Return VectorHelper.GetAveratePosition(GetPostionsOfChidren())
        End If
    End Function
    ''' <summary>
    ''' 返回子簇的平均颜色
    ''' </summary>
    Public Function GetAverageColor() As Color
        If Children.Count = 0 Then
            Return Color
        Else
            Return Utilities.ColorHelper.GetAverageColor(GetColorsOfChildren())
        End If
    End Function

    Private Function CompareTwoCluster(cluster1 As Cluster, cluster2 As Cluster) As Single
        Dim result As Single = 0

        Dim p1 As Vector2 = cluster1.Position
        Dim p2 As Vector2 = cluster2.Position
        Dim positionDistance As Single = 1 / (p1 - p2).Length

        Dim color1 As Color = cluster1.Color()
        Dim color2 As Color = cluster2.Color()
        Dim colorDistance As Single

        Dim vec1 As New Vector3(color1.R, color1.G, color1.B)
        Dim vec2 As New Vector3(color2.R, color2.G, color2.B)
        colorDistance = 1 / (1 + (vec1 - vec2).LengthSquared)

        'result = colorDistance
        result = positionDistance * colorDistance
        Return result
    End Function
    Private Function GetLeavesOfChildren() As List(Of Cluster)
        Return (Children.SelectMany(Of Cluster)(Function(c As Cluster) c.Leaves)).ToList
    End Function
    Private Function GetPostionsOfChidren() As IEnumerable(Of Vector2)
        Return From c As Cluster In Children Select c.Position
    End Function
    Private Function GetColorsOfChildren() As IEnumerable(Of Color)
        Return From c As Cluster In Children Select c.Color
    End Function
End Class
