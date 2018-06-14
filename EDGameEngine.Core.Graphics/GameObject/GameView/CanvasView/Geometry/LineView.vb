Imports Microsoft.Graphics.Canvas
''' <summary>
''' 可视化直线的视图
''' </summary>
Public Class LineView
    Inherits TypedCanvasView(Of VisualLine)
    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        If Target.Fill.State Then
            If Target.Points.Count > 1 Then
                For i = 0 To Target.Points.Count - 2
                    session.DrawLine(Target.Points(i), Target.Points(i + 1), Target.Fill.Color, Target.Width)
                Next
            End If
        End If
    End Sub
End Class
