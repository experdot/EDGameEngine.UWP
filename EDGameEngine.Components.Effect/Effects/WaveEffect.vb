Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 波纹效果器
''' </summary>
Public Class WaveEffect
    Inherits CanvasEffectBase
    ''' <summary>
    ''' 波纹半径
    ''' </summary>
    Public Property Amount As Single = 10

    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(resourceCreator, CType(source, ICanvasImage))
        Dim raws() As Color = bmp.GetPixelColors()
        Dim nows(raws.Count - 1) As Color

        If Amount > 300 Then Amount = 300
        Dim w As Integer = CInt(bmp.Bounds.Width)
        Dim h As Integer = CInt(bmp.Bounds.Height)
        Dim tempX, tempY As Integer

        For x = 0 To w - 1
            For y = 0 To h - 1
                tempX = CInt(x + Math.Sin(y / 10) * Amount)
                tempY = CInt(y + Math.Cos(x / 10) * Amount)
                If tempX < 0 Then tempX = 0
                If tempX > w - 1 Then tempX = w - 1
                If tempY < 0 Then tempY = 0
                If tempY > h - 1 Then tempY = h - 1
                nows(y * w + x) = raws(tempY * w + tempX)
            Next
        Next

        bmp.SetPixelColors(nows)
        Return bmp
    End Function
End Class

