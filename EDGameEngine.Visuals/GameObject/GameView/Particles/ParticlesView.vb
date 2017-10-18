Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 粒子系统视图
''' </summary>
Public Class ParticlesView
    Inherits TypedCanvasView(Of IParticles)
    Public Sub New(Target As IParticles)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        For Each SubParticle In Target.Particles
            drawingSession.FillCircle(SubParticle.Location, SubParticle.Size, SubParticle.Color)
        Next
    End Sub
End Class
