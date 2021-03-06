﻿Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 包含层索引的顶点
''' </summary>
Public Class VertexWithLayer
    Inherits Vertex
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
    ''' <summary>
    ''' 下一个点
    ''' </summary>
    Public Property NextPoint As VertexWithLayer
End Class
