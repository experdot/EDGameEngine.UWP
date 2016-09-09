Imports System

Namespace Global.Box2D
    Public Class DefaultContactFilter
        
        Implements IContactFilter
        ' Methods
        Public Function RayCollide(ByVal userData As Object, ByVal fixture As Fixture) As Boolean Implements IContactFilter.RayCollide
            Return ((userData Is Nothing) OrElse Me.ShouldCollide(DirectCast(userData, Fixture), fixture))
        End Function

        Public Function ShouldCollide(ByVal fixtureA As Fixture, ByVal fixtureB As Fixture) As Boolean Implements IContactFilter.ShouldCollide
            Dim filter As Filter
            Dim filter2 As Filter
            fixtureA.GetFilterData(filter)
            fixtureB.GetFilterData(filter2)
            If ((filter.groupIndex = filter2.groupIndex) AndAlso (filter.groupIndex > 0)) Then
                Return (filter.groupIndex > 0)
            End If
            Return ((Not (filter.maskBits And filter2.categoryBits) = 0) AndAlso ((filter.categoryBits And filter2.maskBits) > 0))
        End Function

    End Class
End Namespace

