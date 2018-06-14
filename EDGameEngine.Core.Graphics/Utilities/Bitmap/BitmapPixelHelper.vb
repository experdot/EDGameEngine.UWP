Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 位图像素辅助类
''' </summary>
Public Class BitmapPixelHelper
    ''' <summary> 
    ''' 由指定阈值判断两个RGB颜色是否相近
    ''' </summary> 
    Public Shared Function GetColorSimilar(ByVal Color1 As Color, ByVal Color2 As Color, ByVal distance As Single) As Boolean
        Dim r As Integer = CInt(Color1.R) - CInt(Color2.R)
        Dim g As Integer = CInt(Color1.G) - CInt(Color2.G)
        Dim b As Integer = CInt(Color1.B) - CInt(Color2.B)
        Dim absDis As Integer = CInt(Math.Sqrt(r * r + g * g + b * b))
        Return If(absDis < distance, True, False)
    End Function
    ''' <summary> 
    ''' 返回指定颜色的RGB平均值
    ''' </summary> 
    Public Shared Function GetColorAverage(ByVal col As Color) As Integer
        Return CInt((CInt(col.R) + CInt(col.G) + CInt(col.B)) / 3)
    End Function
    ''' <summary>
    ''' 返回指定位图的颜色数组
    ''' </summary>
    Public Shared Function GetColorArr(ByRef bmp As CanvasBitmap) As Color(,)
        Dim RawArr = bmp.GetPixelColors()
        Dim TempArr(CInt(bmp.Bounds.Width) - 1, CInt(bmp.Bounds.Height) - 1) As Color
        For i As Integer = 0 To CInt(bmp.Bounds.Width - 1)
            For j As Integer = 0 To CInt(bmp.Bounds.Height - 1)
                TempArr(i, j) = RawArr(CInt(bmp.Bounds.Width * j + i))
            Next
        Next
        Return TempArr
    End Function
    ''' <summary>
    ''' 返回指定图位图的二值化原始像素图像
    ''' </summary>
    Public Shared Function GetThresholdImageRaw(session As CanvasDrawingSession, bmp As CanvasBitmap, split As Single) As CanvasBitmap
        Dim RawColors() As Color = bmp.GetPixelColors
        Dim NowColors(RawColors.Count - 1) As Color
        For i = 0 To RawColors.Count - 1
            Dim temp = GetColorAverage(RawColors(i))
            NowColors(i) = If(temp >= split - 0.5 AndAlso temp < split + 0.5, RawColors(i), Color.FromArgb(0, 0, 0, 0))
        Next
        Return CanvasBitmap.CreateFromColors(session, NowColors, CInt(bmp.Bounds.Width), CInt(bmp.Bounds.Height))
    End Function

    ''' <summary>
    ''' 返回指定位图的二值化数组
    ''' </summary>
    Public Shared Function GetImageBol(ByVal bmp As CanvasBitmap, Optional split As Integer = 128) As Integer(,)
        Dim ResultArr(CInt(bmp.Bounds.Width) - 1, CInt(bmp.Bounds.Height) - 1) As Integer
        Dim RawColors() As Color = bmp.GetPixelColors
        For i = 0 To CInt(bmp.Bounds.Width) - 1
            For j = 0 To CInt(bmp.Bounds.Height) - 1
                ResultArr(i, j) = If(GetColorAverage(RawColors(CInt(bmp.Bounds.Width * j + i))) < split, 1, 0)
            Next
        Next
        Return ResultArr
    End Function
    ''' <summary>
    ''' 返回指定位图的二值化特定范围数组
    ''' </summary>
    Public Shared Function GetImageBolLimit(ByVal bmp As CanvasBitmap, Optional lower As Integer = 128, Optional upper As Integer = 129) As Integer(,)
        Dim ResultArr(CInt(bmp.Bounds.Width) - 1, CInt(bmp.Bounds.Height) - 1) As Integer
        Dim RawColors() As Color = bmp.GetPixelColors
        For i = 0 To CInt(bmp.Bounds.Width) - 1
            For j = 0 To CInt(bmp.Bounds.Height) - 1
                Dim temp As Integer = GetColorAverage(RawColors(CInt(bmp.Bounds.Width * j + i)))
                ResultArr(i, j) = If(temp >= lower AndAlso temp <= upper, 1, 0)
            Next
        Next
        Return ResultArr
    End Function
End Class
