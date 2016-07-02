Imports System.Numerics
Public Class Appearance
    Public Shared ReadOnly Property Normal As Appearance
        Get
            Return New Appearance(True, 1.0F)
        End Get
    End Property
    Public Visible As Boolean
    Public Opcacity As Single
    Public Sub New(visible As Boolean, opcacity As Single)
        Me.Visible = visible
        Me.Opcacity = opcacity
    End Sub
End Class
