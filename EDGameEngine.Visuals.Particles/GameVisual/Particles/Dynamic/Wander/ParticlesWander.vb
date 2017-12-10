Imports System.Numerics
Imports Windows.UI
Public Class ParticlesWander
    Inherits ParticlesBase
    ''' <summary>
    ''' 粒子超出范围时，是否移动至中心
    ''' </summary>
    Public Property IsMoveToCenter As Boolean = True

    Protected Vectors() As Vector2 = {New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, 0.71), New Vector2(0.7, -0.7), New Vector2(-0.7, 0.71), New Vector2(-0.7, -0.7)}

    Public Overrides Sub StartEx()
        Dim center As New Vector2(Scene.Width / 2, Scene.Height / 2)
        Dim dynamics As New List(Of DynamicParticle)
        For i = 0 To Count - 1
            Dim particle As New DynamicParticle(center)
            particle.Size = CSng(1.0F + Rnd.NextDouble * 8.0F)
            particle.Mass = CSng(particle.Size * Count)
            particle.Color = Color.FromArgb(CByte(Rnd.Next(10, 256)), 255, 255, 255)
            dynamics.Add(particle)
        Next
        Particles = dynamics
    End Sub

    Public Overrides Sub UpdateEx()
        Dim center As New Vector2(Scene.Width / 2, Scene.Height / 2)
        Dim length As Single = New Vector2(Scene.Width, Scene.Height).Length / 2
        Dim ratio As Single = CSng(Math.Log(Count))
        For Each SubParticle As DynamicParticle In Particles
            SubParticle.ApplyForce(Vectors(Rnd.Next(8)) * ratio)
            If (SubParticle.Location - center).Length > length Then
                If IsMoveToCenter Then
                    SubParticle.MoveTo(center)
                Else
                    Dim index As Integer = Rnd.Next(0, Count)
                    SubParticle.MoveTo(Particles(index).Location)
                End If
            End If
            SubParticle.Move()
        Next
    End Sub
End Class

