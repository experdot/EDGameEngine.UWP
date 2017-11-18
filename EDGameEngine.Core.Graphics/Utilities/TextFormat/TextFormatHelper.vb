Imports Microsoft.Graphics.Canvas.Text
''' <summary>
''' 提供多个预制字体格式的对象
''' </summary>
Public Class TextFormatHelper
    ''' <summary>
    ''' 居中对齐
    ''' </summary>
    Public Shared Center As New CanvasTextFormat With {
        .VerticalAlignment = CanvasVerticalAlignment.Center,
        .HorizontalAlignment = CanvasHorizontalAlignment.Center}
    ''' <summary>
    ''' 居中对齐，字体大小12
    ''' </summary>
    Public Shared CenterAndSize12 As New CanvasTextFormat With {
        .FontSize = 12,
        .VerticalAlignment = CanvasVerticalAlignment.Center,
        .HorizontalAlignment = CanvasHorizontalAlignment.Center}
End Class
