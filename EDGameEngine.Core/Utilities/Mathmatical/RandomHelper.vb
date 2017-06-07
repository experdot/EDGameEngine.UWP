''' <summary>
''' 随机帮助类
''' </summary>
Public Class RandomHelper
    Private Shared Rnd As New Random

    ''' <summary>
    ''' 返回正态分布的随机数
    ''' </summary>
    Public Shared Function NextNorm(min As Integer, max As Integer, Optional count As Integer = 3) As Integer
        Dim temp As Long = 0
        For i = 0 To count - 1
            temp += Rnd.Next(min, max + 1)
        Next
        Return CInt(temp / count)
    End Function

End Class
