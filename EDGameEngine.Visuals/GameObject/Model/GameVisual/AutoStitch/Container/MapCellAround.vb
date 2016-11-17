Imports System.Numerics
''' <summary>
''' 地图单元格的周边信息
''' </summary>
Public Class MapCellAround
    Public Property FourB As Boolean()
    Public Property FourPoint() As Vector2()
    Public Sub New(x As Integer, y As Integer, p1 As Boolean, p2 As Boolean, p3 As Boolean, p4 As Boolean)
        ReDim FourPoint(3)
        ReDim FourB(3)
        FourPoint(0) = New Vector2(x - 1, y)
        FourPoint(1) = New Vector2(x + 1, y)
        FourPoint(2) = New Vector2(x, y - 1)
        FourPoint(3) = New Vector2(x, y + 1)
        FourB(0) = p1
        FourB(1) = p2
        FourB(2) = p3
        FourB(3) = p4
    End Sub
End Class
