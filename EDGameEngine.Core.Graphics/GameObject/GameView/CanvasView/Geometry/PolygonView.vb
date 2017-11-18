Imports Microsoft.Graphics.Canvas
''' <summary>
''' 可视化多边形的视图
''' </summary>
Public Class PolygonView
    Inherits TypedCanvasView(Of VisualPolygon)
    Public Sub New(Target As VisualPolygon)
        MyBase.New(Target)
    End Sub

    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        If Target.Fill.State Then
            drawingSession.FillGeometry(Target.Geometry, Target.Fill.Color)
        End If
        If Target.Border.State Then
            drawingSession.DrawGeometry(Target.Geometry, Target.Border.Color, Target.Border.Width)
        End If
    End Sub
End Class
