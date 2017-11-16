Imports System.Numerics
''' <summary>
''' 花朵
''' </summary>
Public Class Flower
    ''' <summary>
    ''' 父节点
    ''' </summary>
    Public Property Parent As TreeNode
    ''' <summary>
    ''' 加速度
    ''' </summary>
    Public Property Acceleration As Vector2
    ''' <summary>
    ''' 速度
    ''' </summary>
    Public Property Velocity As Vector2
    ''' <summary>
    ''' 相对位置
    ''' </summary>
    Public Property Location As Vector2
    ''' <summary>
    ''' 旋转
    ''' </summary>
    Public Property Rotation As Single
    ''' <summary>
    ''' 不透明度
    ''' </summary>
    Public Property Opacity As Single = 1.0F
    ''' <summary>
    ''' 绝对位置
    ''' </summary>
    Public ReadOnly Property RealLocation As Vector2
        Get
            If IsDrop Then
                Return DropLocation + Location
            Else
                Return Parent.RealLocation + Location
            End If
        End Get
    End Property
    ''' <summary>
    ''' 大小
    ''' </summary>
    Public Property Size As Single
    ''' <summary>
    ''' 是否脱落
    ''' </summary>
    Public Property IsDrop As Boolean
    ''' <summary>
    ''' 脱落时位置
    ''' </summary>
    Public Property DropLocation As Vector2

    Public Shared Property Rnd As New Random

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        Me.Size = 0.3F + Rnd.NextDouble * 0.3F
        Me.Opacity = 0.6F + Rnd.NextDouble * 0.4F
        Me.Rotation = Math.PI * 2 * Rnd.NextDouble
        Me.Acceleration = New Vector2(-0.5F * Rnd.NextDouble, Rnd.NextDouble * 0.5F - 0.1F)
    End Sub
    ''' <summary>
    ''' 生长
    ''' </summary>
    Public Sub Grow()
        Size += 0.001F * Rnd.NextDouble
        If Size > 1.0F OrElse Rnd.NextDouble > 0.999F Then
            Drop()
        End If
        Fly()
    End Sub
    ''' <summary>
    ''' 脱落
    ''' </summary>
    Private Sub Drop()
        If Me.IsDrop = False Then
            Me.IsDrop = True
            Me.DropLocation = Me.Parent.RealLocation
        End If
    End Sub
    ''' <summary>
    ''' 飞舞
    ''' </summary>
    Private Sub Fly()
        If Me.IsDrop Then
            Velocity += Acceleration * Rnd.NextDouble
            Location += Velocity
            Rotation += Rnd.NextDouble * 0.001F
            Opacity = Math.Max(Opacity - 0.01F * Rnd.NextDouble, 0.0F)
            If RealLocation.X < 0 OrElse Opacity <= 0.0F Then
                Dead()
            End If
        End If
    End Sub
    ''' <summary>
    ''' 死亡
    ''' </summary>
    Private Sub Dead()
        Me.Parent.Flowers.Remove(Me)
        Me.Parent = Nothing
    End Sub
End Class
