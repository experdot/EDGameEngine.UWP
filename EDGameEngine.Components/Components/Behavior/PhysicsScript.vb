Imports System.Numerics
Imports Box2D.UWP
Imports EDGameEngine.Core
''' <summary>
''' 物理仿真脚本
''' </summary>
Public Class PhysicsScript
    Inherits BehaviorBase
    Const TimeStep As Single = 1 / 60
    Const Iterations As Integer = 10
    Dim PhyAABB As AABB
    Dim PhyWorld As Box2D.UWP.World
    Dim BoxBody, GroundBody As Body
    Dim Gravity As New Vector2(0, -10)
    Public Overrides Sub Start()
        '创建世界实例
        PhyWorld = New Box2D.UWP.World(Gravity, True)
        '开启连续物理测试
        PhyWorld.ContinuousPhysics = False
        '创建物体
        BoxBody = PhyWorld.CreateBody(New BodyDef With {.position = New Vector2(0, 0)})
        '创建形状
        Dim shape1 As New PolygonShape
        shape1.SetAsBox(1, 1)

        '创建夹具定义
        Dim bodyFixtureDef As New FixtureDef With {.shape = shape1}
        '绑定夹具
        Dim bodyFixture As Fixture = BoxBody.CreateFixture(bodyFixtureDef)
        bodyFixture.SetDensity(2)
        BoxBody.SetMassFromShapes()
        Debug.WriteLine(BoxBody.GetMassData.mass)
        'BoxBody.SetMassData(New MassData() With {.Mass = 10})

        GroundBody = PhyWorld.CreateBody(New BodyDef With {.position = New Vector2(0, -10)})
        Dim shape2 As New PolygonShape
        shape2.SetAsBox(50, 10)
        bodyFixtureDef.shape = shape2
        GroundBody.CreateFixture(bodyFixtureDef)
        GroundBody.SetMassData(New MassData() With {.mass = 10})

    End Sub
    Public Overrides Sub Update()
        PhyWorld.Step(TimeStep, Iterations, Iterations)
        Debug.WriteLine(BoxBody.GetPosition.Y & "," & BoxBody.GetLinearVelocity.Y & "," & GroundBody.GetPosition.Y)
        Target.Transform.Translation = New Vector2(BoxBody.GetPosition.X, -BoxBody.GetPosition.Y)
        Target.Transform.Rotation = BoxBody.GetAngle
    End Sub
End Class
