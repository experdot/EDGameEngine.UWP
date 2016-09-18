Imports Windows.UI
Public Class ColorEx
    Public Shared Function FromScRgb(r!, g!, b!) As Color
        Return Color.FromArgb(255, CByte(255 * r), CByte(255 * g), CByte(255 * b))
    End Function
End Class
