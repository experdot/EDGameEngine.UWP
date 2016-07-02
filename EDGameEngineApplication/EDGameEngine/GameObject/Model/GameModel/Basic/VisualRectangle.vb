Imports System.Numerics

Public Class VisualRectangle
    Inherits GameVisualModel
    Public Property Rect As Rect
    Public Overrides Property Presenter As GameView = New RectangleView(Me)
    Public Sub New(rect As Rect)
        Me.Rect = rect
    End Sub
    Public Overrides Sub Update()
        Transform.Center = New Vector2(Scene.Width / 2, Scene.Height / 2)
        Transform.Rotation = (Transform.Rotation + 0.05) Mod （Math.PI * 2)
        Appearance.Opcacity = 0.5 + 0.3 * Math.Sin(Environment.TickCount / 500)
    End Sub
End Class
