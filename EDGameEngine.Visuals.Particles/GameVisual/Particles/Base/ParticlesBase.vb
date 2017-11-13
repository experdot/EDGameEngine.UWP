Imports EDGameEngine.Core
''' <summary>
''' 粒子系统基类
''' </summary>
Public MustInherit Class ParticlesBase
    Inherits GameBody
    Implements IParticles
    Public Overridable Property Count As Integer = 100 Implements IParticles.Count
    Public Overridable Property Particles As IEnumerable(Of IParticle) Implements IParticles.Particles

    ''' <summary>
    ''' 回收死亡的粒子
    ''' </summary>
    Public Sub KillDead(Of T As IParticle)(particles As List(Of T))
        Dim modifys As New List(Of Action)
        If particles.Count > 0 Then
            For i = 0 To particles.Count - 1
                Dim subParticle = particles(i)
                If subParticle.IsDead Then
                    modifys.Add(New Action(Sub()
                                               particles.Remove(subParticle)
                                           End Sub))
                End If
            Next
        End If
        InvokeActions(modifys)
    End Sub

    Private Sub InvokeActions(actions As List(Of Action))
        If actions.Count > 0 Then
            For i = 0 To actions.Count - 1
                actions(i).Invoke
            Next
        End If
    End Sub
End Class
