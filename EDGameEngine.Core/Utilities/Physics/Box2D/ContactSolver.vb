Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Numerics

Namespace Global.Box2D
    Public Class ContactSolver
        
        ' Methods
        Public Sub FinalizeVelocityConstraints()
            Dim num As Integer = Me._constraints.Count
            Dim i As Integer
            For i = 0 To num - 1
                Dim constraint As ContactConstraint = Me._constraints.Item(i)
                Dim manifold As Manifold = constraint.manifold
                Dim j As Integer
                For j = 0 To constraint.pointCount - 1
                    Dim point As ManifoldPoint = manifold._points.Item(j)
                    Dim point2 As ContactConstraintPoint = constraint.points.Item(j)
                    point.NormalImpulse = point2.normalImpulse
                    point.TangentImpulse = point2.tangentImpulse
                    manifold._points.Item(j) = point
                Next j
                constraint.manifold = manifold
                Me._constraints.Item(i) = (constraint)
                Me._contacts.Item(i)._manifold = manifold
            Next i
        End Sub

        Public Sub InitVelocityConstraints(ByRef [step] As TimeStep)
            Dim num As Integer = Me._constraints.Count
            Dim i As Integer
            For i = 0 To num - 1
                Dim constraint As ContactConstraint = Me._constraints.Item(i)
                Dim bodyA As Body = constraint.bodyA
                Dim bodyB As Body = constraint.bodyB
                Dim num3 As Single = bodyA._invMass
                Dim num4 As Single = bodyA._invI
                Dim num5 As Single = bodyB._invMass
                Dim num6 As Single = bodyB._invI
                Dim normal As Vector2 = constraint.normal
                Dim vector2 As New Vector2(normal.Y, -normal.X)
                If [step].warmStarting Then
                    Dim j As Integer
                    For j = 0 To constraint.pointCount - 1
                        Dim point As ContactConstraintPoint = constraint.points.Item(j)
                        point.normalImpulse = (point.normalImpulse * [step].dtRatio)
                        point.tangentImpulse = (point.tangentImpulse * [step].dtRatio)
                        Dim vector3 As New Vector2(((point.normalImpulse * normal.X) + (point.tangentImpulse * vector2.X)), ((point.normalImpulse * normal.Y) + (point.tangentImpulse * vector2.Y)))
                        bodyA._angularVelocity = (bodyA._angularVelocity - (num4 * ((point.rA.X * vector3.Y) - (point.rA.Y * vector3.X))))
                        bodyA._linearVelocity.X = (bodyA._linearVelocity.X - (num3 * vector3.X))
                        bodyA._linearVelocity.Y = (bodyA._linearVelocity.Y - (num3 * vector3.Y))
                        bodyB._angularVelocity = (bodyB._angularVelocity + (num6 * ((point.rB.X * vector3.Y) - (point.rB.Y * vector3.X))))
                        bodyB._linearVelocity.X = (bodyB._linearVelocity.X + (num5 * vector3.X))
                        bodyB._linearVelocity.Y = (bodyB._linearVelocity.Y + (num5 * vector3.Y))
                        constraint.points.Item(j) = point
                    Next j
                Else
                    Dim k As Integer
                    For k = 0 To constraint.pointCount - 1
                        Dim point2 As ContactConstraintPoint = constraint.points.Item(k)
                        point2.normalImpulse = 0!
                        point2.tangentImpulse = 0!
                        constraint.points.Item(k) = point2
                    Next k
                End If
                Me._constraints.Item(i) = (constraint)
            Next i
        End Sub

        Public Sub Reset(ByRef [step] As TimeStep, ByVal contacts As List(Of Contact))
            Me._step = [step]
            Me._contacts = contacts
            Dim num2 As Integer = contacts.Count
            Me._constraints.Clear()
            Dim i As Integer
            For i = 0 To num2 - 1
                Dim constraint As New ContactConstraint
                Me._constraints.Add(constraint)
            Next i
            Dim j As Integer
            For j = 0 To num2 - 1
                Dim manifold As Manifold
                Dim contact As Contact = contacts.Item(j)
                Dim fixture As Fixture = contact._fixtureA
                Dim fixture2 As Fixture = contact._fixtureB
                Dim shape As Shape = fixture.GetShape
                Dim shape2 As Shape = fixture2.GetShape
                Dim radiusA As Single = shape._radius
                Dim radiusB As Single = shape2._radius
                Dim body As Body = fixture.GetBody
                Dim body2 As Body = fixture2.GetBody
                contact.GetManifold(manifold)
                Dim num7 As Single = Settings.b2MixFriction(fixture.GetFriction, fixture2.GetFriction)
                Dim num8 As Single = Settings.b2MixRestitution(fixture.GetRestitution, fixture2.GetRestitution)
                Dim vector As Vector2 = body._linearVelocity
                Dim vector2 As Vector2 = body2._linearVelocity
                Dim s As Single = body._angularVelocity
                Dim num10 As Single = body2._angularVelocity
                Debug.Assert((manifold._pointCount > 0))
                Dim manifold2 As New WorldManifold(manifold, body._xf, radiusA, body2._xf, radiusB)
                Dim constraint2 As ContactConstraint = Me._constraints.Item(j)
                constraint2.bodyA = body
                constraint2.bodyB = body2
                constraint2.manifold = manifold
                constraint2.normal = manifold2._normal
                constraint2.pointCount = manifold._pointCount
                constraint2.friction = num7
                constraint2.restitution = num8
                constraint2.localPlaneNormal = manifold._localPlaneNormal
                constraint2.localPoint = manifold._localPoint
                constraint2.radius = (radiusA + radiusB)
                constraint2.type = manifold._type
                Dim k As Integer
                For k = 0 To constraint2.pointCount - 1
                    Dim point As ManifoldPoint = manifold._points.Item(k)
                    Dim point2 As ContactConstraintPoint = constraint2.points.Item(k)
                    point2.normalImpulse = point.NormalImpulse
                    point2.tangentImpulse = point.TangentImpulse
                    point2.localPoint = point.LocalPoint
                    point2.rA = (manifold2._points.Item(k) - body._sweep.c)
                    point2.rB = (manifold2._points.Item(k) - body2._sweep.c)
                    Dim num12 As Single = MathUtils.Cross(point2.rA, constraint2.normal)
                    Dim num13 As Single = MathUtils.Cross(point2.rB, constraint2.normal)
                    num12 = (num12 * num12)
                    num13 = (num13 * num13)
                    Dim num14 As Single = (((body._invMass + body2._invMass) + (body._invI * num12)) + (body2._invI * num13))
                    Debug.Assert((num14 > Settings.b2_FLT_EPSILON))
                    point2.normalMass = (1.0! / num14)
                    Dim num15 As Single = ((body._mass * body._invMass) + (body2._mass * body2._invMass))
                    num15 = (num15 + (((body._mass * body._invI) * num12) + ((body2._mass * body2._invI) * num13)))
                    Debug.Assert((num15 > Settings.b2_FLT_EPSILON))
                    point2.equalizedMass = (1.0! / num15)
                    Dim b As Vector2 = MathUtils.Cross(constraint2.normal, CSng(1.0!))
                    Dim num16 As Single = MathUtils.Cross(point2.rA, b)
                    Dim num17 As Single = MathUtils.Cross(point2.rB, b)
                    num16 = (num16 * num16)
                    num17 = (num17 * num17)
                    Dim num18 As Single = (((body._invMass + body2._invMass) + (body._invI * num16)) + (body2._invI * num17))
                    Debug.Assert((num18 > Settings.b2_FLT_EPSILON))
                    point2.tangentMass = (1.0! / num18)
                    point2.velocityBias = 0!
                    Dim num19 As Single = Vector2.Dot(constraint2.normal, (((vector2 + MathUtils.Cross(num10, point2.rB)) - vector) - MathUtils.Cross(s, point2.rA)))
                    If (num19 < -Settings.b2_velocityThreshold) Then
                        point2.velocityBias = (-constraint2.restitution * num19)
                    End If
                    constraint2.points.Item(k) = point2
                Next k
                If (constraint2.pointCount = 2) Then
                    Dim point3 As ContactConstraintPoint = constraint2.points.Item(0)
                    Dim point4 As ContactConstraintPoint = constraint2.points.Item(1)
                    Dim num20 As Single = body._invMass
                    Dim num21 As Single = body._invI
                    Dim num22 As Single = body2._invMass
                    Dim num23 As Single = body2._invI
                    Dim num24 As Single = MathUtils.Cross(point3.rA, constraint2.normal)
                    Dim num25 As Single = MathUtils.Cross(point3.rB, constraint2.normal)
                    Dim num26 As Single = MathUtils.Cross(point4.rA, constraint2.normal)
                    Dim num27 As Single = MathUtils.Cross(point4.rB, constraint2.normal)
                    Dim num28 As Single = (((num20 + num22) + ((num21 * num24) * num24)) + ((num23 * num25) * num25))
                    Dim num29 As Single = (((num20 + num22) + ((num21 * num26) * num26)) + ((num23 * num27) * num27))
                    Dim num30 As Single = (((num20 + num22) + ((num21 * num24) * num26)) + ((num23 * num25) * num27))
                    Dim num31 As Single = 100.0!
                    If ((num28 * num28) < (num31 * ((num28 * num29) - (num30 * num30)))) Then
                        constraint2.K = New Mat22(New Vector2(num28, num30), New Vector2(num30, num29))
                        constraint2.normalMass = constraint2.K.GetInverse
                    Else
                        constraint2.pointCount = 1
                    End If
                End If
                Me._constraints.Item(j) = constraint2
            Next j
        End Sub

        Public Function SolvePositionConstraints(ByVal baumgarte As Single) As Boolean
            Dim num As Single = 0!
            Dim num2 As Integer = Me._constraints.Count
            Dim i As Integer
            For i = 0 To num2 - 1
                Dim cc As ContactConstraint = Me._constraints.Item(i)
                Dim bodyA As Body = cc.bodyA
                Dim bodyB As Body = cc.bodyB
                Dim num4 As Single = (bodyA._mass * bodyA._invMass)
                Dim num5 As Single = (bodyA._mass * bodyA._invI)
                Dim num6 As Single = (bodyB._mass * bodyB._invMass)
                Dim num7 As Single = (bodyB._mass * bodyB._invI)
                Dim manifold As New PositionSolverManifold(cc)
                Dim vector As Vector2 = manifold._normal
                Dim j As Integer
                For j = 0 To cc.pointCount - 1
                    Dim point As ContactConstraintPoint = cc.points.Item(j)
                    Dim vector2 As Vector2 = manifold._points.Item(j)
                    Dim num9 As Single = manifold._separations.Item(j)
                    Dim vector3 As Vector2 = (vector2 - bodyA._sweep.c)
                    Dim vector4 As Vector2 = (vector2 - bodyB._sweep.c)
                    num = Math.Min(num, num9)
                    Dim num10 As Single = (baumgarte * MathUtils.Clamp((num9 + Settings.b2_linearSlop), -Settings.b2_maxLinearCorrection, 0!))
                    Dim num11 As Single = (-point.equalizedMass * num10)
                    Dim vector5 As New Vector2((num11 * vector.X), (num11 * vector.Y))
                    bodyA._sweep.c.X = (bodyA._sweep.c.X - (num4 * vector5.X))
                    bodyA._sweep.c.Y = (bodyA._sweep.c.Y - (num4 * vector5.Y))
                    bodyA._sweep.a = (bodyA._sweep.a - (num5 * ((vector3.X * vector5.Y) - (vector3.Y * vector5.X))))
                    bodyB._sweep.c.X = (bodyB._sweep.c.X + (num6 * vector5.X))
                    bodyB._sweep.c.Y = (bodyB._sweep.c.Y + (num6 * vector5.Y))
                    bodyB._sweep.a = bodyB._sweep.a + (num7 * ((vector4.X * vector5.Y) - (vector4.Y * vector5.X)))
                    bodyA.SynchronizeTransform()
                    bodyB.SynchronizeTransform()
                Next j
            Next i
            Return (num >= (-1.5! * Settings.b2_linearSlop))
        End Function

        Public Sub SolveVelocityConstraints()
            Dim num As Integer = Me._constraints.Count
            Dim i As Integer
            For i = 0 To num - 1
                Dim vector13 As Vector2
                Dim flag7 As Boolean
                Dim constraint As ContactConstraint = Me._constraints.Item(i)
                Dim bodyA As Body = constraint.bodyA
                Dim bodyB As Body = constraint.bodyB
                Dim num3 As Single = bodyA._angularVelocity
                Dim num4 As Single = bodyB._angularVelocity
                Dim vector As Vector2 = bodyA._linearVelocity
                Dim vector2 As Vector2 = bodyB._linearVelocity
                Dim num5 As Single = bodyA._invMass
                Dim num6 As Single = bodyA._invI
                Dim num7 As Single = bodyB._invMass
                Dim num8 As Single = bodyB._invI
                Dim normal As Vector2 = constraint.normal
                Dim vector4 As New Vector2(normal.Y, -normal.X)
                Dim friction As Single = constraint.friction
                Debug.Assert(((constraint.pointCount = 1) OrElse (constraint.pointCount = 2)))
                Dim j As Integer
                For j = 0 To constraint.pointCount - 1
                    Dim point As ContactConstraintPoint = constraint.points.Item(j)
                    Dim vector5 As New Vector2((((vector2.X + (-num4 * point.rB.Y)) - vector.X) - (-num3 * point.rA.Y)), (((vector2.Y + (num4 * point.rB.X)) - vector.Y) - (num3 * point.rA.X)))
                    Dim num11 As Single = ((vector5.X * vector4.X) + (vector5.Y * vector4.Y))
                    Dim num12 As Single = (point.tangentMass * -num11)
                    Dim high As Single = (friction * point.normalImpulse)
                    Dim num14 As Single = MathUtils.Clamp((point.tangentImpulse + num12), -high, high)
                    num12 = (num14 - point.tangentImpulse)
                    Dim vector6 As New Vector2((num12 * vector4.X), (num12 * vector4.Y))
                    vector.X = (vector.X - (num5 * vector6.X))
                    vector.Y = (vector.Y - (num5 * vector6.Y))
                    num3 = (num3 - (num6 * ((point.rA.X * vector6.Y) - (point.rA.Y * vector6.X))))
                    vector2.X = (vector2.X + (num7 * vector6.X))
                    vector2.Y = (vector2.Y + (num7 * vector6.Y))
                    num4 = (num4 + (num8 * ((point.rB.X * vector6.Y) - (point.rB.Y * vector6.X))))
                    point.tangentImpulse = num14
                    constraint.points.Item(j) = point
                Next j
                If (constraint.pointCount = 1) Then
                    Dim point2 As ContactConstraintPoint = constraint.points.Item(0)
                    Dim vector7 As New Vector2((((vector2.X + (-num4 * point2.rB.Y)) - vector.X) - (-num3 * point2.rA.Y)), (((vector2.Y + (num4 * point2.rB.X)) - vector.Y) - (num3 * point2.rA.X)))
                    Dim num15 As Single = ((vector7.X * normal.X) + (vector7.Y * normal.Y))
                    Dim num16 As Single = (-point2.normalMass * (num15 - point2.velocityBias))
                    Dim num17 As Single = Math.Max((point2.normalImpulse + num16), 0!)
                    num16 = (num17 - point2.normalImpulse)
                    Dim vector8 As New Vector2((num16 * normal.X), (num16 * normal.Y))
                    vector.X = (vector.X - (num5 * vector8.X))
                    vector.Y = (vector.Y - (num5 * vector8.Y))
                    num3 = (num3 - (num6 * ((point2.rA.X * vector8.Y) - (point2.rA.Y * vector8.X))))
                    vector2.X = (vector2.X + (num7 * vector8.X))
                    vector2.Y = (vector2.Y + (num7 * vector8.Y))
                    num4 = (num4 + (num8 * ((point2.rB.X * vector8.Y) - (point2.rB.Y * vector8.X))))
                    point2.normalImpulse = num17
                    constraint.points.Item(0) = point2
                    GoTo Label_0EA3
                End If
                Dim point3 As ContactConstraintPoint = constraint.points.Item(0)
                Dim point4 As ContactConstraintPoint = constraint.points.Item(1)
                Dim v As New Vector2(point3.normalImpulse, point4.normalImpulse)
                Debug.Assert(((v.X >= 0!) AndAlso (v.Y >= 0!)))
                Dim vector10 As New Vector2((((vector2.X + (-num4 * point3.rB.Y)) - vector.X) - (-num3 * point3.rA.Y)), (((vector2.Y + (num4 * point3.rB.X)) - vector.Y) - (num3 * point3.rA.X)))
                Dim vector11 As New Vector2((((vector2.X + (-num4 * point4.rB.Y)) - vector.X) - (-num3 * point4.rA.Y)), (((vector2.Y + (num4 * point4.rB.X)) - vector.Y) - (num3 * point4.rA.X)))
                Dim x As Single = ((vector10.X * normal.X) + (vector10.Y * normal.Y))
                Dim y As Single = ((vector11.X * normal.X) + (vector11.Y * normal.Y))
                Dim vector12 As New Vector2((x - point3.velocityBias), (y - point4.velocityBias))
                vector12 = (vector12 - MathUtils.Multiply(constraint.K, v))
                GoTo Label_0E7A
Label_060B:
                vector13 = -MathUtils.Multiply(constraint.normalMass, vector12)
                If ((vector13.X >= 0!) AndAlso (vector13.Y >= 0!)) Then
                    Dim vector14 As New Vector2((vector13.X - v.X), (vector13.Y - v.Y))
                    Dim vector15 As New Vector2((vector14.X * normal.X), (vector14.X * normal.Y))
                    Dim vector16 As New Vector2((vector14.Y * normal.X), (vector14.Y * normal.Y))
                    Dim vector17 As New Vector2((vector15.X + vector16.X), (vector15.Y + vector16.Y))
                    vector.X = (vector.X - (num5 * vector17.X))
                    vector.Y = (vector.Y - (num5 * vector17.Y))
                    num3 = (num3 - (num6 * (((point3.rA.X * vector15.Y) - (point3.rA.Y * vector15.X)) + ((point4.rA.X * vector16.Y) - (point4.rA.Y * vector16.X)))))
                    vector2.X = (vector2.X + (num7 * vector17.X))
                    vector2.Y = (vector2.Y + (num7 * vector17.Y))
                    num4 = (num4 + (num8 * (((point3.rB.X * vector15.Y) - (point3.rB.Y * vector15.X)) + ((point4.rB.X * vector16.Y) - (point4.rB.Y * vector16.X)))))
                    point3.normalImpulse = vector13.X
                    point4.normalImpulse = vector13.Y
                Else
                    vector13.X = (-point3.normalMass * vector12.X)
                    vector13.Y = 0!
                    x = 0!
                    y = ((constraint.K.col1.Y * vector13.X) + vector12.Y)
                    If ((vector13.X >= 0!) AndAlso (y >= 0!)) Then
                        Dim vector18 As New Vector2((vector13.X - v.X), (vector13.Y - v.Y))
                        Dim vector19 As New Vector2((vector18.X * normal.X), (vector18.X * normal.Y))
                        Dim vector20 As New Vector2((vector18.Y * normal.X), (vector18.Y * normal.Y))
                        Dim vector21 As New Vector2((vector19.X + vector20.X), (vector19.Y + vector20.Y))
                        vector.X = (vector.X - (num5 * vector21.X))
                        vector.Y = (vector.Y - (num5 * vector21.Y))
                        num3 = (num3 - (num6 * (((point3.rA.X * vector19.Y) - (point3.rA.Y * vector19.X)) + ((point4.rA.X * vector20.Y) - (point4.rA.Y * vector20.X)))))
                        vector2.X = (vector2.X + (num7 * vector21.X))
                        vector2.Y = (vector2.Y + (num7 * vector21.Y))
                        num4 = (num4 + (num8 * (((point3.rB.X * vector19.Y) - (point3.rB.Y * vector19.X)) + ((point4.rB.X * vector20.Y) - (point4.rB.Y * vector20.X)))))
                        point3.normalImpulse = vector13.X
                        point4.normalImpulse = vector13.Y
                    Else
                        vector13.X = 0!
                        vector13.Y = (-point4.normalMass * vector12.Y)
                        x = ((constraint.K.col2.X * vector13.Y) + vector12.X)
                        y = 0!
                        If ((vector13.Y >= 0!) AndAlso (x >= 0!)) Then
                            Dim vector22 As New Vector2((vector13.X - v.X), (vector13.Y - v.Y))
                            Dim vector23 As New Vector2((vector22.X * normal.X), (vector22.X * normal.Y))
                            Dim vector24 As New Vector2((vector22.Y * normal.X), (vector22.Y * normal.Y))
                            Dim vector25 As New Vector2((vector23.X + vector24.X), (vector23.Y + vector24.Y))
                            vector.X = (vector.X - (num5 * vector25.X))
                            vector.Y = (vector.Y - (num5 * vector25.Y))
                            num3 = (num3 - (num6 * (((point3.rA.X * vector23.Y) - (point3.rA.Y * vector23.X)) + ((point4.rA.X * vector24.Y) - (point4.rA.Y * vector24.X)))))
                            vector2.X = (vector2.X + (num7 * vector25.X))
                            vector2.Y = (vector2.Y + (num7 * vector25.Y))
                            num4 = (num4 + (num8 * (((point3.rB.X * vector23.Y) - (point3.rB.Y * vector23.X)) + ((point4.rB.X * vector24.Y) - (point4.rB.Y * vector24.X)))))
                            point3.normalImpulse = vector13.X
                            point4.normalImpulse = vector13.Y
                        Else
                            vector13.X = 0!
                            vector13.Y = 0!
                            x = vector12.X
                            y = vector12.Y
                            If ((x >= 0!) AndAlso (y >= 0!)) Then
                                Dim vector26 As New Vector2((vector13.X - v.X), (vector13.Y - v.Y))
                                Dim vector27 As New Vector2((vector26.X * normal.X), (vector26.X * normal.Y))
                                Dim vector28 As New Vector2((vector26.Y * normal.X), (vector26.Y * normal.Y))
                                Dim vector29 As New Vector2((vector27.X + vector28.X), (vector27.Y + vector28.Y))
                                vector.X = (vector.X - (num5 * vector29.X))
                                vector.Y = (vector.Y - (num5 * vector29.Y))
                                num3 = (num3 - (num6 * (((point3.rA.X * vector27.Y) - (point3.rA.Y * vector27.X)) + ((point4.rA.X * vector28.Y) - (point4.rA.Y * vector28.X)))))
                                vector2.X = (vector2.X + (num7 * vector29.X))
                                vector2.Y = (vector2.Y + (num7 * vector29.Y))
                                num4 = (num4 + (num8 * (((point3.rB.X * vector27.Y) - (point3.rB.Y * vector27.X)) + ((point4.rB.X * vector28.Y) - (point4.rB.Y * vector28.X)))))
                                point3.normalImpulse = vector13.X
                                point4.normalImpulse = vector13.Y
                            End If
                        End If
                    End If
                End If
                GoTo Label_0E82
Label_0E7A:
                flag7 = True
                GoTo Label_060B
Label_0E82:
                constraint.points.Item(0) = point3
                constraint.points.Item(1) = point4
Label_0EA3:
                Me._constraints.Item(i) = constraint
                bodyA._linearVelocity = vector
                bodyA._angularVelocity = num3
                bodyB._linearVelocity = vector2
                bodyB._angularVelocity = num4
            Next i
        End Sub


        ' Fields
        Public _constraints As List(Of ContactConstraint) = New List(Of ContactConstraint)(50)
        Private _contacts As List(Of Contact)
        Public _step As TimeStep
    End Class
End Namespace

