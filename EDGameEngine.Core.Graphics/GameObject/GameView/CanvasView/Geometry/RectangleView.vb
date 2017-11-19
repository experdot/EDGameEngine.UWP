Imports Microsoft.Graphics.Canvas
''' <summary>
''' 可视化矩形的视图
''' </summary>
Public Class RectangleView
    Inherits TypedCanvasView(Of VisualRectangle)
    Public Sub New(Target As VisualRectangle)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        If Target.Fill.State Then
            drawingSession.FillRectangle(Target.Rectangle, Target.Fill.Color)
        End If
        If Target.Border.State Then
            Dim border As Single = Target.Border.Width / 2.0F
            Dim x As Single = CSng(Target.Rectangle.X - border)
            Dim y As Single = CSng(Target.Rectangle.Y - border)
            Dim width As Single = CSng(Target.Rectangle.Width + border)
            Dim height As Single = CSng(Target.Rectangle.Height + border)
            drawingSession.DrawRectangle(New Rect(x, y, width, height), Target.Border.Color, Target.Border.Width)
        End If
    End Sub
End Class