Imports System.Numerics
Public Class VisualLine
    Inherits GameVisualModel
    Public Property Points As New List(Of Vector2)
    Public Overrides Sub Start()
    End Sub

    Public Overrides Sub Update()
        Transform.Position = New Vector2(Scene.Width / 2, Scene.Height / 2)
    End Sub
End Class
