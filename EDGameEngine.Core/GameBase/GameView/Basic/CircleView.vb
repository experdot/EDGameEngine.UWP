Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas

Public Class CircleView
    Inherits TypedGameView(Of VisualCircle)
    Public Sub New(Target As VisualCircle)
        MyBase.New(Target)
    End Sub

    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        If Target.Fill.State Then
            DrawingSession.FillCircle(Vector2.Zero, Target.Radius, Target.Fill.Color)
        End If
        If Target.Border.State Then
            DrawingSession.DrawCircle(Vector2.Zero, Target.Radius, Target.Border.Color, Target.Border.Width)
        End If
    End Sub
End Class
