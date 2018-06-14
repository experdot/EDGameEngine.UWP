Imports EDGameEngine.Core
Imports FarseerPhysics.Collision
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories
Imports FarseerPhysics.Collision.Shapes
Imports Windows.UI
Imports System.Numerics
Imports EDGameEngine.Core.Graphics
Imports EDGameEngine.Core.Utilities
''' <summary>
''' 物理仿真脚本
''' </summary>
Public Class PhysicsScript
    Inherits BehaviorBase
    Const TimeStep As Single = 1 / 50
    Const Scale As Single = 36

    Public Property IgnoreGravity As Boolean = False
    Public Property CreateStatic As Boolean = True

    Dim PhysicWorld As World
    Dim Gravity As New Vector2(0, -10)
    Public Overrides Sub Start()
        '创建世界实例
        PhysicWorld = New World(Gravity)

        '创建物体
        For i = 0 To 50
            CreateRectangle(CSng(Rnd.NextDouble() * 10 - 5),
                            CSng(Rnd.NextDouble() * 10 + 10),
                            CSng(Rnd.NextDouble() * 0.5 + 0.1),
                            CSng(Rnd.NextDouble() * 0.5 + 0.1),
                            BodyType.Dynamic, 0, CSng(Rnd.NextDouble() * 1))
        Next
        For i = 0 To 10
            CreateCircle(CSng(Rnd.NextDouble() * 10 - 5),
                         CSng(Rnd.NextDouble() * 10 + 10),
                         CSng(Rnd.NextDouble() + 0.1), BodyType.Dynamic)
        Next
        If CreateStatic Then
            CreateCircle(4.5, 2.5, 2.5, BodyType.Static)
            CreateRectangle(-0.5, 5, 10, 1, BodyType.Static, 0.5)
            CreateRectangle(-8, 0, 3, 3, BodyType.Static, -0.2, 1)
            CreateRectangle(0, -3, 30, 0.5, BodyType.Static, 0)
        End If
        AddHandler Scene.Inputs.Mouse.PointerPressed, AddressOf PointerPressed
    End Sub
    Public Overrides Sub Update()
        Static Last As Date = Date.Now
        Dim steping As Single = CSng(Date.Now.Subtract(Last).TotalMilliseconds / 1000)
        PhysicWorld.Step(steping)
        Last = Date.Now

        For Each body In PhysicWorld.BodyList
            Dim temp = CType(body.UserData, IGameBody)
            temp.Transform.Translation = New System.Numerics.Vector2(body.Position.X * Scale + Scene.Width / 2, -body.Position.Y * Scale + Scene.Height / 2)
            temp.Transform.Rotation = -body.Rotation
        Next

        PointerMove(Scene.Inputs.Mouse.Location)
    End Sub

    Public Sub CreateRectangle(x As Single, y As Single, w As Single, h As Single, t As BodyType, Optional r As Single = 0, Optional rv As Single = 0)
        Dim FillStyle As New FillStyle(True) With {.Color = Color.FromArgb(CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)))}
        Dim BorderStyle As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}

        Dim tempBody = BodyFactory.CreateRectangle(PhysicWorld, w, h, 1, New Vector2(x, y), r, t)
        Dim tempRect As New VisualRectangle() With
        {
            .Rectangle = New Rect(-w * Scale / 2, -h * Scale / 2, w * Scale, h * Scale),
            .Border = BorderStyle,
            .Fill = FillStyle
        }
        'tempRect.Transform.Center = New System.Numerics.Vector2(w * Scale / 2, h * Scale / 2)
        Scene.AddGameVisual(tempRect, New RectangleView(), 0)
        tempBody.UserData = tempRect
        If Not rv = 0 AndAlso t = BodyType.Static Then
            tempBody.BodyType = BodyType.Dynamic
            tempBody.IgnoreGravity = True
            tempBody.Mass = Single.MaxValue
        End If
        tempBody.IgnoreGravity = IgnoreGravity
        tempBody.AngularVelocity = rv
        tempBody.SleepingAllowed = False
    End Sub
    Public Sub CreateCircle(x As Single, y As Single, r As Single, t As BodyType)
        Dim FillStyle As New FillStyle(True) With {.Color = Colors.Red}
        Dim BorderStyle As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}

        Dim tempBody = BodyFactory.CreateCircle(PhysicWorld, r, 1, New Vector2(x, y), t)
        Dim tempCircle As New VisualCircle() With {.Radius = r * Scale, .Border = BorderStyle, .Fill = FillStyle}
        Scene.AddGameVisual(tempCircle, New CircleView(), 0)
        tempBody.IgnoreGravity = IgnoreGravity
        tempBody.UserData = tempCircle
        tempBody.SleepingAllowed = False
    End Sub
    Private Sub PointerMove(loc As Vector2)
        Dim temp = loc - Camera.Transform.Translation
        Dim real = New Vector2((temp.X - Scene.Width / 2), -(temp.Y - Scene.Height / 2)) / Scale
        For Each body In PhysicWorld.BodyList
            Dim v = real - body.Position
            Dim r = (real - body.Position).Length
            v.SetMag(1)
            If r < 0.5 Then r = 0.5
            body.ApplyForce(v / r * 5)
        Next
    End Sub

    Private Sub PointerPressed(loc As Vector2)
        Dim temp = loc - Camera.Transform.Translation
        Dim real = New Vector2((temp.X - Scene.Width / 2), -(temp.Y - Scene.Height / 2)) / Scale
        CreateRectangle(real.X, real.Y,
                        CSng(Rnd.NextDouble() * 0.5 + 0.1), CSng(Rnd.NextDouble() * 0.5 + 0.1),
                        BodyType.Dynamic, 0, CSng(Rnd.NextDouble() * 1))
    End Sub
End Class
