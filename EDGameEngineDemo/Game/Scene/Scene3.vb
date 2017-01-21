Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Windows.UI
Public Class Scene3
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        Dim rect As New Rect(0, 0, 100, 20)
        Dim rect2 As New Rect(0, 0, 10, 10)
        Dim rect3 As New Rect(0, 0, 20, 20)
        Dim fill As New FillStyle(True) With {.Color = Colors.Red}
        Dim border As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}

        Dim rectModel As New VisualRectangle() With {.Rectangle = rect, .Border = border, .Fill = fill}
        Dim rectModel2 As New VisualRectangle() With {.Rectangle = rect2, .Border = border, .Fill = fill}
        Dim rectModel3 As New VisualRectangle() With {.Rectangle = rect3, .Border = border, .Fill = fill}

        Me.AddGameVisual(rectModel, New RectangleView(rectModel) With {.CacheAllowed = True}, 0)
        Me.AddGameVisual(rectModel2, New RectangleView(rectModel2) With {.CacheAllowed = True}, 0)
        Me.AddGameVisual(rectModel3, New RectangleView(rectModel3) With {.CacheAllowed = True}, 0)
        rectModel.Transform.Translation = New Vector2(0, 200)

        rectModel2.GameComponents.Behaviors.Add(New PhysicsScript With {.target1 = rectModel2, .target2 = rectModel3, .target3 = rectModel})
    End Sub
End Class
