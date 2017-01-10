Imports System.Numerics
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
End Class
