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
        Dim center As New Vector2(Scene.Width / 2, Scene.Height * 1.4F)
        Spots = New List(Of SpotParticle) From {
            New SpotParticle(center) With {.Color = Color.FromArgb(255, 0, 0, 0), .Size = 128.0F}
        }
        Spots.First.Velocity = New Vector2(0, -12.0F)
        Spots.First.Age = 30
        Particles = Spots
    End Sub

    Public Overrides Sub UpdateEx()
        If Spots.Count > 0 Then
            For i = 0 To Spots.Count - 1
                Spots(i).Update(Spots, RandomHelper.NextNorm(1, 3))
            Next
            KillDead(Of SpotParticle)(Spots)
        End If
    End Sub
End Class
