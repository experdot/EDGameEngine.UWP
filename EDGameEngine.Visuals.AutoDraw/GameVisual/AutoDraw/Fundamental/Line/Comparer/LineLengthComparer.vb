Imports EDGameEngine.Visuals
''' <summary>
''' 线条长度比较器
''' </summary>
Public Class LineLengthComparer
    Implements IComparer(Of ILine)

    Public Function Compare(x As ILine, y As ILine) As Integer Implements IComparer(Of ILine).Compare
        If x.Points.Count > y.Points.Count Then
            Return -1
        Else
            Return 1
        End If
    End Function
End Class
