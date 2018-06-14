Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 对比度效果器
''' </summary>
Public Class ContrastEffect
    Inherits CanvasEffectBase
    Public Property Amount As Single = 2.0F
    Public Overrides Function Effect(source As IGraphicsEffectSource, creator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(creator, CType(source, ICanvasImage))
        Dim raws() As Color = bmp.GetPixelColors
        Dim nows(raws.Count - 1) As Color
        Dim k = Math.Max(0, Amount)
        Dim s = 255 ^ (k - 1)
        For i = 0 To raws.Count - 1
            Dim r = CByte((CInt(raws(i).R) ^ k) / s)
            Dim g = CByte((CInt(raws(i).G) ^ k) / s)
            Dim b = CByte((CInt(raws(i).B) ^ k) / s)
            nows(i) = Color.FromArgb(raws(i).A, r, g, b)
        Next
        bmp.SetPixelColors(nows)
        Return bmp
    End Function
End Class

