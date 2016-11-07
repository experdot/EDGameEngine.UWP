Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
Public Class ParticalFollow
    Inherits ParticalsBase
    Dim Vectors() As Vector2 = {New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, 0.71), New Vector2(0.7, -0.7), New Vector2(-0.7, 0.71), New Vector2(-0.7, -0.7)}
    Dim MouseVec As New Vector2
    Public Overrides Sub UpdateEx()
        Dim CenterVec As New Vector2(Scene.Width / 2, Scene.Height / 2)
        Dim RectLength As Single = CSng(Math.Sqrt(Scene.Width * Scene.Width / 4 + Scene.Height * Scene.Height / 4))
        For Each SubEle In Particals
            SubEle.ApplyForce(Vectors(Rnd.Next(8)) * 2)
            'SubEle.ApplyForce(New Vector2(1 * Rnd.NextDouble, 0))
            'SubEle.ApplyForce(MouseVec - SubEle.Location)
            If (SubEle.Location - CenterVec).Length > RectLength Then
                Dim index As Integer = Rnd.Next(0, Count)
                SubEle.StartNew(Particals(index).Location)
            End If
            SubEle.Move()
        Next
    End Sub
    Public Overrides Sub StartEx()
        Particals = New List(Of Partical)
        For i = 0 To Count - 1
            Particals.Add(New Partical(New Vector2(Scene.Width / 2, Scene.Height / 2)))
            Particals(i).Mass = CSng(1 + Rnd.NextDouble * 99)
            Particals(i).Size = CSng(0.1 + Rnd.NextDouble * 4)
            Particals(i).ImageSize = CSng(2 + Rnd.NextDouble * 2)
            Particals(i).Color = Color.FromArgb(255, 0, 0, 0)
        Next
        AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf OnMouseMove
    End Sub
    Public Sub OnMouseMove(ByVal Location As Vector2)
        MouseVec = Location - Scene.Camera.Position
    End Sub
End Class
