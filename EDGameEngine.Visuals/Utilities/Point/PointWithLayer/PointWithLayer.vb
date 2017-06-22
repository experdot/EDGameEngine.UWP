Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 包含层索引的点
''' </summary>
Public Class PointWithLayer
    Inherits Point
    ''' <summary>
    ''' 层索引
    ''' </summary>
    Public Property LayerIndex As Integer
    ''' <summary>
    ''' 中心
    ''' </summary>
    Public Property Center As Vector2
    ''' <summary>
    ''' 用户定义的大小
    ''' </summary>
    Public Property UserSize As Single
    ''' <summary>
    ''' 用户定义的颜色
    ''' </summary>
    Public Property UserColor As Color
End Class
