Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Windows.UI
Public Class Scene1
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        'Dim rect As New Rect(50, 50, 30, 30)
        'Dim fill As New FillStyle(True) With {.Color = Colors.Red}
        'Dim border As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}
        'Dim rectModel As New VisualRectangle() With {.Rect = rect, .Border = border, .Fill = fill}
        'Dim circleModel As New VisualCircle() With {.Radius = 50, .Border = border, .Fill = fill}

        'Me.AddGameVisual(rectModel, New RectangleView(rectModel) With {.CacheAllowed = True}, 1)
        'Me.AddGameVisual(circleModel, New CircleView(circleModel) With {.CacheAllowed = True}, 0)
        ''circleModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 2.0F})
        ''rectModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
        'circleModel.GameComponents.Behaviors.Add(New PhysicsScript)

        'Dim tempModel As New ParticalFollow()
        'Me.AddGameVisual(tempModel, New ParticalView(tempModel))
        'tempModel.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height)})

        'Dim tempModel2 As New Plant(New Vector2(Width / 2, Height * 0.8))
        'Me.AddGameVisual(tempModel2, New PlantView(tempModel2))

        Dim tempModel3 As New AutoDrawModel() With {.Image = ImageManager.GetResource(ImageResourceID.Scenery1)}
        Me.AddGameVisual(tempModel3, New AutoDrawView(tempModel3))

        'Dim tempModel4 As New Pointer
        'Me.AddGameVisual(tempModel4, New PointerView(tempModel4))
        'tempModel4.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 0.96})
        'Me.GameComponents.Behaviors.Add(New CreateBodyScript())

        'Dim tempModel5 As New Mandelbrot
        'Me.AddGameVisual(tempModel5, New MandelbrotView(tempModel5))

        'Me.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 1})
        Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
    End Sub
End Class
