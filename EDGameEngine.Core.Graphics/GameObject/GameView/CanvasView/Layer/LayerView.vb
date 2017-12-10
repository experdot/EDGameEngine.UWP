Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 图层视图
''' </summary>
Public Class LayerView
    Inherits TypedCanvasView(Of ILayer)
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        For Each SubBody In Target.GameBodys
            CType(SubBody.Presenter, ICanvasView).BeginDraw(drawingSession)
        Next
    End Sub
End Class
