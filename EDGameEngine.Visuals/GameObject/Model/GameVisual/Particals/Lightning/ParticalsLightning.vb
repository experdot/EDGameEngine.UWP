Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 闪电粒子系统
''' </summary>
Public Class ParticalsLightning
    Inherits ParticalsBase

    Protected Vectors() As Vector2 = {New Vector2(0, 1),
        New Vector2(0, -1),
        New Vector2(-1, 0),
        New Vector2(1, 0),
        New Vector2(0.7, -0.7),
        New Vector2(0.7, 0.7),
        New Vector2(-0.7, -0.7),
        New Vector2(-0.7, 0.7)}
    Dim Spots As List(Of SpotPartical)

    Public Overrides Sub StartEx()
        Dim center As New Vector2(Scene.Width / 2, Scene.Height * 0.9F)
        Spots = New List(Of SpotPartical)
        Spots.Add(New SpotPartical(center) With {.Color = Colors.Black, .Size = 4.0F * 8})
        Spots.Last.Velocity = New Vector2(0, -1.4 * 5)
        Particals = Spots
    End Sub

    Public Overrides Sub UpdateEx()
        If Spots.Count < 8000 Then
            For i = 0 To Spots.Count - 1
                Spots(i).DivideRandom(Spots, Rnd.Next(2, 5))

                'Spots(i).ApplyForce(Vectors(Rnd.Next(8)) / 6)
                Spots(i).Move()
            Next
        Else
            For i = 0 To Spots.Count - 1
                'Spots(i).ApplyForce(Vectors(Rnd.Next(8)) / 6)
                Spots(i).Move()
            Next
        End If

    End Sub
End Class
