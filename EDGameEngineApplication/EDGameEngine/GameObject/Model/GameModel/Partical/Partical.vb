Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 粒子类，表示一个拥有加速度、加速度和位置矢量的抽象粒子
''' </summary>
Public Class Partical
    Public Property Location As Vector2 '位置矢量
    Public Property Velocity As Vector2 '速度
    Public Property Acceleration As Vector2 '加速度
    Public Property Mass As Single = 10.0 '质量大小
    Public Property Age As Single = 0 '生命周期
    Public Property Alpha As Single = 255
    Public Property Size As Single = 1
    Public Property Radius As Single = 0
    Public Property RadiusX As Single = 0
    Public Property RadiusY As Single = 0
    Public Property ImageSize As Single = 1 '粒子图像的大小
    Public Property Color As Color '粒子颜色
    Public Property Parent As Partical '环绕父粒子
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
    ''' <param name="forceVec">指定的力</param>
    Public Sub ApplyForce(forceVec As Vector2)
        Acceleration = Acceleration + forceVec / Mass
    End Sub
    ''' <summary>
    ''' 更新粒子位置，重绘每帧图像前调用该方法
    ''' </summary>
    Public Sub Move()
        Velocity += Acceleration '更新速度
        Velocity.LimitMag(10) '粒子限速
        Location += Velocity '更新位置
        Acceleration = Vector2.Zero
    End Sub
    Public Sub StartNew(Loc As Vector2)
        Location = Loc
        Velocity.SetMag(0)
    End Sub
End Class
