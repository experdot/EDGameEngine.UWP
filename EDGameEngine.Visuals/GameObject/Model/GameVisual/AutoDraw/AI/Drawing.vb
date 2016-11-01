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

    Public Sub New(pixels As PixelData, split As Integer)
        Dim temp As Integer = CInt(255 / split)
        For i = 0 To split - 1
            Dim tempAI = New SequenceAI(GetImageBolLimit(pixels.Colors, pixels.Width, pixels.Height, CInt(i * temp - temp / 2), CInt(i * temp + temp / 2)))
            Lines.AddRange(tempAI.Lines)
        Next
        MatchLineColor(pixels)
    End Sub
    ''' <summary>
    ''' 降噪
    ''' </summary>
    Public Sub Denoising(Optional count As Integer = 10)
        Dim actions As New List(Of Action)
        For Each SubSeq In Lines
            If SubSeq.Points.Count < count Then
                actions.Add(Sub()
                                Lines.Remove(SubSeq)
                            End Sub)
            End If
        Next
        For Each SubAct In actions
            SubAct.Invoke
        Next
        actions.Clear()
    End Sub
    ''' <summary>
    ''' 配色
    ''' </summary>
    Public Sub MatchLineColor(pixels As PixelData)
        For Each SubLine In Lines
            For Each SubPoint In SubLine.Points
                SubPoint.Color = pixels.Colors(CInt(SubPoint.Position.Y) * pixels.Width + CInt(SubPoint.Position.X))
            Next
        Next
    End Sub
    ''' <summary>
    ''' 匹配大小
    ''' </summary>
    Public Sub MatchLineSize()
        For Each SubLine In Lines
            SubLine.CalcSize()
        Next
    End Sub
    ''' <summary>
    ''' 匹配平均色
    ''' </summary>
    Public Sub MatchAverageColor()
        For Each SubLine In Lines
            SubLine.CalcAverageColor()
        Next
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
