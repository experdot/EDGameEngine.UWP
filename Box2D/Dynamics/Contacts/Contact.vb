Imports System
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    Public MustInherit Class Contact
        
        ' Methods
        Shared Sub New()
            Dim funcArray1 As Func(Of Fixture, Fixture, Contact)(,) = New Func(Of Fixture, Fixture, Contact)(,) {{New Func(Of Fixture, Fixture, Contact)(AddressOf Lambdas.Current.cctor_b__25_0), New Func(Of Fixture, Fixture, Contact)(AddressOf Lambdas.Current.cctor_b__25_1)}, {New Func(Of Fixture, Fixture, Contact)(AddressOf Lambdas.Current.cctor_b__25_2), New Func(Of Fixture, Fixture, Contact)(AddressOf Lambdas.Current.cctor_b__25_3)}}
            Contact.s_registers = funcArray1
        End Sub

        Friend Sub New()
            Me._nodeA = New ContactEdge
            Me._nodeB = New ContactEdge
            Me._fixtureA = Nothing
            Me._fixtureB = Nothing
        End Sub

        Friend Sub New(ByVal fA As Fixture, ByVal fB As Fixture)
            Me._nodeA = New ContactEdge
            Me._nodeB = New ContactEdge
            Me._flags = ContactFlags.None
            If (fA.IsSensor OrElse fB.IsSensor) Then
                Me._flags = (Me._flags Or ContactFlags.NonSolid)
            End If
            Me._fixtureA = fA
            Me._fixtureB = fB
            Me._manifold._pointCount = 0
            Me._prev = Nothing
            Me._next = Nothing
            Me._nodeA.Contact = Nothing
            Me._nodeA.Prev = Nothing
            Me._nodeA.Next = Nothing
            Me._nodeA.Other = Nothing
            Me._nodeB.Contact = Nothing
            Me._nodeB.Prev = Nothing
            Me._nodeB.Next = Nothing
            Me._nodeB.Other = Nothing
        End Sub

        Public Function AreTouching() As Boolean
            Return ((Me._flags And ContactFlags.Touch) = ContactFlags.Touch)
        End Function

        Friend MustOverride Function ComputeTOI(ByRef sweepA As Sweep, ByRef sweepB As Sweep) As Single

        Friend Shared Function Create(ByVal fixtureA As Fixture, ByVal fixtureB As Fixture) As Contact
            Dim shapeType As ShapeType = fixtureA.ShapeType
            Dim type2 As ShapeType = fixtureB.ShapeType
            Debug.Assert(((ShapeType.Unknown < shapeType) AndAlso (shapeType < ShapeType.TypeCount)))
            Debug.Assert(((ShapeType.Unknown < type2) AndAlso (type2 < ShapeType.TypeCount)))
            If (shapeType > type2) Then
                Return Contact.s_registers(DirectCast(shapeType, Integer), DirectCast(type2, Integer)).Invoke(fixtureA, fixtureB)
            End If
            Return Contact.s_registers(DirectCast(shapeType, Integer), DirectCast(type2, Integer)).Invoke(fixtureB, fixtureA)
        End Function

        Friend MustOverride Sub Evaluate()

        Public Sub FlagForFiltering()
            Me._flags = (Me._flags Or ContactFlags.Filter)
        End Sub

        Public Function GetFixtureA() As Fixture
            Return Me._fixtureA
        End Function

        Public Function GetFixtureB() As Fixture
            Return Me._fixtureB
        End Function

        Public Sub GetManifold(<Out> ByRef manifold As Manifold)
            manifold = Me._manifold
        End Sub

        Public Function GetNext() As Contact
            Return Me._next
        End Function

        Public Sub GetWorldManifold(<Out> ByRef worldManifold As WorldManifold)
            Dim form As XForm
            Dim form2 As XForm
            Dim body As Body = Me._fixtureA.GetBody
            Dim body2 As Body = Me._fixtureB.GetBody
            Dim shape As Shape = Me._fixtureA.GetShape
            Dim shape2 As Shape = Me._fixtureB.GetShape
            body.GetXForm(form)
            body.GetXForm(form2)
            worldManifold = New WorldManifold(Me._manifold, form, shape.Radius, form2, shape2.Radius)
        End Sub

        Public Function IsSolid() As Boolean
            Return ((Me._flags And ContactFlags.NonSolid) = ContactFlags.None)
        End Function

        Public Sub SetSolid(ByVal solid As Boolean)
            If solid Then
                Me._flags = (Me._flags And Not ContactFlags.NonSolid)
            Else
                Me._flags = (Me._flags Or ContactFlags.NonSolid)
            End If
        End Sub

        Friend Sub Update(ByVal listener As IContactListener)
            Dim oldManifold As Manifold = Me._manifold
            Me.Evaluate()
            Dim body As Body = Me._fixtureA.GetBody
            Dim body2 As Body = Me._fixtureB.GetBody
            Dim num As Integer = oldManifold._pointCount
            Dim num2 As Integer = Me._manifold._pointCount
            If ((num2 = 0) AndAlso (num > 0)) Then
                body.WakeUp()
                body2.WakeUp()
            End If
            If (((body.IsStatic OrElse body.IsBullet) OrElse body2.IsStatic) OrElse body2.IsBullet) Then
                Me._flags = (Me._flags And Not ContactFlags.Slow)
            Else
                Me._flags = (Me._flags Or ContactFlags.Slow)
            End If
            Dim i As Integer
            For i = 0 To Me._manifold._pointCount - 1
                Dim point As ManifoldPoint = Me._manifold._points.Item(i)
                point.NormalImpulse = 0!
                point.TangentImpulse = 0!
                Dim id As ContactID = point.Id
                Dim j As Integer
                For j = 0 To oldManifold._pointCount - 1
                    Dim point2 As ManifoldPoint = oldManifold._points.Item(j)
                    If (point2.Id.Key = id.Key) Then
                        point.NormalImpulse = point2.NormalImpulse
                        point.TangentImpulse = point2.TangentImpulse
                        Exit For
                    End If
                Next j
                Me._manifold._points.Item(i) = point
            Next i
            If ((num = 0) AndAlso (num2 > 0)) Then
                Me._flags = (Me._flags Or ContactFlags.Touch)
                listener.BeginContact(Me)
            End If
            If ((num > 0) AndAlso (num2 = 0)) Then
                Me._flags = (Me._flags And Not ContactFlags.Touch)
                listener.EndContact(Me)
            End If
            If ((Me._flags And ContactFlags.NonSolid) = ContactFlags.None) Then
                listener.PreSolve(Me, oldManifold)
                If (Me._manifold._pointCount = 0) Then
                    Me._flags = (Me._flags And Not ContactFlags.Touch)
                End If
            End If
        End Sub


        ' Fields
        Friend _fixtureA As Fixture
        Friend _fixtureB As Fixture
        Friend _flags As ContactFlags
        Friend _manifold As Manifold
        Friend _next As Contact
        Friend _nodeA As ContactEdge
        Friend _nodeB As ContactEdge
        Friend _prev As Contact
        Friend _toi As Single
        Friend Shared s_registers As Func(Of Fixture, Fixture, Contact)(,)


        Private NotInheritable Class Lambdas
            
            ' Methods
            Friend Function cctor_b__25_0(ByVal f1 As Fixture, ByVal f2 As Fixture) As Contact
                Return New CircleContact(f1, f2)
            End Function

            Friend Function cctor_b__25_1(ByVal f1 As Fixture, ByVal f2 As Fixture) As Contact
                Return New PolygonAndCircleContact(f1, f2)
            End Function

            Friend Function cctor_b__25_2(ByVal f1 As Fixture, ByVal f2 As Fixture) As Contact
                Return New PolygonAndCircleContact(f1, f2)
            End Function

            Friend Function cctor_b__25_3(ByVal f1 As Fixture, ByVal f2 As Fixture) As Contact
                Return New PolygonContact(f1, f2)
            End Function


            ' Fields
            Public Shared ReadOnly Current As Lambdas = New Lambdas
        End Class
    End Class
End Namespace

