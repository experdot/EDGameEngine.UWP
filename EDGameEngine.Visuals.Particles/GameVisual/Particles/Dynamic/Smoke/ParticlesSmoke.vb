Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Utilities
Imports Windows.UI
Public Class ParticlesSmoke
    Inherits ParticlesBase

    Protected Vectors() As Vector2 = {New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, 0.71), New Vector2(0.7, -0.7), New Vector2(-0.7, 0.71), New Vector2(-0.7, -0.7)}

    Dim MaxAge As Single = 50.0F
    Dim MouseLocation As New Vector2

    Public Overrides Sub StartEx()
        Dim dynamics As New List(Of DynamicParticle)
        For i = 0 To Count - 1
            dynamics.Add(New DynamicParticle(New Vector2(Scene.Width / 2, Scene.Height / 2)))
            dynamics(i).Mass = CSng(10)
            dynamics(i).Size = CSng(0.1 + Rnd.NextDouble * 100)
            dynamics(i).Color = Color.FromArgb(CByte(Rnd.Next(10, 256)), 255, 255, 255)
            dynamics(i).Age = CSng(Rnd.NextDouble * (MaxAge))
        Next
        Particles = dynamics
        AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf OnMouseMove
    End Sub

    Public Overrides Sub UpdateEx()
        Dim center As New Vector2(Scene.Width / 2, Scene.Height * 0.9F)
        Dim lenth As Single = CSng(Math.Sqrt(Scene.Width * Scene.Width / 4 + Scene.Height * Scene.Height / 4))
        For Each particle As DynamicParticle In Particles
            particle.ApplyForce(Vectors(Rnd.Next(8)))
            particle.ApplyForce(Vectors(1))
            particle.Move()
            particle.Age -= 0.1F + CSng(Rnd.NextDouble * 1)
            If particle.Age < 0 OrElse (particle.Location - center).Length > lenth Then
                particle.Age = MaxAge
                particle.Color = Color.FromArgb(CByte(particle.Age / MaxAge * 100), 255, 255, 255)
                Dim rotation As Single = CSng(Math.PI * 2 * Rnd.NextDouble())
                Dim radius As Single = CSng(10 * Rnd.NextDouble())
                particle.MoveTo(center + New Vector2(1, 0).RotateNew(rotation) * radius)
                particle.Velocity = Vector2.Zero
                particle.Acceleration = Vector2.Zero
            End If
            particle.Size = (MaxAge - particle.Age)
        Next
    End Sub

    Public Sub OnMouseMove(location As Vector2)
        MouseLocation = location - Scene.Camera.Position
    End Sub
End Class
