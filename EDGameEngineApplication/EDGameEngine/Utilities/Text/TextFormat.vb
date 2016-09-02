Imports Microsoft.Graphics.Canvas.Text
''' <summary>
''' 提供多个预制字体格式
''' </summary>
Public Class TextFormat
    Public Shared Center As New CanvasTextFormat With {
        .VerticalAlignment = CanvasVerticalAlignment.Center,
        .HorizontalAlignment = CanvasVerticalAlignment.Center}
End Class
