Imports Microsoft.Graphics.Canvas
''' <summary>
''' 可视化文字的视图
''' </summary>
Public Class TextView
    Inherits TypedCanvasView(Of IVisualText)
    Public Sub New(Target As IVisualText)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        drawingSession.DrawText(Target.Text, Target.Offset, Target.Color)
    End Sub
End Class
