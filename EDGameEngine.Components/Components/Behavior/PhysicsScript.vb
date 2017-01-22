Imports EDGameEngine.Core
Imports FarseerPhysics.Collision
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories
Imports FarseerPhysics.Collision.Shapes
Imports Microsoft.Xna.Framework
Imports Windows.UI
''' <summary>
''' 物理仿真脚本
''' </summary>
Public Class PhysicsScript
    Inherits BehaviorBase
    Const TimeStep As Single = 1 / 60
    Dim PhyWorld As FarseerPhysics.Dynamics.World
    Dim Gravity As New Vector2(0, 10)
    Public Overrides Sub Start()
        Dim fill As New FillStyle(True) With {.Color = Colors.Red}
        Dim border As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}
        '创建世界实例
        PhyWorld = New FarseerPhysics.Dynamics.World(Gravity)

        '创建物体
        For i = 0 To 5
            Dim tempBody = BodyFactory.CreateRectangle(PhyWorld, 10, 10, 1, New Vector2(20 + i * 13, 20 + i), 0, BodyType.Dynamic)
            Dim tempRect As New VisualRectangle() With {.Rectangle = New Rect(0, 0, 10, 10), .Border = border, .Fill = fill}
            tempRect.Transform.Center = New System.Numerics.Vector2(5, 5)
            Scene.AddGameVisual(tempRect, New RectangleView(tempRect), 0)
            tempBody.UserData = tempRect
        Next
        BodyFactory.CreateRectangle(PhyWorld, 50, 1, 1, New Vector2(20, 400), 0, BodyType.Static)
    End Sub
    Public Overrides Sub Update()
        PhyWorld.Step(TimeStep)

        For Each SubData In PhyWorld.BodyList
            If SubData.BodyType = BodyType.Static Then Continue For
            Dim temp = CType(SubData.UserData, IGameBody)
            temp.Transform.Translation = New System.Numerics.Vector2(SubData.Position.X, SubData.Position.Y)
            temp.Transform.Rotation = SubData.Rotation
        Next
    End Sub
End Class
