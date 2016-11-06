Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Public MustInherit Class GameView
    Implements IGameView

    Public Overridable Property Cache As CanvasBitmap Implements IGameView.Cache
    Public Overridable Property CacheAllowed As Boolean Implements IGameView.CacheAllowed
    Public Overridable Property CommandList As CanvasCommandList Implements IGameView.CommandList

    Public MustOverride Sub OnDraw(drawingSession As CanvasDrawingSession) Implements IGameView.OnDraw
    Public MustOverride Sub BeginDraw(drawingSession As CanvasDrawingSession) Implements IGameView.BeginDraw
End Class
