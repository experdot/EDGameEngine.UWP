Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 测试效果器
''' </summary>
Public Class TestEffect
    Inherits CanvasEffectBase
    Public Property Split As Integer = 128
    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(resourceCreator, CType(source, ICanvasImage))
        Dim raws() As Color = bmp.GetPixelColors
        Dim nows(raws.Count - 1) As Color

        Static Period As Single = 0.0F
        Period = (Period + 0.1F) Mod CSng(Math.PI * 2)

        Dim k = 2 + Math.Sin(Period) * 2
        Dim s = 255 ^ (k - 1)
        For i = 0 To raws.Count - 1
            Dim r = CByte((CInt(raws(i).R) ^ k) / s)
            Dim g = CByte((CInt(raws(i).G) ^ k) / s)
            Dim b = CByte((CInt(raws(i).B) ^ k) / s)
            nows(i) = Color.FromArgb(255, r, g, b)
        Next
        bmp.SetPixelColors(nows)
        Return bmp
    End Function
End Class
