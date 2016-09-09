Imports EDGameEngine
Imports Windows.UI

Namespace Global.Box2D
    Public Class World

        ' Methods
        Public Sub New(ByVal gravity As Vector2, ByVal doSleep As Boolean)
            Me.WarmStarting = True
            Me.ContinuousPhysics = True
            Me._allowSleep = doSleep
            Me.Gravity = gravity
        End Sub

        Public Function CreateBody(ByVal def As BodyDef) As Body
            Debug.Assert(Not Me.IsLocked)
            If Me.IsLocked Then
                Return Nothing
            End If
            Dim body As New Body(def, Me) With {
                ._prev = Nothing,
                ._next = Me._bodyList
            }
            If (Me._bodyList IsNot Nothing) Then
                Me._bodyList._prev = body
            End If
            Me._bodyList = body
            Me._bodyCount += 1
            Return body
        End Function

        Public Function CreateJoint(ByVal def As JointDef) As Joint
            Debug.Assert(Not Me.IsLocked)
            If Me.IsLocked Then
                Return Nothing
            End If
            Dim joint As Joint = Joint.Create(def)
            joint._prev = Nothing
            joint._next = Me._jointList
            If (Me._jointList IsNot Nothing) Then
                Me._jointList._prev = joint
            End If
            Me._jointList = joint
            Me._jointCount += 1
            joint._edgeA.Joint = joint
            joint._edgeA.Other = joint._bodyB
            joint._edgeA.Prev = Nothing
            joint._edgeA.Next = joint._bodyA._jointList
            If (joint._bodyA._jointList IsNot Nothing) Then
                joint._bodyA._jointList.Prev = joint._edgeA
            End If
            joint._bodyA._jointList = joint._edgeA
            joint._edgeB.Joint = joint
            joint._edgeB.Other = joint._bodyA
            joint._edgeB.Prev = Nothing
            joint._edgeB.Next = joint._bodyB._jointList
            If (joint._bodyB._jointList IsNot Nothing) Then
                joint._bodyB._jointList.Prev = joint._edgeB
            End If
            joint._bodyB._jointList = joint._edgeB
            Dim a As Body = def.body1
            Dim b As Body = def.body2
            Dim isStatic As Boolean = a.IsStatic
            Dim flag2 As Boolean = b.IsStatic
            If (Not def.collideConnected AndAlso (Not isStatic OrElse Not flag2)) Then
                If flag2 Then
                    MathUtils.Swap(Of Body)(a, b)
                End If
                Dim edge As ContactEdge = b.ConactList
                Do While (edge IsNot Nothing)
                    If (edge.Other Is a) Then
                        edge.Contact.FlagForFiltering()
                    End If
                    edge = edge.Next
                Loop
            End If
            Return joint
        End Function

        Public Sub DestroyBody(ByVal b As Body)
            Debug.Assert((Me._bodyCount > 0))
            Debug.Assert(Not Me.IsLocked)
            If Not Me.IsLocked Then
                Dim [next] As JointEdge = b._jointList
                Do While ([next] IsNot Nothing)
                    Dim edge3 As JointEdge = [next]
                    [next] = [next].Next
                    If (Me.DestructionListener IsNot Nothing) Then
                        Me.DestructionListener.SayGoodbye(edge3.Joint)
                    End If
                    Me.DestroyJoint(edge3.Joint)
                Loop
                b._jointList = Nothing
                Dim edge2 As ContactEdge = b._contactList
                Do While (edge2 IsNot Nothing)
                    Dim edge4 As ContactEdge = edge2
                    edge2 = edge2.Next
                    Me._contactManager.Destroy(edge4.Contact)
                Loop
                b._contactList = Nothing
                Dim fixture As Fixture = b._fixtureList
                Do While (fixture IsNot Nothing)
                    Dim fixture2 As Fixture = fixture
                    fixture = fixture._next
                    If (Me.DestructionListener IsNot Nothing) Then
                        Me.DestructionListener.SayGoodbye(fixture2)
                    End If
                    fixture2.Destroy(Me._contactManager._broadPhase)
                Loop
                b._fixtureList = Nothing
                b._fixtureCount = 0
                If (b._prev IsNot Nothing) Then
                    b._prev._next = b._next
                End If
                If (b._next IsNot Nothing) Then
                    b._next._prev = b._prev
                End If
                If (b Is Me._bodyList) Then
                    Me._bodyList = b._next
                End If
                Me._bodyCount -= 1
            End If
        End Sub

        Public Sub DestroyJoint(ByVal j As Joint)
            Debug.Assert(Not Me.IsLocked)
            If Not Me.IsLocked Then
                Dim flag As Boolean = j._collideConnected
                If (j._prev IsNot Nothing) Then
                    j._prev._next = j._next
                End If
                If (j._next IsNot Nothing) Then
                    j._next._prev = j._prev
                End If
                If (j Is Me._jointList) Then
                    Me._jointList = j._next
                End If
                Dim body As Body = j._bodyA
                Dim body2 As Body = j._bodyB
                body.WakeUp()
                body2.WakeUp()

                If (j._edgeA.Prev IsNot Nothing) Then
                    j._edgeA.Prev.Next = j._edgeA.Next
                End If
                If (j._edgeA.Next IsNot Nothing) Then
                    j._edgeA.Next.Prev = j._edgeA.Prev
                End If
                If (j._edgeA Is body._jointList) Then
                    body._jointList = j._edgeA.Next
                End If
                j._edgeA.Prev = Nothing
                j._edgeA.Next = Nothing
                If (j._edgeB.Prev IsNot Nothing) Then
                    j._edgeB.Prev.Next = j._edgeB.Next
                End If
                If (j._edgeB.Next IsNot Nothing) Then
                    j._edgeB.Next.Prev = j._edgeB.Prev
                End If
                If (j._edgeB Is body2._jointList) Then
                    body2._jointList = j._edgeB.Next
                End If
                j._edgeB.Prev = Nothing
                j._edgeB.Next = Nothing
                Debug.Assert((Me._jointCount > 0))
                Me._jointCount -= 1
                If Not flag Then
                    Dim edge As ContactEdge = body2.ConactList
                    Do While (edge IsNot Nothing)
                        If (edge.Other Is body) Then
                            edge.Contact.FlagForFiltering()
                        End If
                        edge = edge.Next
                    Loop
                End If
            End If
        End Sub

        Public Sub DrawDebugData()
            If (Not Me.DebugDraw Is Nothing) Then
                Dim flags As DebugDrawFlags = Me.DebugDraw.Flags
                If ((flags And DebugDrawFlags.Shape) = DebugDrawFlags.Shape) Then
                    Dim body As Body = Me._bodyList
                    Do While (body IsNot Nothing)
                        Dim form As XForm
                        body.GetXForm(form)
                        Dim fixture As Fixture = body.FixtureList
                        Do While (fixture IsNot Nothing)
                            If body.IsStatic Then
                                Me.DrawShape(fixture, form, ColorEx.FromScRgb(0.5!, 0.9!, 0.5!))
                            ElseIf body.IsSleeping Then
                                Me.DrawShape(fixture, form, ColorEx.FromScRgb(0.5!, 0.5!, 0.9!))
                            Else
                                Me.DrawShape(fixture, form, ColorEx.FromScRgb(0.9!, 0.9!, 0.9!))
                            End If
                            fixture = fixture.GetNext
                        Loop
                        body = body.GetNext
                    Loop
                End If
                If ((flags And DebugDrawFlags.Joint) = DebugDrawFlags.Joint) Then
                    Dim joint As Joint = Me._jointList
                    Do While (joint IsNot Nothing)
                        If (joint.JointType <> JointType.Mouse) Then
                            Me.DrawJoint(joint)
                        End If
                        joint = joint.GetNext
                    Loop
                End If
                If ((flags And DebugDrawFlags.Pair) = DebugDrawFlags.Pair) Then
                End If
                If ((flags And DebugDrawFlags.AABB) = DebugDrawFlags.AABB) Then
                    Dim color As Color = ColorEx.FromScRgb(0.9!, 0.3!, 0.9!)
                    Dim phase As BroadPhase = Me._contactManager._broadPhase
                    Dim body2 As Body = Me._bodyList
                    Do While (body2 IsNot Nothing)
                        Dim fixture2 As Fixture = body2.FixtureList
                        Do While (fixture2 IsNot Nothing)
                            Dim aabb As AABB
                            phase.GetAABB(fixture2._proxyId, aabb)
                            Dim vertices As New FixedArray8(Of Vector2)
                            vertices.Item(0) = New Vector2(aabb.lowerBound.X, aabb.lowerBound.Y)
                            vertices.Item(1) = New Vector2(aabb.upperBound.X, aabb.lowerBound.Y)
                            vertices.Item(2) = New Vector2(aabb.upperBound.X, aabb.upperBound.Y)
                            vertices.Item(3) = New Vector2(aabb.lowerBound.X, aabb.upperBound.Y)
                            Me.DebugDraw.DrawPolygon(vertices, 4, color)
                            fixture2 = fixture2.GetNext
                        Loop
                        body2 = body2.GetNext
                    Loop
                End If
                If ((flags And DebugDrawFlags.CenterOfMass) = DebugDrawFlags.CenterOfMass) Then
                    Dim body3 As Body = Me._bodyList
                    Do While (body3 IsNot Nothing)
                        Dim form2 As XForm
                        body3.GetXForm(form2)
                        form2.Position = body3.WorldCenter
                        Me.DebugDraw.DrawXForm(form2)
                        body3 = body3.GetNext
                    Loop
                End If
            End If
        End Sub

        Private Sub DrawJoint(ByVal joint As Joint)
            Dim form As XForm
            Dim form2 As XForm
            Dim body As Body = joint.GetBody1
            Dim body2 As Body = joint.GetBody2
            body.GetXForm(form)
            body2.GetXForm(form2)
            Dim position As Vector2 = form.Position
            Dim vector2 As Vector2 = form2.Position
            Dim vector3 As Vector2 = joint.GetAnchor1
            Dim vector4 As Vector2 = joint.GetAnchor2
            Dim color As Color = ColorEx.FromScRgb(0.5!, 0.8!, 0.8!)
            Select Case joint.JointType
                Case JointType.Distance
                    Me.DebugDraw.DrawSegment(vector3, vector4, color)
                    Exit Select
                Case JointType.Pulley
                    Dim joint2 As PulleyJoint = DirectCast(joint, PulleyJoint)
                    Dim vector5 As Vector2 = joint2.GetGroundAnchor1
                    Dim vector6 As Vector2 = joint2.GetGroundAnchor2
                    Me.DebugDraw.DrawSegment(vector5, vector3, color)
                    Me.DebugDraw.DrawSegment(vector6, vector4, color)
                    Me.DebugDraw.DrawSegment(vector5, vector6, color)
                    Exit Select
                Case JointType.Mouse
                    Exit Select
                Case Else
                    Me.DebugDraw.DrawSegment(position, vector3, color)
                    Me.DebugDraw.DrawSegment(vector3, vector4, color)
                    Me.DebugDraw.DrawSegment(vector2, vector4, color)
                    Exit Select
            End Select
        End Sub

        Private Sub DrawShape(ByVal fixture As Fixture, ByVal xf As XForm, ByVal color As Color)
            Dim color2 As Color = ColorEx.FromScRgb(0.9!, 0.6!, 0.6!)
            Select Case fixture.ShapeType
                Case ShapeType.Circle
                    Dim shape As CircleShape = DirectCast(fixture.GetShape, CircleShape)
                    Dim center As Vector2 = MathUtils.Multiply(xf, shape._p)
                    Dim radius As Single = shape._radius
                    Dim axis As Vector2 = xf.R.col1
                    Me.DebugDraw.DrawSolidCircle(center, radius, axis, color)
                    Exit Select
                Case ShapeType.Polygon
                    Dim shape2 As PolygonShape = DirectCast(fixture.GetShape, PolygonShape)
                    Dim count As Integer = shape2._vertexCount
                    Debug.Assert((count <= Settings.b2_maxPolygonVertices))
                    Dim vertices As New FixedArray8(Of Vector2)
                    Dim i As Integer
                    For i = 0 To count - 1
                        vertices.Item(i) = MathUtils.Multiply(xf, shape2._vertices.Item(i))
                    Next i
                    Me.DebugDraw.DrawSolidPolygon(vertices, count, color)
                    Exit Select
            End Select
        End Sub

        Public Function GetBodyList() As Body
            Return Me._bodyList
        End Function

        Public Function GetContactList() As Contact
            Return Me._contactManager._contactList
        End Function

        Public Function GetJointList() As Joint
            Return Me._jointList
        End Function

        Public Sub Query(ByVal callback As Func(Of Fixture, Boolean), ByRef aabb As AABB)
            Me._contactManager._broadPhase.Query(Of Fixture)(callback, aabb)
        End Sub

        Private Sub Solve(ByRef [step] As TimeStep)
            Me._island.Reset(Me._bodyCount, Me._contactManager._contactCount, Me._jointCount, Me._contactManager.ContactListener)
            Dim body As Body = Me._bodyList
            Do While (body IsNot Nothing)
                body._flags = (body._flags And Not BodyFlags.Island)
                body = body._next
            Loop
            Dim contact As Contact = Me._contactManager._contactList
            Do While (contact IsNot Nothing)
                contact._flags = (contact._flags And Not ContactFlags.Island)
                contact = contact._next
            Loop
            Dim joint As Joint = Me._jointList
            Do While (joint IsNot Nothing)
                joint._islandFlag = False
                joint = joint._next
            Loop
            Dim num As Integer = Me._bodyCount
            Dim bodyArray As Body() = New Body(Me._bodyCount - 1) {}
            Dim body2 As Body = Me._bodyList
            Do While (body2 IsNot Nothing)
                If (((body2._flags And (BodyFlags.Sleep Or (BodyFlags.Island Or BodyFlags.Frozen))) <= BodyFlags.None) AndAlso Not body2.IsStatic) Then
                    Me._island.Clear()
                    Dim num2 As Integer = 0
                    bodyArray(num2) = body2
                    num2 += 1
                    body2._flags = (body2._flags Or BodyFlags.Island)
                    Do While (num2 > 0)
                        num2 = num2 - 1
                        Dim body3 As Body = bodyArray(num2)
                        Me._island.Add(body3)
                        body3._flags = (body3._flags And Not BodyFlags.Sleep)
                        If Not body3.IsStatic Then
                            Dim edge As ContactEdge = body3._contactList
                            Do While (edge IsNot Nothing)
                                If (((edge.Contact._flags And (ContactFlags.Island Or ContactFlags.NonSolid)) <= ContactFlags.None) AndAlso ((edge.Contact._flags And ContactFlags.Touch) <> ContactFlags.None)) Then
                                    Me._island.Add(edge.Contact)
                                    edge.Contact._flags = (edge.Contact._flags Or ContactFlags.Island)
                                    Dim other As Body = edge.Other
                                    If ((other._flags And BodyFlags.Island) <= BodyFlags.None) Then
                                        Debug.Assert((num2 < num))
                                        bodyArray(num2) = other
                                        num2 += 1
                                        other._flags = (other._flags Or BodyFlags.Island)
                                    End If
                                End If
                                edge = edge.Next
                            Loop
                            Dim edge2 As JointEdge = body3._jointList
                            Do While (edge2 IsNot Nothing)
                                If Not edge2.Joint._islandFlag Then
                                    Me._island.Add(edge2.Joint)
                                    edge2.Joint._islandFlag = True
                                    Dim body5 As Body = edge2.Other
                                    If ((body5._flags And BodyFlags.Island) <= BodyFlags.None) Then
                                        Debug.Assert((num2 < num))
                                        bodyArray(num2) = body5
                                        num2 += 1
                                        body5._flags = (body5._flags Or BodyFlags.Island)
                                    End If
                                End If
                                edge2 = edge2.Next
                            Loop
                        End If
                    Loop
                    Me._island.Solve([step], Me.Gravity, Me._allowSleep)
                    Dim i As Integer
                    For i = 0 To Me._island._bodyCount - 1
                        Dim body6 As Body = Me._island._bodies(i)
                        If body6.IsStatic Then
                            body6._flags = (body6._flags And Not BodyFlags.Island)
                        End If
                    Next i
                End If
                body2 = body2._next
            Loop
            Dim body7 As Body = Me._bodyList
            Do While (body7 IsNot Nothing)
                If (((body7._flags And (BodyFlags.Sleep Or BodyFlags.Frozen)) <= BodyFlags.None) AndAlso Not body7.IsStatic) Then
                    body7.SynchronizeFixtures()
                End If
                body7 = body7.GetNext
            Loop
            Me._contactManager.FindNewContacts()
        End Sub

        Private Sub SolveTOI(ByRef [step] As TimeStep)
            Me._island.Reset(Me._bodyCount, Settings.b2_maxTOIContactsPerIsland, Settings.b2_maxTOIJointsPerIsland, Me._contactManager.ContactListener)
            Dim num As Integer = Me._bodyCount
            Dim bodyArray As Body() = New Body(Me._bodyCount - 1) {}
            Dim body As Body = Me._bodyList
            Do While (body IsNot Nothing)
                body._flags = (body._flags And Not BodyFlags.Island)
                body._sweep.t0 = 0!
                body = body._next
            Loop
            Dim contact As Contact = Me._contactManager._contactList
            Do While (contact IsNot Nothing)
                contact._flags = (contact._flags And Not (ContactFlags.Toi Or ContactFlags.Island))
                contact = contact._next
            Loop
            Dim joint As Joint = Me._jointList
            Do While (joint IsNot Nothing)
                joint._islandFlag = False
                joint = joint._next
            Loop
            Do While True
                Dim contact2 As Contact = Nothing
                Dim t As Single = 1.0!
                Dim contact3 As Contact = Me._contactManager._contactList
                Do While (contact3 IsNot Nothing)
                    If ((contact3._flags And (ContactFlags.Slow Or ContactFlags.NonSolid)) <= ContactFlags.None) Then
                        Dim num6 As Single = 1.0!
                        If ((contact3._flags And ContactFlags.Toi) > ContactFlags.None) Then
                            num6 = contact3._toi
                        Else
                            Dim fixture3 As Fixture = contact3.GetFixtureA
                            Dim fixture4 As Fixture = contact3.GetFixtureB
                            Dim body5 As Body = fixture3.GetBody
                            Dim body6 As Body = fixture4.GetBody
                            If ((body5.IsStatic OrElse body5.IsSleeping) AndAlso (body6.IsStatic OrElse body6.IsSleeping)) Then
                                Continue Do
                            End If
                            Dim num7 As Single = body5._sweep.t0
                            If (body5._sweep.t0 < body6._sweep.t0) Then
                                num7 = body6._sweep.t0
                                body5._sweep.Advance(num7)
                            ElseIf (body6._sweep.t0 < body5._sweep.t0) Then
                                num7 = body5._sweep.t0
                                body6._sweep.Advance(num7)
                            End If
                            Debug.Assert((num7 < 1.0!))
                            num6 = contact3.ComputeTOI(body5._sweep, body6._sweep)
                            Debug.Assert(((0! <= num6) AndAlso (num6 <= 1.0!)))
                            If ((0! < num6) AndAlso (num6 < 1.0!)) Then
                                num6 = Math.Min((((1.0! - num6) * num7) + num6), 1.0!)
                            End If
                            contact3._toi = num6
                            contact3._flags = (contact3._flags Or ContactFlags.Toi)
                        End If
                        If ((Settings.b2_FLT_EPSILON < num6) AndAlso (num6 < t)) Then
                            contact2 = contact3
                            t = num6
                        End If
                    End If
                    contact3 = contact3._next
                Loop
                If ((contact2 Is Nothing) OrElse ((1.0! - (100.0! * Settings.b2_FLT_EPSILON)) < t)) Then
                    Return
                End If
                Dim fixtureA As Fixture = contact2.GetFixtureA
                Dim fixtureB As Fixture = contact2.GetFixtureB
                Dim body2 As Body = fixtureA.GetBody
                Dim body3 As Body = fixtureB.GetBody
                body2.Advance(t)
                body3.Advance(t)
                contact2.Update(Me._contactManager.ContactListener)
                contact2._flags = (contact2._flags And Not ContactFlags.Toi)
                If ((contact2._flags And ContactFlags.Touch) <> ContactFlags.None) Then
                    Dim step2 As TimeStep
                    Dim body4 As Body = body2
                    If body4.IsStatic Then
                        body4 = body3
                    End If
                    Me._island.Clear()
                    Dim num3 As Integer = 0
                    Dim num4 As Integer = 0
                    bodyArray((num3 + num4)) = body4
                    num4 += 1
                    body4._flags = (body4._flags Or BodyFlags.Island)
                    Do While (num4 > 0)
                        Dim body7 As Body = bodyArray(num3)
                        num3 += 1
                        num4 -= 1
                        Me._island.Add(body7)
                        body7._flags = (body7._flags And Not BodyFlags.Sleep)
                        If Not body7.IsStatic Then
                            Dim edge As ContactEdge = body7._contactList
                            Do While (edge IsNot Nothing)
                                If (((Me._island._contacts.Count <> Me._island._contactCapacity) AndAlso ((edge.Contact._flags And (ContactFlags.Island Or (ContactFlags.Slow Or ContactFlags.NonSolid))) <= ContactFlags.None)) AndAlso ((edge.Contact._flags And ContactFlags.Touch) <> ContactFlags.None)) Then
                                    Me._island.Add(edge.Contact)
                                    edge.Contact._flags = (edge.Contact._flags Or ContactFlags.Island)
                                    Dim other As Body = edge.Other
                                    If ((other._flags And BodyFlags.Island) <= BodyFlags.None) Then
                                        If Not other.IsStatic Then
                                            other.Advance(t)
                                            other.WakeUp()
                                        End If
                                        Debug.Assert(((num3 + num4) < num))
                                        bodyArray((num3 + num4)) = other
                                        num4 += 1
                                        other._flags = (other._flags Or BodyFlags.Island)
                                    End If
                                End If
                                edge = edge.Next
                            Loop
                            Dim edge2 As JointEdge = body7._jointList
                            Do While (edge2 IsNot Nothing)
                                If ((Me._island._jointCount <> Me._island._jointCapacity) AndAlso Not edge2.Joint._islandFlag) Then
                                    Me._island.Add(edge2.Joint)
                                    edge2.Joint._islandFlag = True
                                    Dim body9 As Body = edge2.Other
                                    If ((body9._flags And BodyFlags.Island) <= BodyFlags.None) Then
                                        If Not body9.IsStatic Then
                                            body9.Advance(t)
                                            body9.WakeUp()
                                        End If
                                        Debug.Assert(((num3 + num4) < num))
                                        bodyArray((num3 + num4)) = body9
                                        num4 += 1
                                        body9._flags = (body9._flags Or BodyFlags.Island)
                                    End If
                                End If
                                edge2 = edge2.Next
                            Loop
                        End If
                    Loop
                    step2.warmStarting = False
                    step2.dt = ((1.0! - t) * [step].dt)
                    step2.inv_dt = (1.0! / step2.dt)
                    step2.dtRatio = 0!
                    step2.velocityIterations = [step].velocityIterations
                    step2.positionIterations = [step].positionIterations
                    Me._island.SolveTOI(step2)
                    Dim i As Integer
                    For i = 0 To Me._island._bodyCount - 1
                        Dim body10 As Body = Me._island._bodies(i)
                        body10._flags = (body10._flags And Not BodyFlags.Island)
                        If (((body10._flags And (BodyFlags.Sleep Or BodyFlags.Frozen)) <= BodyFlags.None) AndAlso Not body10.IsStatic) Then
                            body10.SynchronizeFixtures()
                            Dim edge3 As ContactEdge = body10._contactList
                            Do While (edge3 IsNot Nothing)
                                edge3.Contact._flags = (edge3.Contact._flags And Not ContactFlags.Toi)
                                edge3 = edge3.Next
                            Loop
                        End If
                    Next i
                    Dim num5 As Integer = Me._island._contacts.Count
                    Dim j As Integer
                    For j = 0 To num5 - 1
                        Dim local1 As Contact = Me._island._contacts.Item(j)
                        local1._flags = (local1._flags And Not (ContactFlags.Toi Or ContactFlags.Island))
                    Next j
                    Dim k As Integer
                    For k = 0 To Me._island._jointCount - 1
                        Dim joint2 As Joint = Me._island._joints(k)
                        joint2._islandFlag = False
                    Next k
                    Me._contactManager.FindNewContacts()
                End If
            Loop
        End Sub

        Public Sub [Step](ByVal dt As Single, ByVal velocityIterations As Integer, ByVal positionIterations As Integer)
            Dim [step] As TimeStep
            Dim num As Integer = Me._contactManager._broadPhase.ComputeHeight
            If ((Me._flags And WorldFlags.NewFixture) = WorldFlags.NewFixture) Then
                Me._contactManager.FindNewContacts()
                Me._flags = (Me._flags And Not WorldFlags.NewFixture)
            End If
            Me._flags = (Me._flags Or WorldFlags.Locked)
            [step].dt = dt
            [step].velocityIterations = velocityIterations
            [step].positionIterations = positionIterations
            If (dt > 0!) Then
                [step].inv_dt = (1.0! / dt)
            Else
                [step].inv_dt = 0!
            End If
            [step].dtRatio = (Me._inv_dt0 * dt)
            [step].warmStarting = Me.WarmStarting
            Me._contactManager.Collide()

            If ([step].dt > 0!) Then
                Me.Solve([step])
            End If
            If (Me.ContinuousPhysics AndAlso ([step].dt > 0!)) Then
                Me.SolveTOI([step])
            End If
            If ([step].dt > 0!) Then
                Me._inv_dt0 = [step].inv_dt
            End If
            Me._flags = (Me._flags And Not WorldFlags.Locked)
        End Sub


        ' Properties
        Public ReadOnly Property BodyCount As Integer
            Get
                Return Me._bodyCount
            End Get
        End Property

        Public ReadOnly Property ContactCount As Integer
            Get
                Return Me._contactManager._contactCount
            End Get
        End Property

        Public Property ContactFilter As IContactFilter
            Get
                Return Me._contactManager.ContactFilter
            End Get
            Set(ByVal value As IContactFilter)
                Me._contactManager.ContactFilter = value
            End Set
        End Property

        Public Property ContactListener As IContactListener
            Get
                Return Me._contactManager.ContactListener
            End Get
            Set(ByVal value As IContactListener)
                Me._contactManager.ContactListener = value
            End Set
        End Property

        Public Property ContinuousPhysics As Boolean
        Public Property DebugDraw As DebugDraw
        Public Property DestructionListener As IDestructionListener
        Public Property Gravity As Vector2
        Public Property IsLocked As Boolean
            Get
                Return ((Me._flags And WorldFlags.Locked) = WorldFlags.Locked)
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me._flags = (Me._flags Or WorldFlags.Locked)
                Else
                    Me._flags = (Me._flags And Not WorldFlags.Locked)
                End If
            End Set
        End Property

        Public ReadOnly Property JointCount As Integer
            Get
                Return Me._jointCount
            End Get
        End Property

        Public ReadOnly Property ProxyCount As Integer
            Get
                Return Me._contactManager._broadPhase.ProxyCount
            End Get
        End Property

        Public Property WarmStarting As Boolean

        ' Fields
        Friend _allowSleep As Boolean
        Friend _bodyCount As Integer
        Friend _bodyList As Body
        Friend _contactManager As ContactManager = New ContactManager
        Friend _flags As WorldFlags
        Friend _groundBody As Body
        Friend _inv_dt0 As Single
        Friend _island As Island = New Island
        Friend _jointCount As Integer
        Friend _jointList As Joint
    End Class
End Namespace

