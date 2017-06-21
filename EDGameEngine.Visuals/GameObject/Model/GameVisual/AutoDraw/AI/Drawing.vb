Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 线条画
''' </summary>
Public Class Drawing
    ''' <summary>
    ''' 线条集合
    ''' </summary>
    Public Property Lines As New List(Of Line)
    ''' <summary>
    ''' 画笔大小
    ''' </summary>
    Public Property PenSize As Single = 1
    ''' <summary>
    ''' 画笔颜色Alpha通道
    ''' </summary>
    Public Property PenAlpha As Integer = 255
    ''' <summary>
    ''' 层索引
    ''' </summary>
    Public Property LayerIndex As Integer = 0
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(pixels As PixelData, split As Integer, index As Integer, Optional mode As ScanMode = ScanMode.Rect)
        LayerIndex = index
        Dim temp As Integer = CInt(256 / split)
        For i = 0 To split
            Using tempAI = New SequenceAI(GetImageBolLimit(pixels.Colors, pixels.Width, pixels.Height, CInt(i * temp - temp / 2), CInt(i * temp + temp / 2)), mode)
                Lines.AddRange(tempAI.Lines)
            End Using
        Next
        UpdateLinesColor(pixels)
        UpdateLinesIndexAndLocation()
    End Sub
    ''' <summary>
    ''' 返回指定位置附近的线条集合
    ''' </summary>
    Public Function GetLinesByLocation(loc As Vector2, distance As Single) As List(Of Line)
        Dim result As New List(Of Line)
        result.AddRange(Lines.Where(Function(line As Line)
                                        Return line.IsNear(loc, distance)
                                    End Function))
        result.ForEach(Sub(line As Line)
                           Lines.Remove(line)
                       End Sub)
        Return result
    End Function
    ''' <summary>
    ''' 降噪
    ''' </summary>
    Public Sub Denoising(Optional amount As Integer = 10)
        Lines.RemoveAll(Function(line As Line)
                            Return line.Points.Count <= amount
                        End Function)
    End Sub

    ''' <summary>
    ''' 更新线条大小
    ''' </summary>
    Public Sub UpdatePointsSizeOfLine()
        For Each SubLine In Lines
            SubLine.CalcSize()
        Next
    End Sub
    ''' <summary>
    ''' 更新线条颜色
    ''' </summary>
    Public Sub UpdatePointsColorOfLine()
        For Each SubLine In Lines
            SubLine.CalcAverageColor()
        Next
    End Sub


    ''' <summary>
    ''' 更新配色
    ''' </summary>
    Private Sub UpdateLinesColor(pixels As PixelData)
        For Each SubLine In Lines
            For Each SubPoint In SubLine.Points
                SubPoint.Color = pixels.Colors(CInt(SubPoint.Position.Y) * pixels.Width + CInt(SubPoint.Position.X))
            Next
        Next
    End Sub
    ''' <summary>
    ''' 更新绘制点的层索引与位置
    ''' </summary>
    Private Sub UpdateLinesIndexAndLocation()
        For Each SubLine In Lines
            SubLine.UpdateLayerIndex(Me.LayerIndex)
            SubLine.CalcLocation()
        Next
        Lines.RemoveAll(Function(line)
                            Return line.Points.Count = 0
                        End Function)
    End Sub

    ''' <summary>
    ''' 返回指定颜色数组的二值化特定范围数组
    ''' </summary>
    Private Shared Function GetImageBolLimit(rawColors As Color(), w As Integer, h As Integer, Optional lower As Integer = 128, Optional upper As Integer = 129) As Integer(,)
        Dim ResultArr(w - 1, h - 1) As Integer
        For i = 0 To w - 1
            For j = 0 To h - 1
                Dim temp As Integer = GetColorAverage(rawColors(CInt(w * j + i)))
                ResultArr(i, j) = If(temp >= lower AndAlso temp <= upper, 1, 0)
            Next
        Next
        Return ResultArr
    End Function
    ''' <summary> 
    ''' 返回指定颜色的RGB平均值
    ''' </summary> 
    Private Shared Function GetColorAverage(col As Color) As Integer
        Return CInt((CInt(col.R) + CInt(col.G) + CInt(col.B)) / 3)
    End Function
End Class
