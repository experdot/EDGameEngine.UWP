Imports Microsoft.Graphics.Canvas
''' <summary>
''' 表示一个由Win2D渲染的视图
''' </summary>
Public Interface ICanvasView
    Inherits IGameView
    ''' <summary>
    ''' 位图缓存
    ''' </summary>
    Property Cache As CanvasBitmap
    ''' <summary>
    ''' 绘图命令
    ''' </summary>
    Property CommandList As CanvasCommandList
    ''' <summary>
    ''' 预绘制
    ''' </summary>
    Sub BeginDraw(session As CanvasDrawingSession)
    ''' <summary>
    ''' 绘制
    ''' </summary>
    Sub OnDraw(session As CanvasDrawingSession)
End Interface
