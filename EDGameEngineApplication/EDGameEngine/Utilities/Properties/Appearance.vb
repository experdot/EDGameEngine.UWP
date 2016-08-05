Imports System.Numerics
''' <summary>
''' 外观
''' </summary>
Public Class Appearance
    Public Shared ReadOnly Property Normal As Appearance
        Get
            Return New Appearance(True, 1.0F)
        End Get
    End Property
    ''' <summary>
    ''' 可见性
    ''' </summary>
    ''' <returns></returns>
    Public Property Visible As Boolean
    ''' <summary>
    ''' 不透明度
    ''' </summary>
    ''' <returns></returns>
    Public Property Opcacity As Single
    Public Sub New(visible As Boolean, opcacity As Single)
        Me.Visible = visible
        Me.Opcacity = opcacity
    End Sub
End Class
