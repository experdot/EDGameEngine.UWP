Imports Microsoft.Graphics.Canvas
''' <summary>
''' 由Win2D渲染的视图基类
''' </summary>
Public MustInherit Class CanvasView
    Implements ICanvasView

    Public Overridable Property Cache As CanvasBitmap Implements ICanvasView.Cache
    Public Overridable Property CacheAllowed As Boolean Implements IGameView.CacheAllowed
    Public Overridable Property CommandList As CanvasCommandList Implements ICanvasView.CommandList

    Public MustOverride Sub OnDraw(drawingSession As CanvasDrawingSession) Implements ICanvasView.OnDraw
    Public MustOverride Sub BeginDraw(drawingSession As CanvasDrawingSession) Implements ICanvasView.BeginDraw
End Class
