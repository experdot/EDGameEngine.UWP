Imports Microsoft.Graphics.Canvas
''' <summary>
''' 可视化矩形的视图
''' </summary>
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