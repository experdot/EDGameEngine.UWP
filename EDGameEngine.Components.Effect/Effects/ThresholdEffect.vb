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
        Dim RawColors() As Color = bmp.GetPixelColors
        Dim NowColors(RawColors.Count - 1) As Color
        For i = 0 To RawColors.Count - 1
            NowColors(i) = If(BitmapPixelHelper.GetColorAverage(RawColors(i)) < Split, Colors.Black, Color.FromArgb(0, 0, 0, 0))
        Next
        bmp.SetPixelColors(NowColors)
        Return bmp
    End Function
End Class
