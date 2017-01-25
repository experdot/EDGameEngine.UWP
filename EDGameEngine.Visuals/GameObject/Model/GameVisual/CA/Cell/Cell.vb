Imports System.Numerics
Imports EDGameEngine.Visuals
Imports Windows.UI
''' <summary>
''' 细胞
''' </summary>
Public Class Cell
    Implements ICell

    ''' <summary>
    ''' 位置
    ''' </summary>
    Public Property Location As Vector2 = Vector2.Zero Implements ICell.Location
    ''' <summary>
    ''' 大小
    ''' </summary>
    Public Property Size As Single = 1 Implements ICell.Size
    ''' <summary>
    ''' 颜色
    ''' </summary>
    Public Property Color As Color = Colors.Black Implements ICell.Color
    ''' <summary>
    ''' 年龄
    ''' </summary>
    Public Property Age As Integer = 0 Implements ICell.Age

    Shared xArray() As Integer = {-1, 0, 1, 1, 1, 0, -1, -1}
    Shared yArray() As Integer = {-1, -1, -1, 0, 1, 1, 1, 0}
    Shared Rnd As New Random
    Public Sub Grow() Implements ICell.Grow
        Age += 1
    End Sub

    Public Sub Move() Implements ICell.Move
        Dim r As Integer = Rnd.Next(0, 8)
        Location += New Vector2(xArray(r), yArray(r))
    End Sub
End Class
