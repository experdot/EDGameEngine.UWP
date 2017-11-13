Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 表示一个拥有加速度、速度和位置矢量的动态粒子
''' </summary>
Public Class DynamicParticle
    Implements IParticle

    Public Property Location As Vector2 Implements IParticle.Location
    Public Property Size As Single = 1 Implements IParticle.Size
    Public Property Color As Color Implements IParticle.Color
    Public Property IsDead As Boolean Implements IParticle.IsDead
    Public Property Age As Single Implements IParticle.Age
    ''' <summary>
    ''' 速度
    ''' </summary>
    Public Property Velocity As Vector2 '速度
    ''' <summary>
    ''' 加速度
    ''' </summary>
    Public Property Acceleration As Vector2 '加速度
    ''' <summary>
    ''' 质量
    ''' </summary>
    Public Property Mass As Single = 10.0 '质量大小
    ''' <summary>
    ''' Sprite缩放
    ''' </summary>
    Public Property ImageScale As Single = 1.0F
    ''' <summary>
    ''' 速度上限
    ''' </summary>
    Public Property VelocityUpon As Single = 5.0F
    ''' <summary>
    ''' 随机数发生器
    ''' </summary>
    Public Shared Rnd As New Random

    ''' <summary>
    ''' 初始化一个粒子
    ''' </summary>
    Public Sub New(loc As Vector2)
        Location = loc
        Velocity = New Vector2(0, 0)
        Acceleration = New Vector2(0, 0)
    End Sub
    ''' <summary>
    ''' 指定的力作用于当前对象
    ''' </summary>
    Public Sub ApplyForce(forceVec As Vector2)
        Acceleration = Acceleration + forceVec / Mass
    End Sub
    ''' <summary>
    ''' 更新粒子位置，重绘每帧图像前调用该方法
    ''' </summary>
    Public Sub Move()
        Velocity += Acceleration '更新速度
        Velocity.LimitMag(VelocityUpon) '粒子限速
        Location += Velocity '更新位置
        Acceleration = Vector2.Zero
    End Sub
    ''' <summary>
    ''' 移动至新的位置
    ''' </summary>
    Public Sub MoveTo(Loc As Vector2)
        Location = Loc
        Velocity.SetMag(0)
    End Sub

End Class
