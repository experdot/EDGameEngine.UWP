Imports System.Numerics
''' <summary>
''' 碎片容器地图的单元格
''' </summary>
Public Class MapCell
    ''' <summary>
    ''' 位置
    ''' </summary>
    Public Property Location As Vector2
    ''' <summary>
    ''' 碎片
    ''' </summary>
    Public Property Fragment As Fragment
    Public Property Value As Integer
    Public Property Around As MapCellAround
    Public Sub New(x As Integer, y As Integer, p1 As Boolean, p2 As Boolean, p3 As Boolean, p4 As Boolean)
        Location = New Vector2(x, y)
        Around = New MapCellAround(x, y, p1, p2, p3, p4)
    End Sub
End Class
