Imports EDGameEngine
Imports Microsoft.Graphics.Canvas

Public MustInherit Class GameView
    Implements IGameView
    Public MustOverride Sub OnDraw(DrawingSession As CanvasDrawingSession) Implements IGameView.OnDraw
    Public MustOverride Sub BeginDraw(DrawingSession As CanvasDrawingSession) Implements IGameView.BeginDraw
End Class
