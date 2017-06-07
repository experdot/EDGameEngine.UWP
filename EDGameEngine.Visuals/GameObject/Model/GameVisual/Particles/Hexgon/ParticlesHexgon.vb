Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 六边形粒子系统
''' </summary>
Public Class ParticlesHexgon
    Inherits ParticlesBase

    Dim Lines As List(Of LineParticle)

    Public Overrides Sub StartEx()
        Dim center As New Vector2(Scene.Width * 0.5F, Scene.Height * 0.5F)
        Dim velocity As New Vector2(0, -4.0F)
        Lines = New List(Of LineParticle)
        For i = 0 To 2
            Lines.Add(New LineParticle(center) With {.Color = Color.FromArgb(255, 0, 0, 0), .Size = 4.0F * 6})
            Lines.Last.Velocity = velocity.RotateNew(CSng(Math.PI / 3 * 2 * i))
        Next
        Particles = Lines
    End Sub

    Public Overrides Sub UpdateEx()
        Static count As Integer = 0

        For i = 0 To Lines.Count - 1
            Lines(i).Update(Lines)
        Next

    End Sub
End Class
