Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 二值化效果器
''' </summary>
Public Class ThresholdEffector
    Inherits EffectorBase
    Public Property Split As Integer = 128
    Public Overrides Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource
        Using bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(DrawingSession, source)
            Dim RawColors() As Color = bmp.GetPixelColors
            Dim NowColors(RawColors.Count - 1) As Color
            For i = 0 To RawColors.Count - 1
                NowColors(i) = If(BitmapPixelHelper.GetColorAverage(RawColors(i)) < Split, Colors.Black, Color.FromArgb(0, 0, 0, 0))
            Next
            Return CanvasBitmap.CreateFromColors(DrawingSession, NowColors, bmp.Bounds.Width, bmp.Bounds.Height)
        End Using
    End Function
End Class
