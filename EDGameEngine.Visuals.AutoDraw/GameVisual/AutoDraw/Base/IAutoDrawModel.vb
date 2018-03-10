Imports System.Collections.Concurrent
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Microsoft.Graphics.Canvas

Public Interface IAutoDrawModel
    Inherits IGameVisual
    ''' <summary>
    ''' 绘制圆的图层索引集
    ''' </summary>
    Property CircleLayers As Integer()
    ''' <summary>
    ''' 当前绘制点集
    ''' </summary>
    Property CurrentPoints As ConcurrentQueue(Of VertexWithLayer)
    ''' <summary>
    ''' 原始图像
    ''' </summary>
    Property Image As CanvasBitmap
    ''' <summary>
    ''' 图像大小
    ''' </summary>
    Property ImageSize As Size
    ''' <summary>
    ''' 图层数量
    ''' </summary>
    Property LayerCount As Integer
    ''' <summary>
    ''' 单帧绘制长度
    ''' </summary>
    Property PointsCountPerFrame As Integer
    ''' <summary>
    ''' 单帧绘制最大长度
    ''' </summary>
    Property PointsCountMaxPerFrame As Integer
End Interface
