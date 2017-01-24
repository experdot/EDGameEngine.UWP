Imports EDGameEngine.Core
Imports FarseerPhysics.Collision
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories
Imports FarseerPhysics.Collision.Shapes
Imports Windows.UI
Imports System.Numerics
Imports Windows.System
''' <summary>
''' 物理仿真脚本
''' </summary>
Public Class PhysicsScript
    Inherits BehaviorBase
    Const TimeStep As Single = 1 / 50
    Const Scale As Single = 36
    Dim PhyWorld As FarseerPhysics.Dynamics.World
    Dim Gravity As New Vector2(0, -10)
    Public Overrides Sub Start()
        '创建世界实例
        PhyWorld = New FarseerPhysics.Dynamics.World(Gravity)

        '创建物体
        For i = 0 To 50
            CreateRectangle(CSng(Rnd.NextDouble() * 10 - 5),
                            CSng(Rnd.NextDouble() * 10 + 10),
                            CSng(Rnd.NextDouble() * 0.5 + 0.1),
                            CSng(Rnd.NextDouble() * 0.5 + 0.1),
                            BodyType.Dynamic, 0, CSng(Rnd.NextDouble() * 1))
        Next
        For i = 0 To 1
            CreateCircle(CSng(Rnd.NextDouble() * 10 - 5),
                         CSng(Rnd.NextDouble() * 10 + 10),
                         CSng(Rnd.NextDouble() + 0.1), BodyType.Dynamic)
        Next
        CreateCircle(4.5, 2.5, 2.5, BodyType.Static)
        CreateRectangle(-0.5, 5, 10, 1, BodyType.Static, 0.5)
        CreateRectangle(-8, 0, 3, 3, BodyType.Static, -0.2, 1)
        CreateRectangle(0, -3, 30, 0.5, BodyType.Static, 0)
        'AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf PointerPressed
    End Sub
    Public Overrides Sub Update()
        PhyWorld.Step(TimeStep)

        For Each SubData In PhyWorld.BodyList
            Dim temp = CType(SubData.UserData, IGameBody)
            temp.Transform.Translation = New System.Numerics.Vector2(SubData.Position.X * Scale + Scene.Width / 2, -SubData.Position.Y * Scale + Scene.Height / 2)
            temp.Transform.Rotation = -SubData.Rotation
        Next
        PointerPressed(Scene.Inputs.Mouse.Location)

    End Sub

    Public Sub CreateRectangle(x As Single, y As Single, w As Single, h As Single, t As BodyType, Optional r As Single = 0, Optional rv As Single = 0)
        Dim FillStyle As New FillStyle(True) With {.Color = Color.FromArgb(CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)))}
        Dim BorderStyle As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}

        Dim tempBody = BodyFactory.CreateRectangle(PhyWorld, w, h, 1, New Vector2(x, y), r, t)
        Dim tempRect As New VisualRectangle() With {.Rectangle = New Rect(-w * Scale / 2, -h * Scale / 2, w * Scale, h * Scale), .Border = BorderStyle, .Fill = FillStyle}
        'tempRect.Transform.Center = New System.Numerics.Vector2(w * Scale / 2, h * Scale / 2)
        Scene.AddGameVisual(tempRect, New RectangleView(tempRect), 0)
        tempBody.UserData = tempRect
        If Not rv = 0 AndAlso t = BodyType.Static Then
            tempBody.BodyType = BodyType.Dynamic
            tempBody.IgnoreGravity = True
            tempBody.Mass = Single.MaxValue
        End If
        'tempBody.IgnoreGravity = True
        tempBody.AngularVelocity = rv
        tempBody.SleepingAllowed = False
    End Sub
    Public Sub CreateCircle(x As Single, y As Single, r As Single, t As BodyType)
        Dim FillStyle As New FillStyle(True) With {.Color = Colors.Red}
        Dim BorderStyle As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}

        Dim tempBody = BodyFactory.CreateCircle(PhyWorld, r, 1, New Vector2(x, y), t)
        Dim tempCircle As New VisualCircle() With {.Radius = r * Scale, .Border = BorderStyle, .Fill = FillStyle}
        Scene.AddGameVisual(tempCircle, New CircleView(tempCircle), 0)
        tempBody.UserData = tempCircle
        tempBody.SleepingAllowed = False
    End Sub
    Private Sub PointerPressed(loc As Vector2)

        Dim temp = loc - Camera.Transform.Translation
        Dim real = New Vector2((temp.X - Scene.Width / 2), -(temp.Y - Scene.Height / 2)) / Scale
        'CreateRectangle(real.X,
        '                real.Y,
        '        CSng(Rnd.NextDouble() * 0.5 + 0.1),
        '        CSng(Rnd.NextDouble() * 0.5 + 0.1),
        '        BodyType.Dynamic, 0, CSng(Rnd.NextDouble() * 1))
        For Each SubData In PhyWorld.BodyList
            Dim v = real - SubData.Position
            Dim r = (real - SubData.Position).Length
            v.SetMag(1)
            If r < 0.5 Then r = 0.5
            SubData.ApplyForce(v / r * 5)
        Next
    End Sub
End Class
