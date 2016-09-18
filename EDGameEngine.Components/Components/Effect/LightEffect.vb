Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 表示光照效果器
''' </summary>
Public Class LightEffect
    Inherits EffectBase
    Public Overrides Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource
        Static ShadowColor As Color = Color.FromArgb(255, 128, 128, 128)
        Static LightColor As Color = Color.FromArgb(255, 255, 255, 255)
        Static ScanVec As New Vector2(1, 0)
        Static ScanRadius As Single
        Static w, h As Integer
        Static LightLoc As New Vector2(0, 30)
        Static Rnd As New Random
        Using bmp As CanvasBitmap = BitmapCacheHelper.CacheImage(DrawingSession, CType(source, ICanvasImage))
            Dim RawColors() As Color = bmp.GetPixelColors
            Dim NowColors(RawColors.Count - 1) As Color
            w = CInt(bmp.Bounds.Width)
            h = CInt(bmp.Bounds.Height)
            ScanRadius = CSng(Math.Sqrt(w * w + h * h))
            LightLoc.X = CSng(Math.Cos(Environment.TickCount / 5000) * w / 3 + w / 2)
            LightLoc.Y = CSng(Math.Sin(Environment.TickCount / 5000) * h / 3 + h / 2)
            '添加阴影区
            For i = 0 To RawColors.Count - 1
                NowColors(i) = If(RawColors(i).A = 0, ShadowColor, RawColors(i))
            Next
            '添加光照区
            For rotate As Single = 0 To (Math.PI * 2) Step 1 / ScanRadius
                Dim IsBlack As Boolean = False
                For length As Single = 0 To ScanRadius Step 1
                    Dim tempVec As Vector2 = ScanVec.RotateNew(rotate) * length + LightLoc
                    Dim x As Integer = CInt(tempVec.X)
                    Dim y As Integer = CInt(tempVec.Y)
                    If (x > w - 1 OrElse x < 0 OrElse y > h - 1 OrElse y < 0) Then
                        Exit For
                    Else
                        If RawColors(y * w + x).A = 0 Then
                            If IsBlack Then
                                NowColors(y * w + x) = Color.FromArgb(CByte(180 * length / ScanRadius + 50 * (1 - length / ScanRadius)), 0, 0, 0)
                            Else
                                NowColors(y * w + x) = Color.FromArgb(CByte(128 * length / ScanRadius), 0, 0, 0)
                            End If
                        Else
                            IsBlack = True
                        End If
                    End If
                Next
            Next

            Return CanvasBitmap.CreateFromColors(DrawingSession, NowColors, CInt(bmp.Bounds.Width), CInt(bmp.Bounds.Height))
        End Using
    End Function
End Class
