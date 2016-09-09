Imports System

Namespace Global.Box2D
    Public Interface IContactListener
        ' Methods
        Sub BeginContact(ByVal contact As Contact)
        Sub EndContact(ByVal contact As Contact)
        Sub PostSolve(ByVal contact As Contact, ByRef impulse As ContactImpulse)
        Sub PreSolve(ByVal contact As Contact, ByRef oldManifold As Manifold)
    End Interface
End Namespace

