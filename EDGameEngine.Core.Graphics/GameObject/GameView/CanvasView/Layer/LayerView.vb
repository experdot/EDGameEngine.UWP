Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 图层视图
''' </summary>
Public Class LayerView
    Inherits TypedCanvasView(Of ILayer)
    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        For Each body In Target.GameBodys
            CType(body.Presenter, ICanvasView)?.BeginDraw(session)
        Next
    End Sub
End Class
