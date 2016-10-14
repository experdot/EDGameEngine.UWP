Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    Public Class Body

        Friend Sub New(ByVal bd As BodyDef, ByVal world As World)
            If bd.isBullet Then
                Me._flags = (Me._flags Or BodyFlags.Bullet)
            End If
            If bd.fixedRotation Then
                Me._flags = (Me._flags Or BodyFlags.FixedRotation)
            End If
            If bd.allowSleep Then
                Me._flags = (Me._flags Or BodyFlags.AllowSleep)
            End If
            If bd.isSleeping Then
                Me._flags = (Me._flags Or BodyFlags.Sleep)
            End If
            Me._world = world
            Me._xf.Position = bd.position
            Me._xf.RoateMatrix.SetValue(bd.angle)
            Me._sweep.localCenter = bd.massData.Centroid
            Me._sweep.t0 = 1.0!
            Me._sweep.a0 = Me._sweep.a.SetValue(bd.angle)
            Me._sweep.c0 = Me._sweep.c.SetValue(MathUtils.Multiply(Me._xf, Me._sweep.localCenter))
            Me._jointList = Nothing
            Me._contactList = Nothing
            Me._prev = Nothing
            Me._next = Nothing
            Me._linearVelocity = bd.linearVelocity
            Me._angularVelocity = bd.angularVelocity
            Me._linearDamping = bd.linearDamping
            Me._angularDamping = bd.angularDamping
            Me._force = New Vector2(0!, 0!)
            Me._torque = 0!
            Me._linearVelocity = Vector2.Zero
            Me._angularVelocity = 0!
            Me._sleepTime = 0!
            Me._invMass = 0!
            Me._I = 0!
            Me._invI = 0!
            Me._mass = bd.massData.Mass
            If (Me._mass > 0!) Then
                Me._invMass = (1.0! / Me._mass)
            End If
            Me._I = bd.massData.InertiaMoment
            If ((Me._I > 0!) AndAlso ((Me._flags And BodyFlags.FixedRotation) = BodyFlags.None)) Then
                Me._invI = (1.0! / Me._I)
            End If
            If ((Me._invMass = 0!) AndAlso (Me._invI = 0!)) Then
                Me._type = BodyType.Static
            Else
                Me._type = BodyType.Dynamic
            End If
            Me._userData = bd.userData
            Me._fixtureList = Nothing
            Me._fixtureCount = 0
        End Sub

        Friend Sub Advance(ByVal t As Single)
            Me._sweep.Advance(t)
            Me._sweep.c = Me._sweep.c0
            Me._sweep.a = Me._sweep.a0
            Me.SynchronizeTransform()
        End Sub

        Public Sub AllowSleeping(ByVal flag As Boolean)
            If flag Then
                Me._flags = (Me._flags Or BodyFlags.AllowSleep)
            Else
                Me._flags = (Me._flags And Not BodyFlags.AllowSleep)
                Me.WakeUp()
            End If
        End Sub

        Public Sub ApplyForce(ByVal force As Vector2, ByVal point As Vector2)
            If Me.IsSleeping Then
                Me.WakeUp()
            End If
            Me._force = (Me._force + force)
            Me._torque = (Me._torque + MathUtils.Cross((point - Me._sweep.c), force))
        End Sub

        Public Sub ApplyImpulse(ByVal impulse As Vector2, ByVal point As Vector2)
            If Me.IsSleeping Then
                Me.WakeUp()
            End If
            Me._linearVelocity = (Me._linearVelocity + (Me._invMass * impulse))
            Me._angularVelocity = (Me._angularVelocity + (Me._invI * MathUtils.Cross((point - Me._sweep.c), impulse)))
        End Sub

        Public Sub ApplyTorque(ByVal torque As Single)
            If Me.IsSleeping Then
                Me.WakeUp()
            End If
            Me._torque = (Me._torque + torque)
        End Sub

        Public Function CreateFixture(ByVal def As FixtureDef) As Fixture
            Debug.Assert(Not Me._world.IsLocked)
            If Me._world.IsLocked Then
                Return Nothing
            End If
            Dim broadPhase As BroadPhase = Me._world._contactManager._broadPhase
            Dim fixture As New Fixture
            fixture.Create(broadPhase, Me, Me._xf, def)
            fixture._next = Me._fixtureList
            Me._fixtureList = fixture
            Me._fixtureCount += 1
            fixture._body = Me
            Me._world._flags = (Me._world._flags Or WorldFlags.NewFixture)
            Return fixture
        End Function

        Public Function CreateFixture(ByVal shape As Shape, ByVal density As Single) As Fixture
            Debug.Assert(Not Me._world.IsLocked)
            If Me._world.IsLocked Then
                Return Nothing
            End If
            Dim broadPhase As BroadPhase = Me._world._contactManager._broadPhase
            Dim def As New FixtureDef With {
                .shape = shape,
                .density = density
            }
            Dim fixture As New Fixture
            fixture.Create(broadPhase, Me, Me._xf, def)
            fixture._next = Me._fixtureList
            Me._fixtureList = fixture
            Me._fixtureCount += 1
            fixture._body = Me
            Me._world._flags = (Me._world._flags Or WorldFlags.NewFixture)
            Return fixture
        End Function

        Public Sub DestroyFixture(ByVal fixture As Fixture)
            Debug.Assert(Not Me._world.IsLocked)
            If Not Me._world.IsLocked Then
                Debug.Assert((fixture._body Is Me))
                Debug.Assert((Me._fixtureCount > 0))
                Dim fixture2 As Fixture = Me._fixtureList
                Dim flag As Boolean = False
                Do While (fixture2 IsNot Nothing)
                    If (fixture2 Is fixture) Then
                        Me._fixtureList = fixture._next
                        flag = True
                        Exit Do
                    End If
                    fixture2 = fixture2._next
                Loop
                Debug.Assert(flag)
                Dim [next] As ContactEdge = Me._contactList
                Do While ([next] IsNot Nothing)
                    Dim c As Contact = [next].Contact
                    [next] = [next].Next
                    Dim fixtureA As Fixture = c.GetFixtureA
                    Dim fixtureB As Fixture = c.GetFixtureB
                    If ((fixture Is fixtureA) OrElse (fixture Is fixtureB)) Then
                        Me._world._contactManager.Destroy(c)
                    End If
                Loop
                Dim broadPhase As BroadPhase = Me._world._contactManager._broadPhase
                fixture.Destroy(broadPhase)
                fixture._body = Nothing
                fixture._next = Nothing
                Me._fixtureCount -= 1
            End If
        End Sub

        Public ReadOnly Property Angle As Single
            Get
                Return Me._sweep.a
            End Get
        End Property

        Public ReadOnly Property AngularDamping As Single
            Get
                Return Me._angularDamping
            End Get
        End Property

        Public Property AngularVelocity As Single
            Get
                Return Me._angularVelocity
            End Get
            Set(value As Single)
                Me._angularVelocity = value
            End Set
        End Property

        Public ReadOnly Property ConactList As ContactEdge
            Get
                Return Me._contactList
            End Get
        End Property

        Public ReadOnly Property FixtureList As Fixture
            Get
                Return Me._fixtureList
            End Get
        End Property

        Public ReadOnly Property Inertia As Single
            Get
                Return Me._I
            End Get
        End Property

        Public ReadOnly Property JointList As JointEdge
            Get
                Return Me._jointList
            End Get
        End Property

        Public Property LinearDamping As Single
            Get
                Return Me._linearDamping
            End Get
            Set(value As Single)
                Me._angularDamping = value
            End Set
        End Property

        Public Property LinearVelocity As Vector2
            Get
                Return Me._linearVelocity
            End Get
            Set
                Me._linearVelocity = Value
            End Set
        End Property

        Public Function GetLinearVelocityFromLocalPoint(ByVal localPoint As Vector2) As Vector2
            Return Me.GetLinearVelocityFromWorldPoint(Me.GetWorldPoint(localPoint))
        End Function

        Public Function GetLinearVelocityFromWorldPoint(ByVal worldPoint As Vector2) As Vector2
            Return (Me._linearVelocity + MathUtils.Cross(Me._angularVelocity, (worldPoint - Me._sweep.c)))
        End Function

        Public Function GetLocalCenter() As Vector2
            Return Me._sweep.localCenter
        End Function

        Public Function GetLocalPoint(ByVal worldPoint As Vector2) As Vector2
            Return MathUtils.MultiplyT(Me._xf, worldPoint)
        End Function

        Public Function GetLocalVector(ByVal worldVector As Vector2) As Vector2
            Return MathUtils.MultiplyT(Me._xf.RoateMatrix, worldVector)
        End Function

        Public Function GetMass() As Single
            Return Me._mass
        End Function

        Public Function GetMassData() As MassData
            Dim data As MassData
            data.Mass = Me._mass
            data.InertiaMoment = Me._I
            data.Centroid = Me.WorldCenter
            Return data
        End Function

        Public Function GetNext() As Body
            Return Me._next
        End Function

        Public ReadOnly Property Position As Vector2
            Get
                Return Me._xf.Position
            End Get
        End Property

        Public Property UserData As Object
            Get
                Return _userData
            End Get
            Set(value As Object)
                _userData = value
            End Set
        End Property

        Public ReadOnly Property World As World
            Get
                Return Me._world
            End Get
        End Property

        Public ReadOnly Property WorldCenter As Vector2
            Get
                Return Me._sweep.c
            End Get
        End Property

        Public ReadOnly Property GetWorldPoint(ByVal localPoint As Vector2) As Vector2
            Get
                Return MathUtils.Multiply(Me._xf, localPoint)
            End Get
        End Property

        Public Function GetWorldVector(ByVal localVector As Vector2) As Vector2
            Return MathUtils.Multiply(Me._xf.RoateMatrix, localVector)
        End Function

        Public Sub GetXForm(<Out> ByRef xf As XForm)
            xf = Me._xf
        End Sub

        Friend Function IsConnected(ByVal other As Body) As Boolean
            Dim edge As JointEdge = Me._jointList
            Do While (edge IsNot Nothing)
                If (edge.Other Is other) Then
                    Return Not edge.Joint._collideConnected
                End If
                edge = edge.Next
            Loop
            Return False
        End Function

        Public Sub PutToSleep()
            Me._flags = (Me._flags Or BodyFlags.Sleep)
            Me._sleepTime = 0!
            Me._linearVelocity = Vector2.Zero
            Me._angularVelocity = 0!
            Me._force = Vector2.Zero
            Me._torque = 0!
        End Sub

        Public Sub SetBullet(ByVal flag As Boolean)
            If flag Then
                Me._flags = (Me._flags Or BodyFlags.Bullet)
            Else
                Me._flags = (Me._flags And Not BodyFlags.Bullet)
            End If
        End Sub

        Public Sub SetMassData(ByVal massData As MassData)
            Debug.Assert(Not Me._world.IsLocked)
            If Not Me._world.IsLocked Then
                Me._invMass = 0!
                Me._I = 0!
                Me._invI = 0!
                Me._mass = massData.Mass
                If (Me._mass > 0!) Then
                    Me._invMass = (1.0! / Me._mass)
                End If
                Me._I = massData.InertiaMoment
                If ((Me._I > 0!) AndAlso ((Me._flags And BodyFlags.FixedRotation) = BodyFlags.None)) Then
                    Me._invI = (1.0! / Me._I)
                End If
                Me._sweep.localCenter = massData.Centroid
                Me._sweep.c0 = Me._sweep.c.SetValue(MathUtils.Multiply(Me._xf, Me._sweep.localCenter))
                Dim type As BodyType = Me._type
                If ((Me._invMass = 0!) AndAlso (Me._invI = 0!)) Then
                    Me._type = BodyType.Static
                Else
                    Me._type = BodyType.Dynamic
                End If
                If (type <> Me._type) Then
                    Dim edge As ContactEdge = Me._contactList
                    Do While (edge IsNot Nothing)
                        edge.Contact.FlagForFiltering()
                        edge = edge.Next
                    Loop
                End If
            End If
        End Sub

        Public Sub SetMassFromShapes()
            Debug.Assert(Not Me._world.IsLocked)
            If Not Me._world.IsLocked Then
                Me._mass = 0!
                Me._invMass = 0!
                Me._I = 0!
                Me._invI = 0!
                Dim vector As Vector2 = Vector2.Zero
                Dim fixture As Fixture = Me._fixtureList
                Do While (fixture IsNot Nothing)
                    Dim data As MassData
                    fixture.ComputeMass(data)
                    Me._mass = (Me._mass + data.Mass)
                    vector = (vector + (data.Mass * data.Centroid))
                    Me._I = (Me._I + data.InertiaMoment)
                    fixture = fixture._next
                Loop
                If (Me._mass > 0!) Then
                    Me._invMass = (1.0! / Me._mass)
                    vector = (vector * Me._invMass)
                End If
                If ((Me._I > 0!) AndAlso ((Me._flags And BodyFlags.FixedRotation) = BodyFlags.None)) Then
                    Me._I = (Me._I - (Me._mass * Vector2.Dot(vector, vector)))
                    Debug.Assert((Me._I > 0!))
                    Me._invI = (1.0! / Me._I)
                Else
                    Me._I = 0!
                    Me._invI = 0!
                End If
                Me._sweep.localCenter = vector
                Me._sweep.c0 = Me._sweep.c.SetValue(MathUtils.Multiply(Me._xf, Me._sweep.localCenter))
                Dim type As BodyType = Me._type
                If ((Me._invMass = 0!) AndAlso (Me._invI = 0!)) Then
                    Me._type = BodyType.Static
                Else
                    Me._type = BodyType.Dynamic
                End If
                If (type <> Me._type) Then
                    Dim edge As ContactEdge = Me._contactList
                    Do While (edge IsNot Nothing)
                        edge.Contact.FlagForFiltering()
                        edge = edge.Next
                    Loop
                End If
            End If
        End Sub

        Public Sub SetXForm(ByVal position As Vector2, ByVal angle As Single)
            Debug.Assert(Not Me._world.IsLocked)
            If Not Me._world.IsLocked Then
                Me._xf.RoateMatrix.SetValue(angle)
                Me._xf.Position = position
                Me._sweep.c0 = Me._sweep.c.SetValue(MathUtils.Multiply(Me._xf, Me._sweep.localCenter))
                Me._sweep.a0 = Me._sweep.a.SetValue(angle)
                Dim broadPhase As BroadPhase = Me._world._contactManager._broadPhase
                Dim fixture As Fixture = Me._fixtureList
                Do While (fixture IsNot Nothing)
                    fixture.Synchronize(broadPhase, Me._xf, Me._xf)
                    fixture = fixture._next
                Loop
                Me._world._contactManager.FindNewContacts()
            End If
        End Sub

        Friend Sub SynchronizeFixtures()
            Dim form As New XForm
            form.RoateMatrix.SetValue(Me._sweep.a0)
            form.Position = (Me._sweep.c0 - MathUtils.Multiply(form.RoateMatrix, Me._sweep.localCenter))
            Dim broadPhase As BroadPhase = Me._world._contactManager._broadPhase
            Dim fixture As Fixture = Me._fixtureList
            Do While (fixture IsNot Nothing)
                fixture.Synchronize(broadPhase, form, Me._xf)
                fixture = fixture._next
            Loop
        End Sub

        Friend Sub SynchronizeTransform()
            Me._xf.RoateMatrix.SetValue(Me._sweep.a)
            Me._xf.Position = (Me._sweep.c - MathUtils.Multiply(Me._xf.RoateMatrix, Me._sweep.localCenter))
        End Sub

        Public Sub WakeUp()
            Me._flags = (Me._flags And Not BodyFlags.Sleep)
            Me._sleepTime = 0!
        End Sub

        Public ReadOnly Property IsAllowSleeping As Boolean
            Get
                Return ((Me._flags And BodyFlags.AllowSleep) = BodyFlags.AllowSleep)
            End Get
        End Property

        Public ReadOnly Property IsBullet As Boolean
            Get
                Return ((Me._flags And BodyFlags.Bullet) = BodyFlags.Bullet)
            End Get
        End Property

        Public ReadOnly Property IsDynamic As Boolean
            Get
                Return (Me._type = BodyType.Dynamic)
            End Get
        End Property

        Public ReadOnly Property IsFrozen As Boolean
            Get
                Return ((Me._flags And BodyFlags.Frozen) = BodyFlags.Frozen)
            End Get
        End Property

        Public ReadOnly Property IsSleeping As Boolean
            Get
                Return ((Me._flags And BodyFlags.Sleep) = BodyFlags.Sleep)
            End Get
        End Property

        Public ReadOnly Property IsStatic As Boolean
            Get
                Return (Me._type = BodyType.Static)
            End Get
        End Property

        Friend _angularDamping As Single
        Friend _angularVelocity As Single
        Friend _contactList As ContactEdge
        Friend _fixtureCount As Integer
        Friend _fixtureList As Fixture
        Friend _flags As BodyFlags = BodyFlags.None
        Friend _force As Vector2
        Friend _I As Single
        Friend _invI As Single
        Friend _invMass As Single
        Friend _islandIndex As Integer
        Friend _jointList As JointEdge
        Friend _linearDamping As Single
        Friend _linearVelocity As Vector2
        Friend _mass As Single
        Friend _next As Body
        Friend _prev As Body
        Friend _sleepTime As Single
        Friend _sweep As Sweep
        Friend _torque As Single
        Friend _type As BodyType
        Friend _userData As Object
        Friend _world As World
        Friend _xf As XForm
    End Class
End Namespace

