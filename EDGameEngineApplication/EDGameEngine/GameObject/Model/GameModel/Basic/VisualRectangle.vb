Imports System.Numerics

Public Class VisualRectangle
    Inherits GameVisualModel
    Public Property Rect As Rect
    Public Sub New(rect As Rect)
        Me.Rect = rect
    End Sub
    Public Overrides Sub Start()

    End Sub
    Public Overrides Sub Update()
        Transform.Position = New Vector2(Scene.Width / 2, Scene.Height / 2)
        Transform.Center = New Vector2(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2)
        'Transform.Rotation = (Transform.Rotation + 0.1 * Rnd.NextDouble) Mod （Math.PI * 2)
        'Transform.Scale = New Vector2(Math.Sin(Transform.Rotation), Math.Cos(Transform.Rotation))
    End Sub
End Class
