Imports Windows.UI
Public Class ColorEx
    Public Shared Function FromScRgb(r!, g!, b!) As Color
        Return Color.FromArgb(255, 255 * r, 255 * g, 255 * b)
    End Function
End Class
