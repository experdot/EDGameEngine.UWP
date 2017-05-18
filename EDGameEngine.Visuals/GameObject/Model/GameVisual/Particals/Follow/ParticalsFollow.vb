Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
Public Class ParticalsFollow
    Inherits ParticalsBase

    Protected Vectors() As Vector2 = {New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, 0.71), New Vector2(0.7, -0.7), New Vector2(-0.7, 0.71), New Vector2(-0.7, -0.7)}
    Dim MouseVec As New Vector2

    Public Overrides Sub StartEx()
        Dim dynamics As New List(Of DynamicPartical)
        For i = 0 To Count - 1
            dynamics.Add(New DynamicPartical(New Vector2(Scene.Width / 2, Scene.Height / 2)))
            dynamics(i).Mass = CSng(1 + Rnd.NextDouble * 99)
            dynamics(i).Size = CSng(0.1 + Rnd.NextDouble * 4)
            dynamics(i).ImageSize = CSng(2 + Rnd.NextDouble * 2)
            dynamics(i).Color = Color.FromArgb(255, 0, 0, 0)
        Next
        Particals = dynamics
        AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf OnMouseMove
    End Sub

    Public Overrides Sub UpdateEx()
        Dim center As New Vector2(Scene.Width / 2, Scene.Height / 2)
        Dim lenth As Single = CSng(Math.Sqrt(Scene.Width * Scene.Width / 4 + Scene.Height * Scene.Height / 4))
        For Each SubEle As DynamicPartical In Particals
            SubEle.ApplyForce(Vectors(Rnd.Next(8)) * 2)
            'SubEle.ApplyForce(New Vector2(1 * Rnd.NextDouble, 0))
            'SubEle.ApplyForce(MouseVec - SubEle.Location)
            If (SubEle.Location - center).Length > lenth Then
                Dim index As Integer = Rnd.Next(0, Count)
                SubEle.MoveTo(Particals(index).Location)
            End If
            SubEle.Move()
        Next
    End Sub

    Public Sub OnMouseMove(ByVal Location As Vector2)
        MouseVec = Location - Scene.Camera.Position
    End Sub
End Class
