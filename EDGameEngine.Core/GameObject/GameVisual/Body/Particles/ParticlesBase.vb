''' <summary>
''' 粒子系统基类
''' </summary>
Public MustInherit Class ParticlesBase
    Inherits GameBody
    Implements IParticles
    Public Overridable Property Count As Integer = 100 Implements IParticles.Count
    Public Overridable Property Particals As IEnumerable(Of IParticle) Implements IParticles.Particals
End Class
