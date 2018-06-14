Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 磨砂玻璃效果器
''' </summary>
Public Class FrostedEffect
    Inherits CanvasEffectBase
    ''' <summary>
    ''' 磨砂半径
    ''' </summary>
    Public Property Amount As Single = 10
    ''' <summary>
    ''' 磨砂质量
    ''' </summary>
    Public Property Quality As EffectQuality
    Public Overrides Function Effect(source As IGraphicsEffectSource, creator As ICanvasResourceCreator) As IGraphicsEffectSource
        'Dim bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(resourceCreator, CType(source, ICanvasImage))
        Dim bmp As CanvasBitmap = BitmapCacheHelper.CacheImageClip(creator, CType(source, ICanvasImage), Target.Scene.Rect)
        Dim raws() As Color = bmp.GetPixelColors()
        Dim nows(raws.Count - 1) As Color
        '打散像素
        If Quality = EffectQuality.Low Then
            For i = 0 To raws.Count - 1
                Dim temp As Integer = i + GameBody.Rnd.Next(0, CInt(Amount))
                If temp < 0 Then temp = 0
                If temp >= raws.Count Then temp = raws.Count - 1
                nows(i) = raws(temp)
            Next
        Else
            If Amount > 300 Then Amount = 300
            Dim w As Integer = CInt(bmp.Bounds.Width)
            Dim h As Integer = CInt(bmp.Bounds.Height)
            Dim tempX, tempY As Integer
            For x = 0 To w - 1
                For y = 0 To h - 1
                    tempX = x + Rnd.Next(0, CInt(Amount)) - CInt(Amount / 2)
                    tempY = y + Rnd.Next(0, CInt(Amount)) - CInt(Amount / 2)
                    If tempX < 0 Then tempX = 0
                    If tempX > w - 1 Then tempX = w - 1
                    If tempY < 0 Then tempY = 0
                    If tempY > h - 1 Then tempY = h - 1
                    nows(y * w + x) = raws(tempY * w + tempX)
                Next
            Next
        End If
        bmp.SetPixelColors(nows)
        Return bmp
    End Function
End Class
