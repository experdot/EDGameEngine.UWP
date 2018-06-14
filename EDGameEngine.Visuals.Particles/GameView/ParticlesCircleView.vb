Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 粒子系统视图
''' </summary>
Public Class ParticlesCircleView
    Inherits TypedCanvasView(Of IParticles)
    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        For Each particle In Target.Particles
            session.FillCircle(particle.Location, particle.Size, particle.Color)
        Next
    End Sub
End Class
