Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    Public Class Fixture
        
        ' Methods
        Friend Sub New()
        End Sub

        Public Sub ComputeMass(<Out> ByRef massData As MassData)
            Me._shape.ComputeMass(massData, Me._density)
        End Sub

        Friend Sub Create(ByVal broadPhase As BroadPhase, ByVal body As Body, ByRef xf As XForm, ByVal def As FixtureDef)
            Dim aabb As AABB
            Me._userData = def.userData
            Me._friction = def.friction
            Me._restitution = def.restitution
            Me._density = def.density
            Me._body = body
            Me._next = Nothing
            Me._filter = def.filter
            Me._isSensor = def.isSensor
            Me._shape = def.shape.Clone
            Me._shape.ComputeAABB(aabb, xf)
            Me._proxyId = broadPhase.CreateProxy(aabb, Me)
        End Sub

        Friend Sub Destroy(ByVal broadPhase As BroadPhase)
            If (Me._proxyId <> BroadPhase.NullProxy) Then
                broadPhase.DestroyProxy(Me._proxyId)
                Me._proxyId = BroadPhase.NullProxy
            End If
            Me._shape = Nothing
        End Sub

        Public Function GetBody() As Body
            Return Me._body
        End Function

        Public Function GetDensity() As Single
            Return Me._density
        End Function

        Public Sub GetFilterData(<Out> ByRef filter As Filter)
            filter = Me._filter
        End Sub

        Public Function GetFriction() As Single
            Return Me._friction
        End Function

        Public Function GetNext() As Fixture
            Return Me._next
        End Function

        Public Function GetRestitution() As Single
            Return Me._restitution
        End Function

        Public Function GetShape() As Shape
            Return Me._shape
        End Function

        Public Function GetUserData() As Object
            Return Me._userData
        End Function

        Public Function IsSensor() As Boolean
            Return Me._isSensor
        End Function

        Public Sub SetDensity(ByVal density As Single)
            Me._density = density
        End Sub

        Public Sub SetFilterData(ByRef filter As Filter)
            Me._filter = filter
            If (Not Me._body Is Nothing) Then
                Dim conactList As ContactEdge = Me._body.ConactList
                Do While (conactList IsNot Nothing)
                    Dim contact As Contact = conactList.Contact
                    Dim fixtureA As Fixture = contact.GetFixtureA
                    Dim fixtureB As Fixture = contact.GetFixtureB
                    If ((fixtureA Is Me) OrElse (fixtureB Is Me)) Then
                        contact.FlagForFiltering
                    End If
                Loop
            End If
        End Sub

        Public Sub SetFriction(ByVal friction As Single)
            Me._friction = friction
        End Sub

        Public Sub SetRestitution(ByVal restitution As Single)
            Me._restitution = restitution
        End Sub

        Public Sub SetSensor(ByVal sensor As Boolean)
            If (Me._isSensor <> sensor) Then
                Me._isSensor = sensor
                If (Not Me._body Is Nothing) Then
                    Dim conactList As ContactEdge = Me._body.ConactList
                    Do While (conactList IsNot Nothing)
                        Dim contact As Contact = conactList.Contact
                        Dim fixtureA As Fixture = contact.GetFixtureA
                        Dim fixtureB As Fixture = contact.GetFixtureB
                        If ((fixtureA Is Me) OrElse (fixtureB Is Me)) Then
                            contact.SetSolid(Not Me._isSensor)
                        End If
                    Loop
                End If
            End If
        End Sub

        Public Sub SetUserData(ByVal data As Object)
            Me._userData = data
        End Sub

        Friend Sub Synchronize(ByVal broadPhase As BroadPhase, ByRef transform1 As XForm, ByRef transform2 As XForm)
            If (Me._proxyId <> BroadPhase.NullProxy) Then
                Dim aabb As AABB
                Dim aabb2 As AABB
                Me._shape.ComputeAABB(aabb, transform1)
                Me._shape.ComputeAABB(aabb2, transform2)
                Dim aabb3 As New AABB
                aabb3.Combine(aabb, aabb2)
                broadPhase.MoveProxy(Me._proxyId, aabb3)
            End If
        End Sub

        Public Function TestPoint(ByVal p As Vector2) As Boolean
            Dim form As XForm
            Me._body.GetXForm(form)
            Return Me._shape.TestPoint(form, p)
        End Function

        Public Function TestSegment(<Out> ByRef lambda As Single, <Out> ByRef normal As Vector2, ByRef segment As Segment, ByVal maxLambda As Single) As SegmentCollide
            Dim form As XForm
            Me._body.GetXForm(form)
            Return Me._shape.TestSegment(form, lambda, normal, segment, maxLambda)
        End Function


        ' Properties
        Public ReadOnly Property ShapeType As ShapeType
            Get
                Return Me._shape.ShapeType
            End Get
        End Property


        ' Fields
        Friend _body As Body = Nothing
        Friend _density As Single
        Friend _filter As Filter
        Friend _friction As Single
        Friend _isSensor As Boolean
        Friend _next As Fixture = Nothing
        Friend _proxyId As Integer = BroadPhase.NullProxy
        Friend _restitution As Single
        Friend _shape As Shape = Nothing
        Friend _userData As Object = Nothing
    End Class
End Namespace

