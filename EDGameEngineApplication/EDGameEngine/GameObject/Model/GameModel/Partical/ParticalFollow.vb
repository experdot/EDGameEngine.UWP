Imports System.Numerics
Imports Windows.UI
Public Class ParticalFollow
    Inherits ParticalsBase
    Dim Vectors() As Vector2 = {New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, 0.71), New Vector2(0.7, -0.7), New Vector2(-0.7, 0.71), New Vector2(-0.7, -0.7)}
    Public Overrides Sub Update()
        Dim CenterVec As New Vector2(Scene.Width / 2, Scene.Height / 2)
        Dim RectLength As Single = Math.Sqrt(Scene.Width * Scene.Width / 4 + Scene.Height * Scene.Height / 4)
        For Each SubEle In Particals
            SubEle.ApplyForce(Vectors(Rnd.Next(8)) * 2)
            'SubPartical.ApplyForce(New Vector2(2 * Rnd.NextDouble, 0))
            'SubEle.ApplyForce(New Vector2(World.MouseX - SubEle.Location.X, World.MouseY - SubEle.Location.Y))
            If (SubEle.Location - CenterVec).Length > RectLength Then
                SubEle.StartNew(New Vector2(Scene.Width * Rnd.NextDouble, Scene.Height * Rnd.NextDouble))
                SubEle.Age = 0
            End If
            SubEle.Move()
            SubEle.Age += 0.1
        Next
    End Sub

    Public Overrides Sub InitParticals()
        Particals = New List(Of Partical)
        For i = 0 To Count - 1
            Particals.Add(New Partical(New Vector2(100, 100)))
            Particals(i).Mass = 1 + Rnd.NextDouble * 99
            Particals(i).Size = 0.1 + Rnd.NextDouble * 2
            Particals(i).ImageSize = 2 + Rnd.NextDouble * 2
            Particals(i).Color = Color.FromArgb(255, 255, 255, 255)
        Next
    End Sub
End Class
