Imports System.Numerics
Public Class Transform
    Public Shared ReadOnly Property Normal As Transform
        Get
            Return New Transform(Vector2.Zero, Vector2.One, 0, Vector2.Zero)
        End Get
    End Property
    ''' <summary>
    ''' 位置
    ''' </summary>
    ''' <returns></returns>
    Public Property Position As Vector2
    ''' <summary>
    ''' 缩放
    ''' </summary>
    ''' <returns></returns>
    Public Property Scale As Vector2
    ''' <summary>
    ''' 旋转
    ''' </summary>
    ''' <returns></returns>
    Public Property Rotation As Single
    ''' <summary>
    ''' 旋转中心
    ''' </summary>
    ''' <returns></returns>
    Public Property Center As Vector2
    Public Sub New(position As Vector2, scale As Vector2, rotation As Single, center As Vector2)
        Me.Position = position
        Me.Scale = scale
        Me.Rotation = rotation
        Me.Center = center
    End Sub
End Class