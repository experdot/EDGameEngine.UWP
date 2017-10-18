Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 像素效果器
''' </summary>
Public Class PixelEffect
    Inherits CanvasEffectBase
    ''' <summary>
    ''' 数量
    ''' </summary>
    Public Property Amount As Single = 10

    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(resourceCreator, CType(source, ICanvasImage))
        Dim RawColors() As Color = bmp.GetPixelColors()
        Dim NowColors(RawColors.Count - 1) As Color

        Static Offset As Single = 0.0F
        Offset -= 0.1F

        If Amount > 300 Then Amount = 300

        Dim w As Integer = CInt(bmp.Bounds.Width)
        Dim h As Integer = CInt(bmp.Bounds.Height)
        Dim tempX, tempY As Integer


        Dim theate As Single = 0.01F
        Dim mid As New Vector2(w / 2.0F, h / 2.0F)

        'For x = 0 To w - 1
        '    For y = 0 To h - 1
        '        Dim vec = New Vector2(x, y) - mid
        '        vec.Rotate(Offset * CSng(Math.Log(Math.E + vec.Length)))
        '        vec = vec + mid

        '        tempX = CInt(vec.X)
        '        tempY = CInt(vec.Y)

        '        If tempX < 0 Then tempX = 0
        '        If tempX > w - 1 Then tempX = w - 1
        '        If tempY < 0 Then tempY = 0
        '        If tempY > h - 1 Then tempY = h - 1

        '        NowColors(y * w + x) = RawColors(tempY * w + tempX)
        '    Next
        'Next

        For x = 0 To w - 1
            For y = 0 To h - 1
                Dim vec = New Vector2(x, y) - mid
                Dim len As Single = vec.Length

                vec = vec * CSng(Math.Sin(Offset + Math.Log(Math.E + vec.Length * vec.Length)))
                len = vec.Length

                vec = vec + mid
                len = vec.Length

                tempX = CInt(vec.X)
                tempY = CInt(vec.Y)

                If tempX < 0 Then tempX = 0
                If tempX > w - 1 Then tempX = w - 1
                If tempY < 0 Then tempY = 0
                If tempY > h - 1 Then tempY = h - 1

                NowColors(y * w + x) = RawColors(tempY * w + tempX)
                'NowColors(y * w + x).A = CByte(NowColors(y * w + x).A * Math.Abs(CSng(Math.Sin(Offset + Math.Log(Math.E + len)))))
                'NowColors(y * w + x).R = CByte(NowColors(y * w + x).R * Math.Abs(CSng(Math.Sin(Offset + Math.Log(Math.E + len)))))
                'NowColors(y * w + x).G = CByte(NowColors(y * w + x).G * Math.Abs(CSng(Math.Sin(Offset + Math.Log(Math.E + len)))))
                'NowColors(y * w + x).B = CByte(NowColors(y * w + x).B * Math.Abs(CSng(Math.Sin(Offset + Math.Log(Math.E + len)))))
            Next
        Next

        bmp.SetPixelColors(NowColors)
        Return bmp
    End Function
End Class

