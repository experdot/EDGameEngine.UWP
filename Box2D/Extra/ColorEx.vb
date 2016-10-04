Public Class ColorEx
    Public Shared Function FromScRgb(ByVal r As Single, ByVal g As Single, ByVal b As Single) As Color
        Return FromScRgb(1.0!, r, g, b)
    End Function
    Public Shared Function FromScRgb(ByVal a As Single, ByVal r As Single, ByVal g As Single, ByVal b As Single) As Color
        Dim color As New Color
        If (a < 0!) Then
            a = 0!
        ElseIf (a > 1.0!) Then
            a = 1.0!
        End If
        color.A = (CType(((a * 255.0!) + 0.5!), Byte))
        color.R = (ScRgbTosRgb(r))
        color.G = (ScRgbTosRgb(g))
        color.B = (ScRgbTosRgb(b))
        Return color
    End Function
    Friend Shared Function ScRgbTosRgb(ByVal val As Single) As Byte
        If (val <= 0) Then
            Return 0
        End If
        If (val <= 0.0031308) Then
            Return CType((((255.0! * val) * 12.92!) + 0.5!), Byte)
        End If
        If (val < 1) Then
            Return CType(((255.0! * ((1.055! * CType(Math.Pow(CType(val, Double), 0.41666666666666669), Single)) - 0.055!)) + 0.5!), Byte)
        End If
        Return &HFF
    End Function
End Class
