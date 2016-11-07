Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 磨砂玻璃效果器
''' </summary>
Public Class FrostedEffect
    Inherits EffectBase
    ''' <summary>
    ''' 磨砂半径
    ''' </summary>
    Public Property Amount As Single = 10
    ''' <summary>
    ''' 磨砂质量
    ''' </summary>
    Public Property Quality As EffectQuality
    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Static Rnd As New Random
        Using bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(CType(resourceCreator, CanvasDrawingSession), CType(source, ICanvasImage))
            Dim RawColors() As Color = bmp.GetPixelColors
            Dim NowColors(RawColors.Count - 1) As Color
            '打散像素
            If Quality = EffectQuality.Low Then
                For i = 0 To RawColors.Count - 1
                    Dim temp As Integer = i + Rnd.Next(0, CInt(Amount))
                    If temp < 0 Then temp = 0
                    If temp >= RawColors.Count Then temp = RawColors.Count - 1
                    NowColors(i) = RawColors(temp)
                Next
            Else
                If Amount > 300 Then Amount = 300
                Dim w As Integer = CInt(bmp.Bounds.Width)
                Dim h As Integer = CInt(bmp.Bounds.Height)
                Dim tempX, tempY As Integer
                For x = 0 To w - 1
                    For y = 0 To h - 1
                        tempX = x + Rnd.Next(1, CInt(Amount)) - CInt(Amount / 2)
                        tempY = y + Rnd.Next(1, CInt(Amount)) - CInt(Amount / 2)
                        If tempX < 0 Then tempX = 0
                        If tempX > w - 1 Then tempX = w - 1
                        If tempY < 0 Then tempY = 0
                        If tempY > h - 1 Then tempY = h - 1
                        NowColors(y * w + x) = RawColors(tempY * w + tempX)
                    Next
                Next
            End If
            Return CanvasBitmap.CreateFromColors(resourceCreator, NowColors, CInt(bmp.Bounds.Width), CInt(bmp.Bounds.Height))
        End Using
    End Function
End Class
