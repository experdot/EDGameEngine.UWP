Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
''' <summary>
''' 多边形网格元胞自动机的视图
''' </summary>
Public Class GeometryCAView
    Inherits TypedGameView(Of IGeometryCA)
    Public Sub New(Target As IGeometryCA)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        For Each SubCell In Target.Cells
            If SubCell IsNot Nothing Then
                drawingSession.FillGeometry(Target.Geometry, SubCell.Location, SubCell.Color)
            End If
        Next
    End Sub
End Class
