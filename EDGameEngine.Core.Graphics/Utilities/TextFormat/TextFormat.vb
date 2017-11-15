Imports Microsoft.Graphics.Canvas.Text
''' <summary>
''' 提供多个预制字体格式的对象
''' </summary>
Public Class TextFormat
    Public Shared Center As New CanvasTextFormat With {
        .VerticalAlignment = CanvasVerticalAlignment.Center,
        .HorizontalAlignment = CanvasHorizontalAlignment.Center}
    Public Shared CenterL As New CanvasTextFormat With {
        .FontSize = 12,
        .VerticalAlignment = CanvasVerticalAlignment.Center,
        .HorizontalAlignment = CanvasHorizontalAlignment.Center}
End Class
