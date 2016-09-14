Imports System.Numerics
Imports Box2D
Imports EDGameEngine.Core
''' <summary>
''' 物理仿真脚本
''' </summary>
Public Class PhysicsScript
    Inherits BehaviorBase
    Const TimeStep As Single = 1 / 60
    Const Iterations As Integer = 10
    Dim PhyAABB As AABB
    Dim PhyWorld As Box2D.World
    Dim BoxBody, GroundBody As Body
    Dim Gravity As New Vector2(0, -10)
    Public Overrides Sub Start()
        '创建世界实例
        PhyWorld = New Box2D.World(Gravity, True)
        '开启连续物理测试
        'PhyWorld.ContinuousPhysics = True
        '创建物体
        BoxBody = PhyWorld.CreateBody(New BodyDef With {.position = New Vector2(0, 0)})
        '创建形状
        Dim shape1 As New PolygonShape
        shape1.SetAsBox(1, 1)
        '创建夹具定义
        Dim bodyFixtureDef As New FixtureDef With {.shape = shape1}
        '绑定夹具
        Dim groundFixture As Fixture = BoxBody.CreateFixture(bodyFixtureDef)
        BoxBody.SetMassData(New MassData() With {.mass = 10})
        BoxBody.LinearVelocity = New Vector2(10, 0)
        BoxBody.AngularVelocity = 10

        GroundBody = PhyWorld.CreateBody(New BodyDef With {.position = New Vector2(0, -50)})
        shape1.SetAsBox(200, 20)
        bodyFixtureDef.shape = shape1
        GroundBody.CreateFixture(bodyFixtureDef)
        GroundBody.SetMassData(New MassData() With {.mass = 0})

    End Sub
    Public Overrides Sub Update()
        PhyWorld.Step(TimeStep, Iterations, Iterations)
        'Debug.WriteLine(BoxBody.LinearVelocity.Y)
        Target.Transform.Translation = New Vector2(BoxBody.Position.X, -BoxBody.Position.Y)
        'Target.Transform.Rotation = BoxBody.Angle
    End Sub
End Class
