Imports System.Numerics
''' <summary>
''' 表示由一系列点向量组成的线条
''' </summary>
Public Class PointSequence
    Public Property Points As New List(Of Vector2)
    Public Property Sizes As Single()
    ''' <summary>
    ''' 计算画笔大小
    ''' </summary>
    Public Sub CalcSize()
        If Points.Count < 1 Then Exit Sub
        Static Mid, PenSize As Single
        ReDim Sizes(Points.Count - 1)
        For i = 0 To Points.Count - 1
            Mid = CSng(Math.Abs(i - Points.Count / 2))
            PenSize = 1 '- Mid / Points.Count * 2
            Sizes(i) = PenSize
        Next
    End Sub
End Class
