Imports Microsoft.Graphics.Canvas

Public MustInherit Class GameView
    Implements IGameView
    Public MustOverride Sub Display(DrawingSession As CanvasDrawingSession) Implements IGameView.Display

End Class
