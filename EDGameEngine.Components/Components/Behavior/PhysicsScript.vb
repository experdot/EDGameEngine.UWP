Imports System.Numerics
Imports Box2D.UWP
Imports EDGameEngine.Core
''' <summary>
''' 物理仿真脚本
''' </summary>
Public Class PhysicsScript
    Inherits BehaviorBase
    Const TimeStep As Single = 1 / 100
    Const Iterations As Integer = 10
    Dim PhyAABB As AABB
    Dim PhyWorld As Box2D.UWP.World
    Dim BoxBody, BoxBody2, BoxBody3 As Body
    Dim Gravity As New Vector2(0, 10)
    Public target1, target2, target3 As IGameBody
    Public Overrides Sub Start()
        '创建世界实例
        PhyWorld = New Box2D.UWP.World(Gravity, False)
        '开启连续物理测试
        PhyWorld.ContinuousPhysics = False

        '创建物体
        BoxBody = PhyWorld.CreateBody(New BodyDef With {.position = New Vector2(45, 40)})

        BoxBody2 = PhyWorld.CreateBody(New BodyDef With {.position = New Vector2(20, 35)})


        Dim shape1 As New PolygonShape
        shape1.SetAsBox(0.5, 0.5)

        Dim bodyFixture As Fixture = BoxBody.CreateFixture(New FixtureDef With {.shape = shape1})

        BoxBody.SetMassData(New MassData() With {.mass = 10})
        BoxBody.SetAngularVelocity(2)
        'BoxBody.SetLinearVelocity(New Vector2(5, 0))
        Dim shape2 As New PolygonShape
        shape2.SetAsBox(0.5, 0.5)
        Dim bodyFixture2 As Fixture = BoxBody2.CreateFixture(New FixtureDef With {.shape = shape2})

        BoxBody2.SetMassData(New MassData() With {.mass = 10})

        Debug.WriteLine(bodyFixture2.GetRestitution())
        BoxBody3 = PhyWorld.CreateBody(New BodyDef With {.position = New Vector2(0, 400)})
        Dim shape3 As New PolygonShape
        shape3.SetAsBox(100, 1)
        BoxBody3.CreateFixture(New FixtureDef With {.shape = shape3})
        BoxBody3.SetMassData(New MassData() With {.mass = 0})

        'BoxBody2.SetXForm(New Vector2(20, 60), Math.PI / 4)
    End Sub
    Public Overrides Sub Update()
        PhyWorld.Step(TimeStep, 10, 10)
        Debug.WriteLine(BoxBody.GetPosition.Y & "," & BoxBody.GetLinearVelocity.Y & "," & BoxBody3.GetPosition.Y)
        'Target.Transform.Translation = New Vector2(BoxBody.GetPosition.X, BoxBody.GetPosition.Y)
        'Target.Transform.Rotation = BoxBody.GetAngle

        target1.Transform.Translation = New Vector2(BoxBody.GetPosition.X, BoxBody.GetPosition.Y)
        target1.Transform.Rotation = BoxBody.GetAngle
        target2.Transform.Translation = New Vector2(BoxBody2.GetPosition.X, BoxBody2.GetPosition.Y)
        target2.Transform.Rotation = BoxBody2.GetAngle

        target3.Transform.Translation = New Vector2(BoxBody3.GetPosition.X, BoxBody3.GetPosition.Y)
        target3.Transform.Rotation = BoxBody3.GetAngle
    End Sub
End Class
