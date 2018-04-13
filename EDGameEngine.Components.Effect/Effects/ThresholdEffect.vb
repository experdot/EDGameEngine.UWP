Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 二值化效果器
''' </summary>
Public Class ThresholdEffect
    Inherits CanvasEffectBase
    Public Property Split As Integer = 128
    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(resourceCreator, CType(source, ICanvasImage))
        Dim raws() As Color = bmp.GetPixelColors
        Dim nows(raws.Count - 1) As Color
        For i = 0 To raws.Count - 1
            nows(i) = If(BitmapPixelHelper.GetColorAverage(raws(i)) < Split, Colors.Black, Color.FromArgb(0, 0, 0, 0))
        Next
        bmp.SetPixelColors(nows)
        Return bmp
    End Function
End Class
