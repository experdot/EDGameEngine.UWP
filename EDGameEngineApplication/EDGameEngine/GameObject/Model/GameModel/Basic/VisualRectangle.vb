Imports System.Numerics

Public Class VisualRectangle
    Inherits GameVisualModel
    Public Shared Rnd As New Random
    Public Property Rect As Rect
    Public Overrides Property Presenter As GameView = New RectangleView(Me)
    Public Sub New(rect As Rect)
        Me.Rect = rect
    End Sub
    Public Overrides Sub Update()
        Transform.Position = New Vector2(Scene.Width / 2, Scene.Height / 2)
        Transform.Center = New Vector2(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2)
        Transform.Rotation = (Transform.Rotation + Rnd.NextDouble * 0.1) Mod （Math.PI * 2)
        Appearance.Opcacity = 0.5 + 0.3 * Math.Sin(Environment.TickCount / 500)
    End Sub
End Class
