Imports System

Namespace Global.Box2D
    Public Interface IContactFilter
        ' Methods
        Function RayCollide(ByVal userData As Object, ByVal fixture As Fixture) As Boolean
        Function ShouldCollide(ByVal fixtureA As Fixture, ByVal fixtureB As Fixture) As Boolean
    End Interface
End Namespace

