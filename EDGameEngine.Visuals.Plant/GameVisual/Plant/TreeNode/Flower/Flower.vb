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
    ''' <summary>
    ''' 种类
    ''' </summary>
    Public Property Kind As Integer
    ''' <summary>
    ''' 缩放
    ''' </summary>
    Public Property Scale As Vector2 = Vector2.One

    Public Property Trajectory As New List(Of Vector2)

    Public Shared Property Rnd As New Random

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        Me.Size = 0.3F + Rnd.NextDouble * 0.3F
        Me.Opacity = 0.6F + Rnd.NextDouble * 0.4F
        Me.Rotation = Math.PI * 2 * Rnd.NextDouble
        Me.Acceleration = New Vector2(-0.5F * Rnd.NextDouble, Rnd.NextDouble * 0.5F - 0.1F)
        Me.Kind = If(Rnd.NextDouble < 0.8, 0, 1)
        Me.Scale = New Vector2(0.8F + CSng(Math.Sin(Rotation) * 0.2F), CSng(0.8F + Math.Cos(Rotation) * 0.2F))
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
            Opacity = Math.Max(Opacity - 0.01F * Rnd.NextDouble, 0.0F)
            If RealLocation.Y >= 700 Then
                Location = New Vector2(RealLocation.X, 700) - DropLocation
            Else
                Acceleration += New Vector2(-0.01F, 0.008F) * Rnd.NextDouble
                Velocity += Acceleration * Rnd.NextDouble
                Location += Velocity
                Rotation += Rnd.NextDouble * 0.05F
                If Trajectory.Count = 0 Then
                    If Rnd.NextDouble > 0.995 Then
                        Trajectory.Add(RealLocation)
                    End If
                Else
                    If Trajectory.Count < 20 Then
                        Trajectory.Add(RealLocation)
                    End If
                End If
            End If
            Scale = New Vector2(0.8F + CSng(Math.Sin(Rotation) * 0.2F), CSng(0.8F + Math.Cos(Rotation) * 0.2F))
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
