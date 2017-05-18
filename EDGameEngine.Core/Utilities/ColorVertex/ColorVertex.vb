Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 包含位置和颜色信息的顶点
''' </summary>
Public Structure ColorVertex
    ''' <summary>
    ''' 位置
    ''' </summary>
    Public Position As Vector2
    ''' <summary>
    ''' 颜色
    ''' </summary>
    Public Color As Color
    ''' <summary>
    ''' 由指定的参数创建并初始化一个实例
    ''' </summary>
    Public Sub New(pos As Vector2, col As Color)
        Position = pos
        Color = col
    End Sub
End Structure
