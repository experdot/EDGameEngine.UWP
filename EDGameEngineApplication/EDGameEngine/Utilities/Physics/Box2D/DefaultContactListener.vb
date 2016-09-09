Imports System

Namespace Global.Box2D
    Public Class DefaultContactListener
        
        Implements IContactListener
        ' Methods
        Public Sub BeginContact(ByVal contact As Contact) Implements IContactListener.BeginContact
        End Sub

        Public Sub EndContact(ByVal contact As Contact) Implements IContactListener.EndContact
        End Sub

        Public Sub PostSolve(ByVal contact As Contact, ByRef impulse As ContactImpulse) Implements IContactListener.PostSolve
        End Sub

        Public Sub PreSolve(ByVal contact As Contact, ByRef oldManifold As Manifold) Implements IContactListener.PreSolve
        End Sub

    End Class
End Namespace

