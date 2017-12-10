Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Text
''' <summary>
''' 可视化文字的视图
''' </summary>
Public Class TextView
    Inherits TypedCanvasView(Of IVisualText)
    ''' <summary>
    ''' 字体布局
    ''' </summary>
    Public Property Format As CanvasTextFormat = TextFormatHelper.Center

    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        drawingSession.DrawText(Target.Text, Target.Offset, Target.Color, Format)
    End Sub
End Class
