Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 细胞接口
''' </summary>
Public Interface ICell
    ''' <summary>
    ''' 颜色
    ''' </summary>
    Property Color As Color
    ''' <summary>
    ''' 位置
    ''' </summary>
    Property Location As Vector2
    ''' <summary>
    ''' 大小
    ''' </summary>
    Property Size As Single
    ''' <summary>
    ''' 年龄
    ''' </summary>
    Property Age As Integer
    ''' <summary>
    ''' 生长
    ''' </summary>
    Sub Grow()
    ''' <summary>
    ''' 移动
    ''' </summary>
    Sub Move()
End Interface
