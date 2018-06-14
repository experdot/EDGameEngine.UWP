Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Utilities
Imports Windows.UI
Public Class ParticlesFollow
    Inherits ParticlesBase

    Protected Vectors() As Vector2 = {New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, 0.71), New Vector2(0.7, -0.7), New Vector2(-0.7, 0.71), New Vector2(-0.7, -0.7)}

    Dim MaxAge As Single = 100.0F
    Dim MouseLocation As New Vector2

    Public Overrides Sub StartEx()
        Dim center As New Vector2(Scene.Width / 2, Scene.Height / 2)
        Dim dynamics As New List(Of DynamicParticle)
        For i = 0 To Count - 1
            Dim particle As New DynamicParticle(center)
            particle.Size = CSng(1.0F + Rnd.NextDouble * 4.0F)
            particle.Mass = CSng(particle.Size * Count)
            particle.Color = Color.FromArgb(CByte(Rnd.Next(10, 256)), 255, 255, 255)
            particle.Age = CSng(Rnd.NextDouble * (MaxAge))
            dynamics.Add(particle)
        Next
        Particles = dynamics
        AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf OnMouseMove
    End Sub

    Public Overrides Sub UpdateEx()
        Dim center As New Vector2(Scene.Width / 2, Scene.Height / 2)
        Dim lenth As Single = CSng(Math.Sqrt(Scene.Width * Scene.Width / 4 + Scene.Height * Scene.Height / 4))
        For Each particle As DynamicParticle In Particles
            particle.ApplyForce(Vectors(Rnd.Next(8)))
            Dim distance As Vector2 = MouseLocation - particle.Location
            Dim length As Single = distance.Length
            If length < 0.1F Then
                length = 0.1F
            End If
            Dim direction As Vector2 = distance / length
            Dim ratio As Single = particle.Mass * Count / CSng(length ^ 2)
            particle.ApplyForce(direction * ratio)
            particle.Move()
            particle.Age -= 0.1F + CSng(Rnd.NextDouble * 1)
            If particle.Age < 0 OrElse (particle.Location - center).Length > lenth Then
                particle.Age = MaxAge
                Dim rotation As Single = CSng(Math.PI * 2 * Rnd.NextDouble())
                Dim radius As Single = CSng(10 * Rnd.NextDouble())
                particle.MoveTo(center + New Vector2(1, 0).RotateNew(rotation) * radius)
                particle.Velocity = Vector2.Zero
                particle.Acceleration = Vector2.Zero
            End If
        Next
    End Sub

    Public Sub OnMouseMove(location As Vector2)
        MouseLocation = location - Scene.Camera.Position
    End Sub
End Class
