Imports System.Numerics
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

    Private Shared OffsetX() As Integer = {-1, 1, 0, 0, -1, 1, -1, 1}
    Private Shared OffsetY() As Integer = {0, 0, -1, 1, -1, 1, 1, -1}

    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim bmp As CanvasBitmap = BitmapCacheHelper.CacheEntireImage(resourceCreator, CType(source, ICanvasImage))
        Dim raws() As Color = bmp.GetPixelColors

        'Dim grays = GetGraysOfColors(raws)
        'Dim nows = ConvertGraysToColors(grays, CInt(bmp.Bounds.Width), CInt(bmp.Bounds.Height))
        'Dim nows = ConvertColorsDirectly(raws, CInt(bmp.Bounds.Width), CInt(bmp.Bounds.Height))
        'bmp.SetPixelColors(nows)
        'Return bmp

        Dim creator = CType(resourceCreator, ICanvasResourceCreatorWithDpi)
        Dim canvas As New CanvasRenderTarget(creator, New Size(bmp.Bounds.Width, bmp.Bounds.Height))
        Using ds = canvas.CreateDrawingSession
            ds.Clear(Colors.Transparent)
            Dim grays = GetGraysOfColors(raws)
            ConvertGrays(ds, raws, grays, CInt(bmp.Bounds.Width), CInt(bmp.Bounds.Height))
        End Using

        Return canvas
    End Function

    Private Function GetGraysOfColors(colors As Color()) As Integer()
        Dim result(colors.Length - 1) As Integer
        For i = 0 To result.Length - 1
            result(i) = BitmapPixelHelper.GetColorAverage(colors(i))
        Next
        Return result
    End Function

    Private Sub ConvertGrays(ds As CanvasDrawingSession, raws As Color(), grays As Integer(), width As Integer, height As Integer)
        For x = 0 To width - 1
            For y = 0 To height - 1
                Dim sum As Single = 0
                Dim final As Byte = 0
                Dim center As Integer = grays(y * width + x)
                Dim vector As New Vector2
                For i = 0 To 7
                    Dim dx = x + OffsetX(i)
                    Dim dy = y + OffsetY(i)
                    If (dx >= 0 AndAlso dy >= 0 AndAlso dx < width AndAlso dy < height) Then
                        Dim temp = (grays(dy * width + dx) - center)
                        sum += CSng(temp ^ 2)
                        vector += New Vector2(OffsetX(i), OffsetY(i)) * temp
                    End If
                Next
                final = CByte(Math.Sqrt(Math.Abs(sum)) / 3)

                Dim current = New Vector2(x, y)
                Dim target = current + vector
                Dim raw = raws(y * width + x)
                ds.FillCircle(current, 1, Color.FromArgb(final, raw.R, raw.G, raw.B))
                'ds.DrawLine(current, target, Color.FromArgb(10, gray, gray, gray))
            Next
        Next
    End Sub

    Private Function ConvertGraysToColors(grays As Integer(), width As Integer, height As Integer) As Color()
        Dim result(grays.Length - 1) As Color
        For x = 0 To width - 1
            For y = 0 To height - 1
                Dim sum As Single = 0
                Dim final As Byte = 0
                Dim center As Integer = grays(y * width + x)
                For i = 0 To 7
                    Dim dx = x + OffsetX(i)
                    Dim dy = y + OffsetY(i)
                    If (dx >= 0 AndAlso dy >= 0 AndAlso dx < width AndAlso dy < height) Then
                        sum += CSng((grays(dy * width + dx) - center) ^ 2)
                    End If
                Next
                final = CByte(255 - Math.Sqrt(Math.Abs(sum)) / 3)
                result(y * width + x) = Color.FromArgb(255, final, final, final)
            Next
        Next
        Return result
    End Function

    Private Function ConvertColorsDirectly(colors As Color(), width As Integer, height As Integer) As Color()
        Dim result(colors.Length - 1) As Color
        For x = 0 To width - 1
            For y = 0 To height - 1
                Dim rs As Single = 0
                Dim gs As Single = 0
                Dim bs As Single = 0

                Dim center As Color = colors(y * width + x)
                For i = 0 To 7
                    Dim dx = x + OffsetX(i)
                    Dim dy = y + OffsetY(i)
                    If (dx >= 0 AndAlso dy >= 0 AndAlso dx < width AndAlso dy < height) Then
                        Dim color = colors(dy * width + dx)
                        rs += CSng((CInt(color.R) - CInt(center.R)) ^ 2)
                        gs += CSng((CInt(color.G) - CInt(center.G)) ^ 2)
                        bs += CSng((CInt(color.B) - CInt(center.B)) ^ 2)
                    End If
                Next

                Dim rf, gf, bf As Byte
                rf = CByte(255 - Math.Sqrt(Math.Abs(rs)) / 3)
                gf = CByte(255 - Math.Sqrt(Math.Abs(gs)) / 3)
                bf = CByte(255 - Math.Sqrt(Math.Abs(bs)) / 3)
                result(y * width + x) = Color.FromArgb(255, rf, gf, bf)
            Next
        Next
        Return result
    End Function
End Class
