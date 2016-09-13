Imports System.Numerics
Imports EDGameEngine
Imports Windows.UI
Public Class Scene1
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        Dim rect As New Rect(50, 50, 30, 30)
        Dim fill As New FillStyle(True) With {.Color = Colors.Red}
        Dim border As New BorderStyle(True) With {.Color = Colors.Black, .Width = 10}
        Dim rectModel As New VisualRectangle() With {.Rect = rect, .Border = border, .Fill = fill}
        Dim circleModel As New VisualCircle() With {.Center = New Vector2(30, 30), .Radius = 20, .Border = border, .Fill = fill}

        Me.AddGameVisual(rectModel, New RectangleView(rectModel) With {.CacheAllowed = True})
        Me.AddGameVisual(circleModel, New CircleView(circleModel) With {.CacheAllowed = True})
        'rectModel.GameComponents.Behaviors.Add(New PhysicsScript())

        'Dim tempModel As New ParticalFollow()
        'Me.AddGameVisual(tempModel, New ParticalView(tempModel))
        'tempModel.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height)})


        'Dim tempModel2 As New Plant(New Vector2(Width / 2, Height * 0.8))
        'Me.AddGameVisual(tempModel2, New PlantView(tempModel2))

        'Dim tempModel3 As New AutoDrawModel() With {.Image = ImageManager.GetResource(ImageResourceID.Scenery1)}
        'Me.AddGameVisual(tempModel3, New AutoDrawView(tempModel3))
        'tempModel3.GameComponents.Behaviors.Add(New CameraScript)

        Me.Camera.GameComponents.Behaviors.Add(New CameraScript)
    End Sub
End Class
