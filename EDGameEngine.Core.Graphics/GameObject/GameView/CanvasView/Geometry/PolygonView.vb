Imports Microsoft.Graphics.Canvas
''' <summary>
''' 可视化多边形的视图
''' </summary>
Public Class PolygonView
    Inherits TypedCanvasView(Of VisualPolygon)
    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        If Target.Fill.State Then
            session.FillGeometry(Target.Geometry, Target.Fill.Color)
        End If
        If Target.Border.State Then
            session.DrawGeometry(Target.Geometry, Target.Border.Color, Target.Border.Width)
        End If
    End Sub
End Class
