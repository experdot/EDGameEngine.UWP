Imports System.Numerics
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories
Imports Windows.System
''' <summary>
''' [Experimental]世界
''' </summary>
Public Class GameWorld
    ''' <summary>
    ''' 活动的关卡
    ''' </summary>
    Public Property Mission As IMission
    ''' <summary>
    ''' 物理世界
    ''' </summary>
    Public Property PhysicWorld As World
    ''' <summary>
    ''' [Disabled]物理世界更新帧长
    ''' </summary>
    Public Property TimeStep As Single = 1 / 50.0F
    ''' <summary>
    ''' 物理世界重力
    ''' </summary>
    Public Property Gravity As Vector2 = New Vector2(0, 10)
    ''' <summary>
    ''' AI管理器
    ''' </summary>
    Public Property AIManager As AIManager

    Public Sub New()
        AIManager = New AIManager
        AIManager.AIConrollers.Add(New RandomMoveAI)
    End Sub
    ''' <summary>
    ''' 开始
    ''' </summary>
    Public Sub Start()
        PhysicWorld = New World(Gravity)
        For Each SubBlock In Mission.Blocks
            If SubBlock IsNot Nothing Then
                CreateRectangle(PhysicWorld, SubBlock, SubBlock.Location, BodyType.Static)
            End If
        Next
        For Each SubCharacter In Mission.Characters
            If SubCharacter IsNot Nothing Then
                CreateRectangle(PhysicWorld, SubCharacter, SubCharacter.Location, BodyType.Dynamic)
            End If
        Next
        Mission.Start()
        AIManager.Start(Mission)
    End Sub
    ''' <summary>
    ''' 更新
    ''' </summary>
    Public Sub Update()
        Static Last As Date = Date.Now
        Dim steping As Single = Date.Now.Subtract(Last).TotalMilliseconds / 1000
        PhysicWorld.Step(steping)
        Last = Date.Now

        Mission.Update()
        AIManager.Update(Mission)
    End Sub
    ''' <summary>
    ''' [Experimental]按键测试
    ''' </summary>
    Public Sub KeyDown(key As Char)
        Dim player As ICharacter = Mission.Characters.First
        Dim body As Body = player.Collide.Body
        Select Case key
            Case "W"
                player.AbilityManager.FindAbilityByName("Jump").Release(player)
            Case "S"
                'player.AbilityManager.FindAbilityByName("Jump").Release(player)
            Case "A"
                player.AbilityManager.FindAbilityByName("MoveLeft").Release(player)
            Case "D"
                player.AbilityManager.FindAbilityByName("MoveRight").Release(player)
        End Select
        If body.LinearVelocity.X > 2 Then
            body.LinearVelocity = New Vector2(2, body.LinearVelocity.Y)
        End If
        If body.LinearVelocity.X < -2 Then
            body.LinearVelocity = New Vector2(-2, body.LinearVelocity.Y)
        End If
        If body.LinearVelocity.Y < -10 Then
            body.LinearVelocity = New Vector2(body.LinearVelocity.X, -10)
        End If

    End Sub

    ''' <summary>
    ''' 创建矩形物理实体
    ''' </summary>
    Private Sub CreateRectangle(world As World, target As ICollision, loc As Vector2, t As BodyType, Optional r As Single = 0)
        Dim w As Single = target.Collide.Rect.Width
        Dim h As Single = target.Collide.Rect.Height
        target.Collide.Body = BodyFactory.CreateRectangle(world, w, h, 1.0F, loc, r, t)
        target.Collide.Body.Friction = 0.5F
        target.Collide.Body.SleepingAllowed = False
    End Sub

End Class
