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
    Public Shared Function GetColorAverage(ByVal col As Color)
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
                xoff = buffer1(k - 1) - buffer1(k + 1) 'x方向的偏移
                yoff = buffer1(k - width) - buffer1(k + width) 'y方向的偏移
                If (xoff = 0 AndAlso yoff = 0) OrElse i + yoff <= 0 OrElse i + yoff >= height OrElse j + xoff <= 0 OrElse j + xoff >= width Then
                    k += 1 '这个是边界判断，用Orelse提高判断效率
                    Continue For
                End If
                Dim pos1, pos2 As Integer
                pos1 = (i + yoff) * width + j + xoff
                pos2 = i * width + j
                DesData(pos2) = SrcData(pos1) '然后就是根据偏移量重新渲染界面
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
    Private Function GetImageBol(ByVal bmp As CanvasBitmap) As Integer(,)
        Dim ResultArr(bmp.Bounds.Width - 1, bmp.Bounds.Height - 1) As Integer
        Dim RawColors() As Color = bmp.GetPixelColors
        For i = 0 To bmp.Bounds.Width - 1
            For j = 0 To bmp.Bounds.Height - 1
                ResultArr(i, j) = If(RawColors(bmp.Bounds.Height * j + i).Equals(Colors.White), 0, 1)
            Next
        Next
        Return ResultArr
    End Function
    ''' <summary>
    ''' 检查一个点是否被包围
    ''' </summary>
    ''' <param name="BolArr"></param>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns></returns>
    Private Function CheckPointAround(BolArr As Integer(,), ByVal x As Integer, ByVal y As Integer) As Boolean
        If Not (x > 0 And y > 0 And x < BolArr.GetUpperBound(0) And y < BolArr.GetUpperBound(1)) Then Return True
        If BolArr(x - 1, y) = 1 And BolArr(x + 1, y) = 1 And BolArr(x, y - 1) = 1 And BolArr(x, y + 1) = 1 Then
            Return True '当前点为实体内部
        Else
            Return False '当前点为实体边缘
        End If
    End Function
End Class
