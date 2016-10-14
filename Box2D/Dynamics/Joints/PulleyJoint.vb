Imports System
Imports System.Diagnostics
Imports System.Numerics

Namespace Global.Box2D
    Public Class PulleyJoint
        Inherits Joint
        ' Methods
        Friend Sub New(ByVal def As PulleyJointDef)
            MyBase.New(def)
            Me._groundAnchor1 = def.groundAnchor1
            Me._groundAnchor2 = def.groundAnchor2
            Me._localAnchor1 = def.localAnchor1
            Me._localAnchor2 = def.localAnchor2
            Debug.Assert((Not def.ratio = 0!))
            Me._ratio = def.ratio
            Me._ant = (def.length1 + (Me._ratio * def.length2))
            Me._maxLength1 = Math.Min(def.maxLength1, (Me._ant - (Me._ratio * PulleyJointDef.b2_minPulleyLength)))
            Me._maxLength2 = Math.Min(def.maxLength2, ((Me._ant - PulleyJointDef.b2_minPulleyLength) / Me._ratio))
            Me._impulse = 0!
            Me._limitImpulse1 = 0!
            Me._limitImpulse2 = 0!
        End Sub

        Public Overrides Function GetAnchor1() As Vector2
            Return MyBase._bodyA.GetWorldPoint(Me._localAnchor1)
        End Function

        Public Overrides Function GetAnchor2() As Vector2
            Return MyBase._bodyB.GetWorldPoint(Me._localAnchor2)
        End Function

        Public Function GetGroundAnchor1() As Vector2
            Return Me._groundAnchor1
        End Function

        Public Function GetGroundAnchor2() As Vector2
            Return Me._groundAnchor2
        End Function

        Public Function GetLength1() As Single
            Dim worldPoint As Vector2 = MyBase._bodyA.GetWorldPoint(Me._localAnchor1)
            Dim vector2 As Vector2 = Me._groundAnchor1
            Dim vector3 As Vector2 = (worldPoint - vector2)
            Return vector3.Length
        End Function

        Public Function GetLength2() As Single
            Dim worldPoint As Vector2 = MyBase._bodyB.GetWorldPoint(Me._localAnchor2)
            Dim vector2 As Vector2 = Me._groundAnchor2
            Dim vector3 As Vector2 = (worldPoint - vector2)
            Return vector3.Length
        End Function

        Public Function GetRatio() As Single
            Return Me._ratio
        End Function

        Public Overrides Function GetReactionForce(ByVal inv_dt As Single) As Vector2
            Dim vector As Vector2 = (Me._impulse * Me._u2)
            Return (inv_dt * vector)
        End Function

        Public Overrides Function GetReactionTorque(ByVal inv_dt As Single) As Single
            Return 0!
        End Function

        Friend Overrides Sub InitVelocityConstraints(ByRef [step] As TimeStep)
            Dim form As XForm
            Dim form2 As XForm
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            body.GetXForm(form)
            body2.GetXForm(form2)
            Dim a As Vector2 = MathUtils.Multiply(form.RoateMatrix, (Me._localAnchor1 - body.GetLocalCenter))
            Dim vector2 As Vector2 = MathUtils.Multiply(form2.RoateMatrix, (Me._localAnchor2 - body2.GetLocalCenter))
            Dim vector3 As Vector2 = (body._sweep.c + a)
            Dim vector4 As Vector2 = (body2._sweep.c + vector2)
            Dim vector5 As Vector2 = Me._groundAnchor1
            Dim vector6 As Vector2 = Me._groundAnchor2
            Me._u1 = (vector3 - vector5)
            Me._u2 = (vector4 - vector6)
            Dim num As Single = Me._u1.Length
            Dim num2 As Single = Me._u2.Length
            If (num > Settings.b2_linearSlop) Then
                Me._u1 = (Me._u1 * (1.0! / num))
            Else
                Me._u1 = Vector2.Zero
            End If
            If (num2 > Settings.b2_linearSlop) Then
                Me._u2 = (Me._u2 * (1.0! / num2))
            Else
                Me._u2 = Vector2.Zero
            End If
            Dim num3 As Single = ((Me._ant - num) - (Me._ratio * num2))
            If (num3 > 0!) Then
                Me._state = LimitState.Inactive
                Me._impulse = 0!
            Else
                Me._state = LimitState.AtUpper
            End If
            If (num < Me._maxLength1) Then
                Me._limitState1 = LimitState.Inactive
                Me._limitImpulse1 = 0!
            Else
                Me._limitState1 = LimitState.AtUpper
            End If
            If (num2 < Me._maxLength2) Then
                Me._limitState2 = LimitState.Inactive
                Me._limitImpulse2 = 0!
            Else
                Me._limitState2 = LimitState.AtUpper
            End If
            Dim num4 As Single = MathUtils.Cross(a, Me._u1)
            Dim num5 As Single = MathUtils.Cross(vector2, Me._u2)
            Me._limitMass1 = (body._invMass + ((body._invI * num4) * num4))
            Me._limitMass2 = (body2._invMass + ((body2._invI * num5) * num5))
            Me._pulleyMass = (Me._limitMass1 + ((Me._ratio * Me._ratio) * Me._limitMass2))
            Debug.Assert((Me._limitMass1 > Settings.b2_FLT_EPSILON))
            Debug.Assert((Me._limitMass2 > Settings.b2_FLT_EPSILON))
            Debug.Assert((Me._pulleyMass > Settings.b2_FLT_EPSILON))
            Me._limitMass1 = (1.0! / Me._limitMass1)
            Me._limitMass2 = (1.0! / Me._limitMass2)
            Me._pulleyMass = (1.0! / Me._pulleyMass)
            If [step].warmStarting Then
                Me._impulse = (Me._impulse * [step].dtRatio)
                Me._limitImpulse1 = (Me._limitImpulse1 * [step].dtRatio)
                Me._limitImpulse2 = (Me._limitImpulse2 * [step].dtRatio)
                Dim b As Vector2 = (-(Me._impulse + Me._limitImpulse1) * Me._u1)
                Dim vector8 As Vector2 = (((-Me._ratio * Me._impulse) - Me._limitImpulse2) * Me._u2)
                body._linearVelocity = (body._linearVelocity + (body._invMass * b))
                body._angularVelocity = (body._angularVelocity + (body._invI * MathUtils.Cross(a, b)))
                body2._linearVelocity = (body2._linearVelocity + (body2._invMass * vector8))
                body2._angularVelocity = (body2._angularVelocity + (body2._invI * MathUtils.Cross(vector2, vector8)))
            Else
                Me._impulse = 0!
                Me._limitImpulse1 = 0!
                Me._limitImpulse2 = 0!
            End If
        End Sub

        Friend Overrides Function SolvePositionConstraints(ByVal baumgarte As Single) As Boolean
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            Dim vector As Vector2 = Me._groundAnchor1
            Dim vector2 As Vector2 = Me._groundAnchor2
            Dim num As Single = 0!
            If (Me._state = LimitState.AtUpper) Then
                Dim form As XForm
                Dim form2 As XForm
                body.GetXForm(form)
                body2.GetXForm(form2)
                Dim a As Vector2 = MathUtils.Multiply(form.RoateMatrix, (Me._localAnchor1 - body.GetLocalCenter))
                Dim vector4 As Vector2 = MathUtils.Multiply(form2.RoateMatrix, (Me._localAnchor2 - body2.GetLocalCenter))
                Dim vector5 As Vector2 = (body._sweep.c + a)
                Dim vector6 As Vector2 = (body2._sweep.c + vector4)
                Me._u1 = (vector5 - vector)
                Me._u2 = (vector6 - vector2)
                Dim num2 As Single = Me._u1.Length
                Dim num3 As Single = Me._u2.Length
                If (num2 > Settings.b2_linearSlop) Then
                    Me._u1 = (Me._u1 * (1.0! / num2))
                Else
                    Me._u1 = Vector2.Zero
                End If
                If (num3 > Settings.b2_linearSlop) Then
                    Me._u2 = (Me._u2 * (1.0! / num3))
                Else
                    Me._u2 = Vector2.Zero
                End If
                Dim num4 As Single = ((Me._ant - num2) - (Me._ratio * num3))
                num = Math.Max(num, -num4)
                num4 = MathUtils.Clamp((num4 + Settings.b2_linearSlop), -Settings.b2_maxLinearCorrection, 0!)
                Dim num5 As Single = (-Me._pulleyMass * num4)
                Dim b As Vector2 = (-num5 * Me._u1)
                Dim vector8 As Vector2 = ((-Me._ratio * num5) * Me._u2)
                body._sweep.c = (body._sweep.c + (body._invMass * b))
                body._sweep.a = (body._sweep.a + (body._invI * MathUtils.Cross(a, b)))
                body2._sweep.c = (body2._sweep.c + (body2._invMass * vector8))
                body2._sweep.a = (body2._sweep.a + (body2._invI * MathUtils.Cross(vector4, vector8)))
                body.SynchronizeTransform()
                body2.SynchronizeTransform()
            End If
            If (Me._limitState1 = LimitState.AtUpper) Then
                Dim form3 As XForm
                body.GetXForm(form3)
                Dim vector9 As Vector2 = MathUtils.Multiply(form3.RoateMatrix, (Me._localAnchor1 - body.GetLocalCenter))
                Dim vector10 As Vector2 = (body._sweep.c + vector9)
                Me._u1 = (vector10 - vector)
                Dim num6 As Single = Me._u1.Length
                If (num6 > Settings.b2_linearSlop) Then
                    Me._u1 = (Me._u1 * (1.0! / num6))
                Else
                    Me._u1 = Vector2.Zero
                End If
                Dim num7 As Single = (Me._maxLength1 - num6)
                num = Math.Max(num, -num7)
                num7 = MathUtils.Clamp((num7 + Settings.b2_linearSlop), -Settings.b2_maxLinearCorrection, 0!)
                Dim num8 As Single = (-Me._limitMass1 * num7)
                Dim vector11 As Vector2 = (-num8 * Me._u1)
                body._sweep.c = (body._sweep.c + (body._invMass * vector11))
                body._sweep.a = (body._sweep.a + (body._invI * MathUtils.Cross(vector9, vector11)))
                body.SynchronizeTransform()
            End If
            If (Me._limitState2 = LimitState.AtUpper) Then
                Dim form4 As XForm
                body2.GetXForm(form4)
                Dim vector12 As Vector2 = MathUtils.Multiply(form4.RoateMatrix, (Me._localAnchor2 - body2.GetLocalCenter))
                Dim vector13 As Vector2 = (body2._sweep.c + vector12)
                Me._u2 = (vector13 - vector2)
                Dim num9 As Single = Me._u2.Length
                If (num9 > Settings.b2_linearSlop) Then
                    Me._u2 = (Me._u2 * (1.0! / num9))
                Else
                    Me._u2 = Vector2.Zero
                End If
                Dim num10 As Single = (Me._maxLength2 - num9)
                num = Math.Max(num, -num10)
                num10 = MathUtils.Clamp((num10 + Settings.b2_linearSlop), -Settings.b2_maxLinearCorrection, 0!)
                Dim num11 As Single = (-Me._limitMass2 * num10)
                Dim vector14 As Vector2 = (-num11 * Me._u2)
                body2._sweep.c = (body2._sweep.c + (body2._invMass * vector14))
                body2._sweep.a = (body2._sweep.a + (body2._invI * MathUtils.Cross(vector12, vector14)))
                body2.SynchronizeTransform()
            End If
            Return (num < Settings.b2_linearSlop)
        End Function

        Friend Overrides Sub SolveVelocityConstraints(ByRef [step] As TimeStep)
            Dim form As XForm
            Dim form2 As XForm
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            body.GetXForm(form)
            body2.GetXForm(form2)
            Dim a As Vector2 = MathUtils.Multiply(form.RoateMatrix, (Me._localAnchor1 - body.GetLocalCenter))
            Dim vector2 As Vector2 = MathUtils.Multiply(form2.RoateMatrix, (Me._localAnchor2 - body2.GetLocalCenter))
            If (Me._state = LimitState.AtUpper) Then
                Dim vector3 As Vector2 = (body._linearVelocity + MathUtils.Cross(body._angularVelocity, a))
                Dim vector4 As Vector2 = (body2._linearVelocity + MathUtils.Cross(body2._angularVelocity, vector2))
                Dim num As Single = (-Vector2.Dot(Me._u1, vector3) - (Me._ratio * Vector2.Dot(Me._u2, vector4)))
                Dim num2 As Single = (Me._pulleyMass * -num)
                Dim num3 As Single = Me._impulse
                Me._impulse = Math.Max(0!, (Me._impulse + num2))
                num2 = (Me._impulse - num3)
                Dim b As Vector2 = (-num2 * Me._u1)
                Dim vector6 As Vector2 = ((-Me._ratio * num2) * Me._u2)
                body._linearVelocity = (body._linearVelocity + (body._invMass * b))
                body._angularVelocity = (body._angularVelocity + (body._invI * MathUtils.Cross(a, b)))
                body2._linearVelocity = (body2._linearVelocity + (body2._invMass * vector6))
                body2._angularVelocity = (body2._angularVelocity + (body2._invI * MathUtils.Cross(vector2, vector6)))
            End If
            If (Me._limitState1 = LimitState.AtUpper) Then
                Dim vector7 As Vector2 = (body._linearVelocity + MathUtils.Cross(body._angularVelocity, a))
                Dim num4 As Single = -Vector2.Dot(Me._u1, vector7)
                Dim num5 As Single = (-Me._limitMass1 * num4)
                Dim num6 As Single = Me._limitImpulse1
                Me._limitImpulse1 = Math.Max(0!, (Me._limitImpulse1 + num5))
                num5 = (Me._limitImpulse1 - num6)
                Dim vector8 As Vector2 = (-num5 * Me._u1)
                body._linearVelocity = (body._linearVelocity + (body._invMass * vector8))
                body._angularVelocity = (body._angularVelocity + (body._invI * MathUtils.Cross(a, vector8)))
            End If
            If (Me._limitState2 = LimitState.AtUpper) Then
                Dim vector9 As Vector2 = (body2._linearVelocity + MathUtils.Cross(body2._angularVelocity, vector2))
                Dim num7 As Single = -Vector2.Dot(Me._u2, vector9)
                Dim num8 As Single = (-Me._limitMass2 * num7)
                Dim num9 As Single = Me._limitImpulse2
                Me._limitImpulse2 = Math.Max(0!, (Me._limitImpulse2 + num8))
                num8 = (Me._limitImpulse2 - num9)
                Dim vector10 As Vector2 = (-num8 * Me._u2)
                body2._linearVelocity = (body2._linearVelocity + (body2._invMass * vector10))
                body2._angularVelocity = (body2._angularVelocity + (body2._invI * MathUtils.Cross(vector2, vector10)))
            End If
        End Sub


        ' Fields
        Friend _ant As Single
        Friend _groundAnchor1 As Vector2
        Friend _groundAnchor2 As Vector2
        Friend _impulse As Single
        Friend _limitImpulse1 As Single
        Friend _limitImpulse2 As Single
        Friend _limitMass1 As Single
        Friend _limitMass2 As Single
        Friend _limitState1 As LimitState
        Friend _limitState2 As LimitState
        Friend _localAnchor1 As Vector2
        Friend _localAnchor2 As Vector2
        Friend _maxLength1 As Single
        Friend _maxLength2 As Single
        Friend _pulleyMass As Single
        Friend _ratio As Single
        Friend _state As LimitState
        Friend _u1 As Vector2
        Friend _u2 As Vector2
    End Class
End Namespace

