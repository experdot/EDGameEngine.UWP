Imports System
Imports System.Diagnostics

Namespace Global.Box2D
    Friend Class PolygonContact
        Inherits Contact
        ' Methods
        Friend Sub New(ByVal fixtureA As Fixture, ByVal fixtureB As Fixture)
            MyBase.New(fixtureA, fixtureB)
            Debug.Assert((MyBase._fixtureA.ShapeType = ShapeType.Polygon))
            Debug.Assert((MyBase._fixtureB.ShapeType = ShapeType.Polygon))
        End Sub

        Friend Overrides Function ComputeTOI(ByRef sweepA As Sweep, ByRef sweepB As Sweep) As Single
            Dim input As TOIInput
            input.sweepA = sweepA
            input.sweepB = sweepB
            input.tolerance = Settings.b2_linearSlop
            Return TimeOfImpact.CalculateTimeOfImpact(input, DirectCast(MyBase._fixtureA.GetShape, PolygonShape), DirectCast(MyBase._fixtureB.GetShape, PolygonShape))
        End Function

        Friend Overrides Sub Evaluate()
            Dim form As XForm
            Dim form2 As XForm
            Dim body As Body = MyBase._fixtureA.GetBody
            Dim body2 As Body = MyBase._fixtureB.GetBody
            body.GetXForm(form)
            body2.GetXForm(form2)
            Collision.CollidePolygons(Me._manifold, DirectCast(MyBase._fixtureA.GetShape, PolygonShape), form, DirectCast(MyBase._fixtureB.GetShape, PolygonShape), form2)
        End Sub

    End Class
End Namespace

