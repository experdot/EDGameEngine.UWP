Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class RectangleView
    Inherits TypedGameView(Of VisualRectangle)
    Public Sub New(Target As VisualRectangle)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        If Target.Fill.State Then
            DrawingSession.FillRectangle(Target.Rect, Target.Fill.Color)
        End If
        If Target.Border.State Then
            DrawingSession.DrawRectangle(Target.Rect, Target.Border.Color, Target.Border.Width)
        End If
    End Sub
End Class