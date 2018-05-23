Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 由Win2D渲染的视图基类
''' </summary>
Public MustInherit Class CanvasView
    Implements ICanvasView

    Public Overridable Property Cache As CanvasBitmap Implements ICanvasView.Cache
    Public Overridable Property CacheAllowed As Boolean Implements IGameView.CacheAllowed
    Public Overridable Property CommandList As CanvasCommandList Implements ICanvasView.CommandList
    Public Overridable Property GameVisual As IGameVisual Implements IGameView.GameVisual

    Public MustOverride Sub OnDraw(drawingSession As CanvasDrawingSession) Implements ICanvasView.OnDraw
    Public MustOverride Sub BeginDraw(drawingSession As CanvasDrawingSession) Implements ICanvasView.BeginDraw
    Public MustOverride Sub AttachToGameVisual(target As IGameVisual) Implements IGameView.AttachToGameVisual

    Public Overridable Sub Start() Implements IGameObject.Start

    End Sub

    Public Overridable Sub Update() Implements IGameObject.Update

    End Sub
End Class
