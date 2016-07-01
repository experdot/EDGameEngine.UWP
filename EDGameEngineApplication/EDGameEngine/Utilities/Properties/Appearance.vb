Imports System.Numerics
Public Class Appearance
    Public Shared ReadOnly Normal As New Appearance(True, 1.0F)
    Public Visible As Boolean
    Public Opcacity As Single
    Public Sub New(visible As Boolean, opcacity As Single)
        Me.Visible = visible
        Me.Opcacity = opcacity
    End Sub
End Class
