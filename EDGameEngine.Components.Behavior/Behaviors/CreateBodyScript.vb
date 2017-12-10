Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Windows.UI
Public Class CreateBodyScript
    Inherits BehaviorBase
    Public Overrides Sub Start()
        RemoveHandler Scene.Inputs.Mouse.PointerPressed, AddressOf PointerPressed
        AddHandler Scene.Inputs.Mouse.PointerPressed, AddressOf PointerPressed
    End Sub
    Public Overrides Sub Update()

    End Sub
    Private Sub PointerPressed(loc As Vector2)
        Dim fill As New FillStyle(True) With {.Color = Color.FromArgb(CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)))}
        Dim border As New BorderStyle(True) With {.Color = Color.FromArgb(CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256))), .Width = 1}
        Dim circleModel As New VisualCircle() With {.Radius = 50 * CSng(Rnd.NextDouble), .Border = border, .Fill = fill}
        circleModel.Transform.Translation = loc - Camera.Transform.Translation
        Scene.AddGameVisual(circleModel, New CircleView() With {.CacheAllowed = False}, 0)
        circleModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F * CSng(Rnd.NextDouble)})
    End Sub
End Class
