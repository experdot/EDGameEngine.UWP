Imports Microsoft.Graphics.Canvas

Public Class TextView
    Inherits TypedGameView(Of IVisualText)
    Public Sub New(Target As IVisualText)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        drawingSession.DrawText(Target.Text, Target.Offset, Target.Color)
    End Sub
End Class
