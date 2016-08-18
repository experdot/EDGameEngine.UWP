Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class BitmapPixelHelper
    ''' <summary> 
    ''' 由指定阈值判断两个RGB颜色是否相近
    ''' </summary> 
    Public Shared Function GetColorSimilar(ByVal Color1 As Color, ByVal Color2 As Color, ByVal distance As Single) As Boolean
        Dim r As Integer = CInt(Color1.R) - CInt(Color2.R)
        Dim g As Integer = CInt(Color1.G) - CInt(Color2.G)
        Dim b As Integer = CInt(Color1.B) - CInt(Color2.B)
        Dim absDis As Integer = Math.Sqrt(r * r + g * g + b * b)
        Return If(absDis < distance, True, False)
    End Function
    ''' <summary> 
    ''' 返回指定颜色的RGB平均值
    ''' </summary> 
    Public Shared Function GetColorAverage(ByVal col As Color) As Integer
        Return (CInt(col.R) + CInt(col.G) + CInt(col.B)) / 3
    End Function
    ''' <summary>
    ''' 返回指定位图的颜色数组
    ''' </summary>
    ''' <param name="bmp"></param>
    ''' <returns></returns>
    Public Shared Function GetColorArr(ByRef bmp As CanvasBitmap) As Color(,)
        Dim RawArr = bmp.GetPixelColors()
        Dim TempArr(bmp.Bounds.Width - 1, bmp.Bounds.Height - 1) As Color
        For i = 0 To bmp.Bounds.Width - 1
            For j = 0 To bmp.Bounds.Height - 1
                TempArr(i, j) = RawArr(bmp.Bounds.Width * j + i)
            Next
        Next
        Return TempArr
    End Function
    ''' <summary>
    ''' 返回指定图位图的二值化图像
    ''' </summary>
    ''' <param name="bmp"></param>
    ''' <param name="split"></param>
    ''' <returns></returns>
    Public Shared Function GetThresholdImage(drawingSession As CanvasDrawingSession, bmp As CanvasBitmap, split As Single) As CanvasBitmap
        Dim RawColors() As Color = bmp.GetPixelColors
        Dim NowColors(RawColors.Count - 1) As Color
        For i = 0 To RawColors.Count - 1
            NowColors(i) = If(GetColorAverage(RawColors(i)) < split, Colors.Black, Color.FromArgb(0, 0, 0, 0))
        Next
        Return CanvasBitmap.CreateFromColors(drawingSession, NowColors, bmp.Bounds.Width, bmp.Bounds.Height)
    End Function
    ''' <summary>
    ''' 返回指定图位图的二值化原始像素图像
    ''' </summary>
    ''' <param name="bmp"></param>
    ''' <param name="split"></param>
    ''' <returns></returns>
    Public Shared Function GetThresholdImageRaw(drawingSession As CanvasDrawingSession, bmp As CanvasBitmap, split As Single) As CanvasBitmap
        Dim RawColors() As Color = bmp.GetPixelColors
        Dim NowColors(RawColors.Count - 1) As Color
        For i = 0 To RawColors.Count - 1
            Dim temp = GetColorAverage(RawColors(i))
            NowColors(i) = If(temp >= split - 0.5 AndAlso temp < split + 0.5, RawColors(i), Color.FromArgb(0, 0, 0, 0))
        Next
        Return CanvasBitmap.CreateFromColors(drawingSession, NowColors, bmp.Bounds.Width, bmp.Bounds.Height)
    End Function
    ''' <summary>
    ''' 返回指定位图的水纹图像
    ''' </summary>
    Public Shared Function GetWaveImage(drawingSession As CanvasDrawingSession, rect As Rect, SrcData() As Color, buffer1() As Integer, buffer2() As Integer) As CanvasBitmap
        Dim DesData() As Color = SrcData.Clone
        Dim xoff, yoff As Integer
        Dim width As Integer = rect.Width
        Dim height As Integer = rect.Height
        Dim k As Integer = width
        For i = 1 To height - 2
            For j = 0 To width - 1
                xoff = buffer1(k - 1) - buffer1(k + 1) 'x偏移
                yoff = buffer1(k - width) - buffer1(k + width) 'y偏移
                If (xoff = 0 AndAlso yoff = 0) OrElse i + yoff <= 0 OrElse i + yoff >= height OrElse j + xoff <= 0 OrElse j + xoff >= width Then
                    k += 1 '边界判断
                    Continue For
                End If
                Dim pos1, pos2 As Integer
                pos1 = (i + yoff) * width + j + xoff
                pos2 = i * width + j
                DesData(pos2) = SrcData(pos1) '根据偏移量重新渲染界面
                k += 1
            Next
        Next
        Return CanvasBitmap.CreateFromColors(drawingSession, DesData, width, height)
    End Function
    ''' <summary>
    ''' 返回指定位图的二值化数组
    ''' </summary>
    ''' <param name="bmp"></param>
    ''' <returns></returns>
    Public Shared Function GetImageBol(ByVal bmp As CanvasBitmap, Optional split As Integer = 128) As Integer(,)
        Dim ResultArr(bmp.Bounds.Width - 1, bmp.Bounds.Height - 1) As Integer
        Dim RawColors() As Color = bmp.GetPixelColors
        For i = 0 To bmp.Bounds.Width - 1
            For j = 0 To bmp.Bounds.Height - 1
                ResultArr(i, j) = If(GetColorAverage(RawColors(bmp.Bounds.Width * j + i)) < split, 1, 0)
            Next
        Next
        Return ResultArr
    End Function
    ''' <summary>
    ''' 返回指定位图的二值化特定范围数组
    ''' </summary>
    ''' <param name="bmp"></param>
    ''' <returns></returns>
    Public Shared Function GetImageBolLimit(ByVal bmp As CanvasBitmap, Optional lower As Integer = 128, Optional upper As Integer = 129) As Integer(,)
        Dim ResultArr(bmp.Bounds.Width - 1, bmp.Bounds.Height - 1) As Integer
        Dim RawColors() As Color = bmp.GetPixelColors

        For i = 0 To bmp.Bounds.Width - 1
            For j = 0 To bmp.Bounds.Height - 1
                Dim temp As Integer = GetColorAverage(RawColors(bmp.Bounds.Width * j + i))
                ResultArr(i, j) = If(temp >= lower AndAlso temp <= upper, 1, 0)
            Next
        Next
        Return ResultArr
    End Function
End Class
