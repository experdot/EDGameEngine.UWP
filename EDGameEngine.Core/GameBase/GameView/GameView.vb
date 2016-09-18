Imports Microsoft.Graphics.Canvas
Public MustInherit Class GameView
    Implements IGameView
    Public Overridable Property Cache As CanvasBitmap Implements IGameView.Cache
    Public Overridable Property CacheAllowed As Boolean Implements IGameView.CacheAllowed

    Public MustOverride Sub OnDraw(DrawingSession As CanvasDrawingSession) Implements IGameView.OnDraw
    Public MustOverride Sub BeginDraw(DrawingSession As CanvasDrawingSession) Implements IGameView.BeginDraw
End Class
