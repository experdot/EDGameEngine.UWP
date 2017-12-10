Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
''' <summary>
''' 多边形网格元胞自动机的视图
''' </summary>
Public Class GeometryCAView
    Inherits TypedCanvasView(Of IGeometryCA)
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        For Each SubCell In Target.Cells
            If SubCell IsNot Nothing Then
                drawingSession.FillGeometry(Target.Geometry, SubCell.Location, SubCell.Color)
            End If
        Next
    End Sub
End Class
