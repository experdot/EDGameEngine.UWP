Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 自然树粒子系统
''' </summary>
Public Class ParticlesTree
    Inherits ParticlesBase

    Dim Spots As List(Of SpotParticle)

    Public Overrides Sub StartEx()
        Dim center As New Vector2(Scene.Width / 2, Scene.Height * 0.6F)
        Spots = New List(Of SpotParticle)
        Spots.Add(New SpotParticle(center) With {.Color = Colors.Black, .Size = 4.0F * 5})
        Spots.Last.Velocity = New Vector2(0, -6)
        Particals = Spots
    End Sub

    Public Overrides Sub UpdateEx()
        Static count As Integer = 0

        If Spots.Count < 8000 Then
            For i = 0 To Spots.Count - 1
                Spots(i).DivideRandom(Spots, RandomHelper.NextNorm(2, 5) - 1)
                Spots(i).Update()
            Next
        Else
            For i = 0 To Spots.Count - 1
                Spots(i).Update()
            Next
        End If

    End Sub
End Class
