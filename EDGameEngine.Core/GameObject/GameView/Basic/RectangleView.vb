Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class RectangleView
    Inherits TypedGameView(Of VisualRectangle)
    Public Sub New(Target As VisualRectangle)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        If Target.Fill.State Then
            drawingSession.FillRectangle(Target.Rect, Target.Fill.Color)
        End If
        If Target.Border.State Then
            drawingSession.DrawRectangle(Target.Rect, Target.Border.Color, Target.Border.Width)
        End If
    End Sub
End Class