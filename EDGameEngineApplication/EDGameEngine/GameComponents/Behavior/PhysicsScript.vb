Imports System.Numerics
Imports Box2D
''' <summary>
''' 物理仿真脚本
''' </summary>
Public Class PhysicsScript
    Inherits BehaviorBase
    Const TimeStep As Single = 1 / 60
    Const Iterations As Integer = 10
    Dim PhyAABB As AABB
    Dim PhyWorld As Box2D.World
    Dim GroundBody As Body
    Dim Gravity As New Vector2(0, -10)
    Public Overrides Sub Start()
        PhyAABB.lowerBound = New Vector2(-100, -100)
        PhyAABB.upperBound = New Vector2(100, 100)
        '创建世界实例
        PhyWorld = New Box2D.World(Gravity, True)
        '开启连续物理测试
        PhyWorld.ContinuousPhysics = True
        '创建地面定义
        Dim groundBodyDef As New BodyDef With {.position = New Vector2(0, 0)}
        GroundBody = PhyWorld.CreateBody(groundBodyDef)
        '创建形状
        Dim shape1 As New PolygonShape
        shape1.SetAsBox(1, 1)
        '创建夹具定义
        Dim groundFixtureDef As New FixtureDef
        groundFixtureDef.shape = shape1
        '绑定夹具
        Dim groundFixture As Fixture = GroundBody.CreateFixture(groundFixtureDef)
        GroundBody.SetMassFromShapes()
    End Sub
    Public Overrides Sub Update()
        PhyWorld.Step(TimeStep, Iterations, Iterations)
        Target.Transform.Translation = GroundBody.Position
    End Sub
End Class
