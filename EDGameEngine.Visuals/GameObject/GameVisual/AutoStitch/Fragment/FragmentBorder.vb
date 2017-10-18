''' <summary>
''' 碎片边沿
''' </summary>
Public Class FragmentBorder
    ''' <summary>
    ''' 侧边集合
    ''' </summary>
    Public Property Borders As FragmentSide()

    ''' <summary>
    ''' 由指定的碎片阈值信息初始化一个碎片边沿对象
    ''' </summary>
    Public Sub New(threshold As Integer(,))
        LoadFromThreshold(threshold, threshold.GetUpperBound(0) + 1, threshold.GetUpperBound(1) + 1)
    End Sub
    ''' <summary>
    ''' 加载边界二值信息
    ''' </summary>
    Private Sub LoadFromThreshold(imagepoint As Integer(,), w As Integer, h As Integer)
        ReDim Borders(3)
        Dim indexarr As Integer() = {w, w, h, h}
        Dim xArr As Integer() = {-1, -1, 0, w - 1}
        Dim yArr As Integer() = {0, h - 1, -1, -1}
        '上、下、左和右边界
        For j = 0 To 3
            Borders(j) = New FragmentSide(indexarr(j))
            For i = 0 To indexarr(j) - 1
                Borders(j).Threshold(i) = imagepoint(If(xArr(j) = -1, i, xArr(j)), If(yArr(j) = -1, i, yArr(j)))
                If Borders(j).Threshold(i) = 1 Then Borders(j).IsBlank = False
            Next
        Next
    End Sub
End Class
