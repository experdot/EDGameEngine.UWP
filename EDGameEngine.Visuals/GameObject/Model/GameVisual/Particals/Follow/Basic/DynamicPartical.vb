Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 表示一个拥有加速度、速度和位置矢量的粒子
''' </summary>
Public Class DynamicPartical
    Implements IPartical
    Public Property Location As Vector2 Implements IPartical.Location
    Public Property Size As Single = 1 Implements IPartical.Size
    Public Property Color As Color Implements IPartical.Color

    Public Property Velocity As Vector2 '速度
    Public Property Acceleration As Vector2 '加速度
    Public Property Mass As Single = 10.0 '质量大小

    Public Property Age As Single = 0 '生命周期
    Public Property ImageSize As Single

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
        Velocity.LimitMag(5) '粒子限速
        Location += Velocity '更新位置
        Acceleration = Vector2.Zero
    End Sub
    ''' <summary>
    ''' 移动至新的位置
    ''' </summary>
    ''' <param name="Loc"></param>
    Public Sub MoveTo(Loc As Vector2)
        Location = Loc
        Velocity.SetMag(0)
    End Sub
End Class
