Imports Microsoft.Graphics.Canvas
''' <summary>
''' 表示游戏图层
''' </summary>
Public Interface ILayer
    Inherits IGameVisual
    ''' <summary>
    ''' 图层视图
    ''' </summary>
    Property Presenter As LayerView
    ''' <summary>
    ''' 图层包含的可视化对象
    ''' </summary>
    Property GameBodys As List(Of IGameBody)
    ''' <summary>
    ''' 图层绘制
    ''' </summary>
    Sub OnDraw(drawingSession As CanvasDrawingSession)
End Interface
