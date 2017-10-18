Imports System.Numerics
''' <summary>
''' 平面变换
''' </summary>
Public Class Transform
    ''' <summary>
    ''' 返回一个参数默认的<see cref="Transform"/>对象
    ''' </summary>
    Public Shared ReadOnly Property Normal As Transform
        Get
            Return New Transform(Vector2.Zero, Vector2.One, 0, Vector2.Zero)
        End Get
    End Property
    ''' <summary>
    ''' 位移
    ''' </summary>
    Public Property Translation As Vector2
    ''' <summary>
    ''' 缩放
    ''' </summary>
    Public Property Scale As Vector2
    ''' <summary>
    ''' 旋转
    ''' </summary>
    Public Property Rotation As Single
    ''' <summary>
    ''' 旋转中心
    ''' </summary>
    Public Property Center As Vector2
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(position As Vector2, scale As Vector2, rotation As Single, center As Vector2)
        Me.Translation = position
        Me.Scale = scale
        Me.Rotation = rotation
        Me.Center = center
    End Sub
End Class