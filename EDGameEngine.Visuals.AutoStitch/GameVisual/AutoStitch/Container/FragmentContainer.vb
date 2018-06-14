Imports System.Numerics
''' <summary>
''' 碎片容器
''' </summary>
Public Class FragmentContainer
    ''' <summary>
    ''' 碎片集合
    ''' </summary>
    Public Property Fragments As New List(Of Fragment)
    ''' <summary>
    ''' 容器地图
    ''' </summary>
    Public Map As FragmentContainerMap
    Public Sub New(MapX As Integer, MapY As Integer)
        Map = New FragmentContainerMap(MapX, MapY)
    End Sub
    ''' <summary>
    ''' 添加一个碎片
    ''' </summary>
    Public Sub Add(fragment As Fragment, x As Integer, y As Integer)
        Fragments.Add(fragment)
        fragment.Location = New Vector2(x, y)
        Map.Value(x, y) = True
    End Sub
    ''' <summary>
    ''' 移除一个碎片
    ''' </summary>
    Public Sub Remove(fragment As Fragment, x As Integer, y As Integer)
        Fragments.Remove(fragment)
        fragment.Location = New Vector2(0, 0)
        Map.Value(x, y) = False
    End Sub
    ''' <summary>
    ''' 移动指定位置的碎片至指定的碎片容器
    ''' </summary>
    Public Sub MoveTo(ByRef target As FragmentContainer, x As Integer, y As Integer)
        For Each scrip In Fragments
            If scrip.Location.X = x And scrip.Location.Y = y Then
                scrip.Location = New Vector2(0, 0)
                Map.Value(x, y) = False
                Fragments.Remove(scrip)
                target.Add(scrip, x, y)
                Exit For
            End If
        Next
    End Sub
End Class
