
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Numerics
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    Friend Class Island
        
        ' Methods
        Public Sub Add(ByVal body As Body)
            Debug.Assert((Me._bodyCount < Me._bodyCapacity))
            body._islandIndex = Me._bodyCount
            Dim index As Integer = Me._bodyCount
            Me._bodyCount = (index + 1)
            Me._bodies(index) = body
        End Sub

        Public Sub Add(ByVal contact As Contact)
            Me._contacts.Add(contact)
        End Sub

        Public Sub Add(ByVal joint As Joint)
            Debug.Assert((Me._jointCount < Me._jointCapacity))
            Dim index As Integer = Me._jointCount
            Me._jointCount = (index + 1)
            Me._joints(index) = joint
        End Sub

        Public Sub Clear()
            Me._bodyCount = 0
            Me._contacts.Clear()
            Me._jointCount = 0
        End Sub

        Public Sub Report(ByVal constraints As List(Of ContactConstraint))
            If (Not Me._listener Is Nothing) Then
                Dim num As Integer = Me._contacts.Count
                Dim i As Integer
                For i = 0 To num - 1
                    Dim contact As Contact = Me._contacts.Item(i)
                    Dim constraint As ContactConstraint = constraints.Item(i)
                    Dim impulse As New ContactImpulse
                    Dim j As Integer
                    For j = 0 To constraint.pointCount - 1
                        impulse.normalImpulses.Item(j) = constraint.points.Item(j).normalImpulse
                        impulse.tangentImpulses.Item(j) = constraint.points.Item(j).tangentImpulse
                    Next j
                    Me._listener.PostSolve(contact, impulse)
                Next i
            End If
        End Sub

        Public Sub Reset(ByVal bodyCapacity As Integer, ByVal contactCapacity As Integer, ByVal jointCapacity As Integer, ByVal listener As IContactListener)
            Me._bodyCapacity = bodyCapacity
            Me._contactCapacity = contactCapacity
            Me._jointCapacity = jointCapacity
            Me._bodyCount = 0
            Me._jointCount = 0
            Me._listener = listener
            If ((Me._bodies Is Nothing) OrElse (Me._bodies.Length < bodyCapacity)) Then
                Me._bodies = New Body(bodyCapacity - 1) {}
            End If
            Me._contacts.Clear()

            If ((Me._joints Is Nothing) OrElse (Me._joints.Length < jointCapacity)) Then
                Me._joints = New Joint(jointCapacity - 1) {}
            End If
        End Sub

        Public Sub Solve(ByRef [step] As TimeStep, ByVal gravity As Vector2, ByVal allowSleep As Boolean)
            Dim i As Integer
            For i = 0 To Me._bodyCount - 1
                Dim body As Body = Me._bodies(i)
                If Not body.IsStatic Then
                    body._linearVelocity = (body._linearVelocity + ([step].dt * (gravity + (body._invMass * body._force))))
                    body._angularVelocity = (body._angularVelocity + (([step].dt * body._invI) * body._torque))
                    body._force = New Vector2(0!, 0!)
                    body._torque = 0!
                    body._linearVelocity = (body._linearVelocity * MathUtils.Clamp(CSng((1.0! - ([step].dt * body._linearDamping))), CSng(0!), CSng(1.0!)))
                    body._angularVelocity = (body._angularVelocity * MathUtils.Clamp(CSng((1.0! - ([step].dt * body._angularDamping))), CSng(0!), CSng(1.0!)))
                End If
            Next i
            Me._contactSolver.Reset([step], Me._contacts)
            Me._contactSolver.InitVelocityConstraints([step])
            Dim j As Integer
            For j = 0 To Me._jointCount - 1
                Me._joints(j).InitVelocityConstraints([step])
            Next j
            Dim k As Integer
            For k = 0 To [step].velocityIterations - 1
                Dim num4 As Integer
                For num4 = 0 To Me._jointCount - 1
                    Me._joints(num4).SolveVelocityConstraints([step])
                Next num4
                Me._contactSolver.SolveVelocityConstraints()
            Next k
            Me._contactSolver.FinalizeVelocityConstraints()
            Dim m As Integer
            For m = 0 To Me._bodyCount - 1
                Dim body2 As Body = Me._bodies(m)
                If Not body2.IsStatic Then
                    Dim vec As Vector2 = ([step].dt * body2._linearVelocity)
                    If (Vector2.Dot(vec, vec) > Settings.b2_maxTranslationSquared) Then
                        Extension.Normalize(vec)
                        body2._linearVelocity = ((Settings.b2_maxTranslation * [step].inv_dt) * vec)
                    End If
                    Dim num6 As Single = ([step].dt * body2._angularVelocity)
                    If ((num6 * num6) > Settings.b2_maxRotationSquared) Then
                        If (num6 < 0) Then
                            body2._angularVelocity = (-[step].inv_dt * Settings.b2_maxRotation)
                        Else
                            body2._angularVelocity = ([step].inv_dt * Settings.b2_maxRotation)
                        End If
                    End If
                    body2._sweep.c0 = body2._sweep.c
                    body2._sweep.a0 = body2._sweep.a
                    body2._sweep.c = (body2._sweep.c + ([step].dt * body2._linearVelocity))
                    body2._sweep.a = (body2._sweep.a + ([step].dt * body2._angularVelocity))
                    body2.SynchronizeTransform()
                End If
            Next m
            Dim n As Integer
            For n = 0 To [step].positionIterations - 1
                Dim flag11 As Boolean = Me._contactSolver.SolvePositionConstraints(Settings.b2_contactBaumgarte)
                Dim flag12 As Boolean = True
                Dim num8 As Integer
                For num8 = 0 To Me._jointCount - 1
                    Dim flag13 As Boolean = Me._joints(num8).SolvePositionConstraints(Settings.b2_contactBaumgarte)
                    flag12 = (flag12 And flag13)
                Next num8
                If (flag11 And flag12) Then
                    Exit For
                End If
            Next n
            Me.Report(Me._contactSolver._constraints)
            If allowSleep Then
                Dim num9 As Single = Settings.b2_FLT_MAX
                Dim num10 As Single = (Settings.b2_linearSleepTolerance * Settings.b2_linearSleepTolerance)
                Dim num11 As Single = (Settings.b2_angularSleepTolerance * Settings.b2_angularSleepTolerance)
                Dim num12 As Integer
                For num12 = 0 To Me._bodyCount - 1
                    Dim body3 As Body = Me._bodies(num12)
                    If (Not body3._invMass = 0!) Then
                        If ((body3._flags And BodyFlags.AllowSleep) = BodyFlags.None) Then
                            body3._sleepTime = 0!
                            num9 = 0!
                        End If
                        If ((((body3._flags And BodyFlags.AllowSleep) = BodyFlags.None) OrElse ((body3._angularVelocity * body3._angularVelocity) > num11)) OrElse (Vector2.Dot(body3._linearVelocity, body3._linearVelocity) > num10)) Then
                            body3._sleepTime = 0!
                            num9 = 0!
                        Else
                            body3._sleepTime = (body3._sleepTime + [step].dt)
                            num9 = Math.Min(num9, body3._sleepTime)
                        End If
                    End If
                Next num12
                If (num9 >= Settings.b2_timeToSleep) Then
                    Dim num13 As Integer
                    For num13 = 0 To Me._bodyCount - 1
                        Dim body4 As Body = Me._bodies(num13)
                        body4._flags = (body4._flags Or BodyFlags.Sleep)
                        body4._linearVelocity = Vector2.Zero
                        body4._angularVelocity = 0!
                    Next num13
                End If
            End If
        End Sub

        Public Sub SolveTOI(ByRef subStep As TimeStep)
            Me._contactSolver.Reset(subStep, Me._contacts)
            Dim i As Integer
            For i = 0 To Me._jointCount - 1
                Me._joints(i).InitVelocityConstraints(subStep)
            Next i
            Dim j As Integer
            For j = 0 To subStep.velocityIterations - 1
                Me._contactSolver.SolveVelocityConstraints()
                Dim n As Integer
                For n = 0 To Me._jointCount - 1
                    Me._joints(n).SolveVelocityConstraints(subStep)
                Next n
            Next j
            Dim k As Integer
            For k = 0 To Me._bodyCount - 1
                Dim body As Body = Me._bodies(k)
                If Not body.IsStatic Then
                    Dim vec As Vector2 = (subStep.dt * body._linearVelocity)
                    If (Vector2.Dot(vec, vec) > Settings.b2_maxTranslationSquared) Then
                        Extension.Normalize(vec)
                        body._linearVelocity = ((Settings.b2_maxTranslation * subStep.inv_dt) * vec)
                    End If
                    Dim num6 As Single = (subStep.dt * body._angularVelocity)
                    If ((num6 * num6) > Settings.b2_maxRotationSquared) Then
                        If (num6 < 0) Then
                            body._angularVelocity = (-subStep.inv_dt * Settings.b2_maxRotation)
                        Else
                            body._angularVelocity = (subStep.inv_dt * Settings.b2_maxRotation)
                        End If
                    End If
                    body._sweep.c0 = body._sweep.c
                    body._sweep.a0 = body._sweep.a
                    body._sweep.c = (body._sweep.c + (subStep.dt * body._linearVelocity))
                    body._sweep.a = (body._sweep.a + (subStep.dt * body._angularVelocity))
                    body.SynchronizeTransform()
                End If
            Next k
            Dim baumgarte As Single = 0.75!
            Dim m As Integer
            For m = 0 To subStep.positionIterations - 1
                Dim flag9 As Boolean = Me._contactSolver.SolvePositionConstraints(baumgarte)
                Dim flag10 As Boolean = True
                Dim num8 As Integer
                For num8 = 0 To Me._jointCount - 1
                    Dim flag11 As Boolean = Me._joints(num8).SolvePositionConstraints(baumgarte)
                    flag10 = (flag10 And flag11)
                Next num8
                If (flag9 And flag10) Then
                    Exit For
                End If
            Next m
            Me.Report(Me._contactSolver._constraints)
        End Sub


        ' Fields
        Public _bodies As Body()
        Public _bodyCapacity As Integer
        Public _bodyCount As Integer
        Public _contactCapacity As Integer
        Public _contacts As List(Of Contact) = New List(Of Contact)(50)
        Private _contactSolver As ContactSolver = New ContactSolver
        Public _jointCapacity As Integer
        Public _jointCount As Integer
        Public _joints As Joint()
        Public _listener As IContactListener
        Public _positionIterationCount As Integer
    End Class
End Namespace

