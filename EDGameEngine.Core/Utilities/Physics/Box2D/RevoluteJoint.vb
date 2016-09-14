
Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    Public Class RevoluteJoint
        Inherits Joint
        ' Methods
        Friend Sub New(ByVal def As RevoluteJointDef)
            MyBase.New(def)
            Me._localAnchor1 = def.localAnchor1
            Me._localAnchor2 = def.localAnchor2
            Me._referenceAngle = def.referenceAngle
            Me._impulse = Vector3.Zero
            Me._motorImpulse = 0!
            Me._lowerAngle = def.lowerAngle
            Me._upperAngle = def.upperAngle
            Me._maxMotorTorque = def.maxMotorTorque
            Me._motorSpeed = def.motorSpeed
            Me._enableLimit = def.enableLimit
            Me._enableMotor = def.enableMotor
            Me._limitState = LimitState.Inactive
        End Sub

        Public Sub EnableLimit(ByVal flag As Boolean)
            MyBase._bodyA.WakeUp()
            MyBase._bodyB.WakeUp()
            Me._enableLimit = flag
        End Sub

        Public Sub EnableMotor(ByVal flag As Boolean)
            MyBase._bodyA.WakeUp()
            MyBase._bodyB.WakeUp()
            Me._enableMotor = flag
        End Sub

        Public Overrides Function GetAnchor1() As Vector2
            Return MyBase._bodyA.GetWorldPoint(Me._localAnchor1)
        End Function

        Public Overrides Function GetAnchor2() As Vector2
            Return MyBase._bodyB.GetWorldPoint(Me._localAnchor2)
        End Function

        Public Function GetJointAngle() As Single
            Dim body As Body = MyBase._bodyA
            Return ((MyBase._bodyB._sweep.a - body._sweep.a) - Me._referenceAngle)
        End Function

        Public Function GetJointSpeed() As Single
            Dim body As Body = MyBase._bodyA
            Return (MyBase._bodyB._angularVelocity - body._angularVelocity)
        End Function

        Public Function GetLowerLimit() As Single
            Return Me._lowerAngle
        End Function

        Public Function GetMotorSpeed() As Single
            Return Me._motorSpeed
        End Function

        Public Function GetMotorTorque() As Single
            Return Me._motorImpulse
        End Function

        Public Overrides Function GetReactionForce(ByVal inv_dt As Single) As Vector2
            Dim vector As New Vector2(Me._impulse.X, Me._impulse.Y)
            Return (inv_dt * vector)
        End Function

        Public Overrides Function GetReactionTorque(ByVal inv_dt As Single) As Single
            Return (inv_dt * Me._impulse.Z)
        End Function

        Public Function GetUpperLimit() As Single
            Return Me._upperAngle
        End Function

        Friend Overrides Sub InitVelocityConstraints(ByRef [step] As TimeStep)
            Dim form As XForm
            Dim form2 As XForm
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            If (Me._enableMotor OrElse Me._enableLimit) Then
                Debug.Assert(((body._invI > 0!) OrElse (body2._invI > 0!)))
            End If
            body.GetXForm(form)
            body2.GetXForm(form2)
            Dim a As Vector2 = MathUtils.Multiply(form.R, (Me._localAnchor1 - body.GetLocalCenter))
            Dim vector2 As Vector2 = MathUtils.Multiply(form2.R, (Me._localAnchor2 - body2.GetLocalCenter))
            Dim num As Single = body._invMass
            Dim num2 As Single = body2._invMass
            Dim num3 As Single = body._invI
            Dim num4 As Single = body2._invI
            Me._mass.col1.X = (((num + num2) + ((a.Y * a.Y) * num3)) + ((vector2.Y * vector2.Y) * num4))
            Me._mass.col2.X = (((-a.Y * a.X) * num3) - ((vector2.Y * vector2.X) * num4))
            Me._mass.col3.X = ((-a.Y * num3) - (vector2.Y * num4))
            Me._mass.col1.Y = Me._mass.col2.X
            Me._mass.col2.Y = (((num + num2) + ((a.X * a.X) * num3)) + ((vector2.X * vector2.X) * num4))
            Me._mass.col3.Y = ((a.X * num3) + (vector2.X * num4))
            Me._mass.col1.Z = Me._mass.col3.X
            Me._mass.col2.Z = Me._mass.col3.Y
            Me._mass.col3.Z = (num3 + num4)
            Me._motorMass = (1.0! / (num3 + num4))
            If Not Me._enableMotor Then
                Me._motorImpulse = 0!
            End If
            If Me._enableLimit Then
                Dim num5 As Single = ((body2._sweep.a - body._sweep.a) - Me._referenceAngle)
                If (Math.Abs((Me._upperAngle - Me._lowerAngle)) < (2.0! * Settings.b2_angularSlop)) Then
                    Me._limitState = LimitState.Equal
                ElseIf (num5 <= Me._lowerAngle) Then
                    If (Me._limitState <> LimitState.AtLower) Then
                        Me._impulse.Z = 0!
                    End If
                    Me._limitState = LimitState.AtLower
                ElseIf (num5 >= Me._upperAngle) Then
                    If (Me._limitState <> LimitState.AtUpper) Then
                        Me._impulse.Z = 0!
                    End If
                    Me._limitState = LimitState.AtUpper
                Else
                    Me._limitState = LimitState.Inactive
                    Me._impulse.Z = 0!
                End If
            Else
                Me._limitState = LimitState.Inactive
            End If
            If [step].warmStarting Then
                Me._impulse = (Me._impulse * [step].dtRatio)
                Me._motorImpulse = (Me._motorImpulse * [step].dtRatio)
                Dim b As New Vector2(Me._impulse.X, Me._impulse.Y)
                body._linearVelocity = (body._linearVelocity - (num * b))
                body._angularVelocity = (body._angularVelocity - (num3 * ((MathUtils.Cross(a, b) + Me._motorImpulse) + Me._impulse.Z)))
                body2._linearVelocity = (body2._linearVelocity + (num2 * b))
                body2._angularVelocity = (body2._angularVelocity + (num4 * ((MathUtils.Cross(vector2, b) + Me._motorImpulse) + Me._impulse.Z)))
            Else
                Me._impulse = Vector3.Zero
                Me._motorImpulse = 0!
            End If
        End Sub

        Public Function IsLimitEnabled() As Boolean
            Return Me._enableLimit
        End Function

        Public Function IsMotorEnabled() As Boolean
            Return Me._enableMotor
        End Function

        Public Sub SetLimits(ByVal lower As Single, ByVal upper As Single)
            Debug.Assert((lower <= upper))
            MyBase._bodyA.WakeUp()
            MyBase._bodyB.WakeUp()
            Me._lowerAngle = lower
            Me._upperAngle = upper
        End Sub

        Public Sub SetMaxMotorTorque(ByVal torque As Single)
            MyBase._bodyA.WakeUp()
            MyBase._bodyB.WakeUp()
            Me._maxMotorTorque = torque
        End Sub

        Public Sub SetMotorSpeed(ByVal speed As Single)
            MyBase._bodyA.WakeUp()
            MyBase._bodyB.WakeUp()
            Me._motorSpeed = speed
        End Sub

        Friend Overrides Function SolvePositionConstraints(ByVal baumgarte As Single) As Boolean
            Dim form As XForm
            Dim form2 As XForm
            Dim mat4 As Mat22
            Dim mat5 As Mat22
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            Dim num As Single = 0!
            Dim num2 As Single = 0!
            If (Me._enableLimit AndAlso (Me._limitState > LimitState.Inactive)) Then
                Dim num3 As Single = ((body2._sweep.a - body._sweep.a) - Me._referenceAngle)
                Dim num4 As Single = 0!
                If (Me._limitState = LimitState.Equal) Then
                    Dim num5 As Single = MathUtils.Clamp((num3 - Me._lowerAngle), -Settings.b2_maxAngularCorrection, Settings.b2_maxAngularCorrection)
                    num4 = (-Me._motorMass * num5)
                    num = Math.Abs(num5)
                ElseIf (Me._limitState = LimitState.AtLower) Then
                    Dim num6 As Single = (num3 - Me._lowerAngle)
                    num = -num6
                    num6 = MathUtils.Clamp((num6 + Settings.b2_angularSlop), -Settings.b2_maxAngularCorrection, 0!)
                    num4 = (-Me._motorMass * num6)
                ElseIf (Me._limitState = LimitState.AtUpper) Then
                    Dim num7 As Single = (num3 - Me._upperAngle)
                    num = num7
                    num7 = MathUtils.Clamp((num7 - Settings.b2_angularSlop), 0!, Settings.b2_maxAngularCorrection)
                    num4 = (-Me._motorMass * num7)
                End If
                body._sweep.a = (body._sweep.a - (body._invI * num4))
                body2._sweep.a = (body2._sweep.a + (body2._invI * num4))
                body.SynchronizeTransform()
                body2.SynchronizeTransform()
            End If
            body.GetXForm(form)
            body2.GetXForm(form2)
            Dim a As Vector2 = MathUtils.Multiply(form.R, (Me._localAnchor1 - body.GetLocalCenter))
            Dim vector2 As Vector2 = MathUtils.Multiply(form2.R, (Me._localAnchor2 - body2.GetLocalCenter))
            Dim vec As Vector2 = (((body2._sweep.c + vector2) - body._sweep.c) - a)
            num2 = vec.Length
            Dim num8 As Single = body._invMass
            Dim num9 As Single = body2._invMass
            Dim num10 As Single = body._invI
            Dim num11 As Single = body2._invI
            Dim num12 As Single = (10.0! * Settings.b2_linearSlop)
            If (vec.LengthSquared > (num12 * num12)) Then
                Extension.Normalize(vec)
                Dim num13 As Single = (num8 + num9)
                Debug.Assert((num13 > Settings.b2_FLT_EPSILON))
                Dim num14 As Single = (1.0! / num13)
                Dim vector6 As Vector2 = (num14 * -vec)
                Dim num15 As Single = 0.5!
                body._sweep.c = (body._sweep.c - ((num15 * num8) * vector6))
                body2._sweep.c = (body2._sweep.c + ((num15 * num9) * vector6))
                vec = (((body2._sweep.c + vector2) - body._sweep.c) - a)
            End If
            Dim mat As New Mat22(New Vector2((num8 + num9), 0!), New Vector2(0!, (num8 + num9)))
            Dim b As New Mat22(New Vector2(((num10 * a.Y) * a.Y), ((-num10 * a.X) * a.Y)), New Vector2(((-num10 * a.X) * a.Y), ((num10 * a.X) * a.X)))
            Dim mat3 As New Mat22(New Vector2(((num11 * vector2.Y) * vector2.Y), ((-num11 * vector2.X) * vector2.Y)), New Vector2(((-num11 * vector2.X) * vector2.Y), ((num11 * vector2.X) * vector2.X)))
            Mat22.Add(mat, b, mat4)
            Mat22.Add(mat4, mat3, mat5)
            Dim vector4 As Vector2 = mat5.Solve(-vec)
            body._sweep.c = (body._sweep.c - (body._invMass * vector4))
            body._sweep.a = (body._sweep.a - (body._invI * MathUtils.Cross(a, vector4)))
            body2._sweep.c = (body2._sweep.c + (body2._invMass * vector4))
            body2._sweep.a = (body2._sweep.a + (body2._invI * MathUtils.Cross(vector2, vector4)))
            body.SynchronizeTransform()
            body2.SynchronizeTransform()
            Return ((num2 <= Settings.b2_linearSlop) AndAlso (num <= Settings.b2_angularSlop))
        End Function

        Friend Overrides Sub SolveVelocityConstraints(ByRef [step] As TimeStep)
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            Dim vector As Vector2 = body._linearVelocity
            Dim s As Single = body._angularVelocity
            Dim vector2 As Vector2 = body2._linearVelocity
            Dim num2 As Single = body2._angularVelocity
            Dim num3 As Single = body._invMass
            Dim num4 As Single = body2._invMass
            Dim num5 As Single = body._invI
            Dim num6 As Single = body2._invI
            If (Me._enableMotor AndAlso (Me._limitState <> LimitState.Equal)) Then
                Dim num7 As Single = ((num2 - s) - Me._motorSpeed)
                Dim num8 As Single = (Me._motorMass * -num7)
                Dim num9 As Single = Me._motorImpulse
                Dim high As Single = ([step].dt * Me._maxMotorTorque)
                Me._motorImpulse = MathUtils.Clamp((Me._motorImpulse + num8), -high, high)
                num8 = (Me._motorImpulse - num9)
                s = (s - (num5 * num8))
                num2 = (num2 + (num6 * num8))
            End If
            If (Me._enableLimit AndAlso (Me._limitState > LimitState.Inactive)) Then
                Dim form As XForm
                Dim form2 As XForm
                body.GetXForm(form)
                body2.GetXForm(form2)
                Dim a As Vector2 = MathUtils.Multiply(form.R, (Me._localAnchor1 - body.GetLocalCenter))
                Dim vector4 As Vector2 = MathUtils.Multiply(form2.R, (Me._localAnchor2 - body2.GetLocalCenter))
                Dim vector5 As Vector2 = (((vector2 + MathUtils.Cross(num2, vector4)) - vector) - MathUtils.Cross(s, a))
                Dim num11 As Single = (num2 - s)
                Dim vector6 As New Vector3(vector5.X, vector5.Y, num11)
                Dim vector7 As Vector3 = Me._mass.Solve33(-vector6)
                If (Me._limitState = LimitState.Equal) Then
                    Me._impulse = (Me._impulse + vector7)
                ElseIf (Me._limitState = LimitState.AtLower) Then
                    Dim num12 As Single = (Me._impulse.Z + vector7.Z)
                    If (num12 < 0!) Then
                        Dim vector9 As Vector2 = Me._mass.Solve22(-vector5)
                        vector7.X = vector9.X
                        vector7.Y = vector9.Y
                        vector7.Z = -Me._impulse.Z
                        Me._impulse.X = (Me._impulse.X + vector9.X)
                        Me._impulse.Y = (Me._impulse.Y + vector9.Y)
                        Me._impulse.Z = 0!
                    End If
                ElseIf (Me._limitState = LimitState.AtUpper) Then
                    Dim num13 As Single = (Me._impulse.Z + vector7.Z)
                    If (num13 > 0!) Then
                        Dim vector10 As Vector2 = Me._mass.Solve22(-vector5)
                        vector7.X = vector10.X
                        vector7.Y = vector10.Y
                        vector7.Z = -Me._impulse.Z
                        Me._impulse.X = (Me._impulse.X + vector10.X)
                        Me._impulse.Y = (Me._impulse.Y + vector10.Y)
                        Me._impulse.Z = 0!
                    End If
                End If
                Dim b As New Vector2(vector7.X, vector7.Y)
                vector = (vector - (num3 * b))
                s = (s - (num5 * (MathUtils.Cross(a, b) + vector7.Z)))
                vector2 = (vector2 + (num4 * b))
                num2 = (num2 + (num6 * (MathUtils.Cross(vector4, b) + vector7.Z)))
            Else
                Dim form3 As XForm
                Dim form4 As XForm
                body.GetXForm(form3)
                body2.GetXForm(form4)
                Dim vector11 As Vector2 = MathUtils.Multiply(form3.R, (Me._localAnchor1 - body.GetLocalCenter))
                Dim vector12 As Vector2 = MathUtils.Multiply(form4.R, (Me._localAnchor2 - body2.GetLocalCenter))
                Dim vector13 As Vector2 = (((vector2 + MathUtils.Cross(num2, vector12)) - vector) - MathUtils.Cross(s, vector11))
                Dim vector14 As Vector2 = Me._mass.Solve22(-vector13)
                Me._impulse.X = (Me._impulse.X + vector14.X)
                Me._impulse.Y = (Me._impulse.Y + vector14.Y)
                vector = (vector - (num3 * vector14))
                s = (s - (num5 * MathUtils.Cross(vector11, vector14)))
                vector2 = (vector2 + (num4 * vector14))
                num2 = (num2 + (num6 * MathUtils.Cross(vector12, vector14)))
            End If
            body._linearVelocity = vector
            body._angularVelocity = s
            body2._linearVelocity = vector2
            body2._angularVelocity = num2
        End Sub


        ' Fields
        Public _enableLimit As Boolean
        Public _enableMotor As Boolean
        Public _impulse As Vector3
        Public _limitState As LimitState
        Public _localAnchor1 As Vector2
        Public _localAnchor2 As Vector2
        Public _lowerAngle As Single
        Public _mass As Mat33
        Public _maxMotorTorque As Single
        Public _motorImpulse As Single
        Public _motorMass As Single
        Public _motorSpeed As Single
        Public _referenceAngle As Single
        Public _upperAngle As Single
    End Class
End Namespace

