Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
''' <summary>
''' 多边形网格元胞自动机的视图
''' </summary>
Public Class GeometryCAView
    Inherits TypedCanvasView(Of IGeometryCA)
    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        For Each cell In Target.Cells
            If cell IsNot Nothing Then
                session.FillGeometry(Target.Geometry, cell.Location, cell.Color)
            End If
        Next
    End Sub
End Class
