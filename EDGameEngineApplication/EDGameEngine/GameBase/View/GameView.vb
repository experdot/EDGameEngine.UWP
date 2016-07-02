Imports Microsoft.Graphics.Canvas

Public MustInherit Class GameView
    Implements IGameView
    Public MustOverride Sub OnDraw(DrawingSession As CanvasDrawingSession) Implements IGameView.OnDraw
End Class
