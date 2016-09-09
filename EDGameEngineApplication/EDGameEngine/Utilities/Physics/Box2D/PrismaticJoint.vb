Imports System
Imports System.Diagnostics
Imports System.Numerics

Namespace Global.Box2D
    Public Class PrismaticJoint
        Inherits Joint
        ' Methods
        Friend Sub New(ByVal def As PrismaticJointDef)
            MyBase.New(def)
            Me._localAnchor1 = def.localAnchor1
            Me._localAnchor2 = def.localAnchor2
            Me._localXAxis1 = def.localAxis1
            Me._localYAxis1 = MathUtils.Cross(CSng(1.0!), Me._localXAxis1)
            Me._refAngle = def.referenceAngle
            Me._impulse = Vector3.Zero
            Me._motorMass = 0!
            Me._motorImpulse = 0!
            Me._lowerTranslation = def.lowerTranslation
            Me._upperTranslation = def.upperTranslation
            Me._maxMotorForce = def.maxMotorForce
            Me._motorSpeed = def.motorSpeed
            Me._enableLimit = def.enableLimit
            Me._enableMotor = def.enableMotor
            Me._limitState = LimitState.Inactive
            Me._axis = Vector2.Zero
            Me._perp = Vector2.Zero
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

        Public Function GetJointSpeed() As Single
            Dim form As XForm
            Dim form2 As XForm
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            body.GetXForm(form)
            body2.GetXForm(form2)
            Dim a As Vector2 = MathUtils.Multiply(form.R, (Me._localAnchor1 - body.GetLocalCenter))
            Dim vector2 As Vector2 = MathUtils.Multiply(form2.R, (Me._localAnchor2 - body2.GetLocalCenter))
            Dim vector3 As Vector2 = (body._sweep.c + a)
            Dim vector4 As Vector2 = (body2._sweep.c + vector2)
            Dim vector5 As Vector2 = (vector4 - vector3)
            Dim worldVector As Vector2 = body.GetWorldVector(Me._localXAxis1)
            Dim vector7 As Vector2 = body._linearVelocity
            Dim vector8 As Vector2 = body2._linearVelocity
            Dim s As Single = body._angularVelocity
            Dim num2 As Single = body2._angularVelocity
            Return (Vector2.Dot(vector5, MathUtils.Cross(s, worldVector)) + Vector2.Dot(worldVector, (((vector8 + MathUtils.Cross(num2, vector2)) - vector7) - MathUtils.Cross(s, a))))
        End Function

        Public Function GetJointTranslation() As Single
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            Dim worldPoint As Vector2 = body.GetWorldPoint(Me._localAnchor1)
            Dim vector3 As Vector2 = (body2.GetWorldPoint(Me._localAnchor2) - worldPoint)
            Dim worldVector As Vector2 = body.GetWorldVector(Me._localXAxis1)
            Return Vector2.Dot(vector3, worldVector)
        End Function

        Public Function GetLowerLimit() As Single
            Return Me._lowerTranslation
        End Function

        Public Function GetMotorForce() As Single
            Return Me._motorImpulse
        End Function

        Public Function GetMotorSpeed() As Single
            Return Me._motorSpeed
        End Function

        Public Overrides Function GetReactionForce(ByVal inv_dt As Single) As Vector2
            Return (inv_dt * ((Me._impulse.X * Me._perp) + ((Me._motorImpulse + Me._impulse.Z) * Me._axis)))
        End Function

        Public Overrides Function GetReactionTorque(ByVal inv_dt As Single) As Single
            Return (inv_dt * Me._impulse.Y)
        End Function

        Public Function GetUpperLimit() As Single
            Return Me._upperTranslation
        End Function

        Friend Overrides Sub InitVelocityConstraints(ByRef [step] As TimeStep)
            Dim form As XForm
            Dim form2 As XForm
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            Debug.Assert(((body._invI > 0!) OrElse (body2._invI > 0!)))
            MyBase._localCenter1 = body.GetLocalCenter
            MyBase._localCenter2 = body2.GetLocalCenter
            body.GetXForm(form)
            body2.GetXForm(form2)
            Dim vector As Vector2 = MathUtils.Multiply(form.R, (Me._localAnchor1 - MyBase._localCenter1))
            Dim a As Vector2 = MathUtils.Multiply(form2.R, (Me._localAnchor2 - MyBase._localCenter2))
            Dim vector3 As Vector2 = (((body2._sweep.c + a) - body._sweep.c) - vector)
            MyBase._invMass1 = body._invMass
            MyBase._invI1 = body._invI
            MyBase._invMass2 = body2._invMass
            MyBase._invI2 = body2._invI
            Me._axis = MathUtils.Multiply(form.R, Me._localXAxis1)
            Me._a1 = MathUtils.Cross((vector3 + vector), Me._axis)
            Me._a2 = MathUtils.Cross(a, Me._axis)
            Me._motorMass = (((MyBase._invMass1 + MyBase._invMass2) + ((MyBase._invI1 * Me._a1) * Me._a1)) + ((MyBase._invI2 * Me._a2) * Me._a2))
            Debug.Assert((Me._motorMass > Settings.b2_FLT_EPSILON))
            Me._motorMass = (1.0! / Me._motorMass)
            Me._perp = MathUtils.Multiply(form.R, Me._localYAxis1)
            Me._s1 = MathUtils.Cross((vector3 + vector), Me._perp)
            Me._s2 = MathUtils.Cross(a, Me._perp)
            Dim num As Single = MyBase._invMass1
            Dim num2 As Single = MyBase._invMass2
            Dim num3 As Single = MyBase._invI1
            Dim num4 As Single = MyBase._invI2
            Dim num5 As Single = (((num + num2) + ((num3 * Me._s1) * Me._s1)) + ((num4 * Me._s2) * Me._s2))
            Dim num6 As Single = ((num3 * Me._s1) + (num4 * Me._s2))
            Dim num7 As Single = (((num3 * Me._s1) * Me._a1) + ((num4 * Me._s2) * Me._a2))
            Dim num8 As Single = (num3 + num4)
            Dim num9 As Single = ((num3 * Me._a1) + (num4 * Me._a2))
            Dim num10 As Single = (((num + num2) + ((num3 * Me._a1) * Me._a1)) + ((num4 * Me._a2) * Me._a2))
            Me._K.col1 = New Vector3(num5, num6, num7)
            Me._K.col2 = New Vector3(num6, num8, num9)
            Me._K.col3 = New Vector3(num7, num9, num10)
            If Me._enableLimit Then
                Dim num11 As Single = Vector2.Dot(Me._axis, vector3)
                If (Math.Abs((Me._upperTranslation - Me._lowerTranslation)) < (2.0! * Settings.b2_linearSlop)) Then
                    Me._limitState = LimitState.Equal
                ElseIf (num11 <= Me._lowerTranslation) Then
                    If (Me._limitState <> LimitState.AtLower) Then
                        Me._limitState = LimitState.AtLower
                        Me._impulse.Z = 0!
                    End If
                ElseIf (num11 >= Me._upperTranslation) Then
                    If (Me._limitState <> LimitState.AtUpper) Then
                        Me._limitState = LimitState.AtUpper
                        Me._impulse.Z = 0!
                    End If
                Else
                    Me._limitState = LimitState.Inactive
                    Me._impulse.Z = 0!
                End If
            Else
                Me._limitState = LimitState.Inactive
            End If
            If Not Me._enableMotor Then
                Me._motorImpulse = 0!
            End If
            If [step].warmStarting Then
                Me._impulse = (Me._impulse * [step].dtRatio)
                Me._motorImpulse = (Me._motorImpulse * [step].dtRatio)
                Dim vector4 As Vector2 = ((Me._impulse.X * Me._perp) + ((Me._motorImpulse + Me._impulse.Z) * Me._axis))
                Dim num12 As Single = (((Me._impulse.X * Me._s1) + Me._impulse.Y) + ((Me._motorImpulse + Me._impulse.Z) * Me._a1))
                Dim num13 As Single = (((Me._impulse.X * Me._s2) + Me._impulse.Y) + ((Me._motorImpulse + Me._impulse.Z) * Me._a2))
                body._linearVelocity = (body._linearVelocity - (MyBase._invMass1 * vector4))
                body._angularVelocity = (body._angularVelocity - (MyBase._invI1 * num12))
                body2._linearVelocity = (body2._linearVelocity + (MyBase._invMass2 * vector4))
                body2._angularVelocity = (body2._angularVelocity + (MyBase._invI2 * num13))
            Else
                Me._impulse = Numerics.Vector3.Zero
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
            Me._lowerTranslation = lower
            Me._upperTranslation = upper
        End Sub

        Public Sub SetMaxMotorForce(ByVal force As Single)
            MyBase._bodyA.WakeUp()
            MyBase._bodyB.WakeUp()
            Me._maxMotorForce = force
        End Sub

        Public Sub SetMotorSpeed(ByVal speed As Single)
            MyBase._bodyA.WakeUp()
            MyBase._bodyB.WakeUp()
            Me._motorSpeed = speed
        End Sub

        Friend Overrides Function SolvePositionConstraints(ByVal baumgarte As Single) As Boolean
            Dim vector6 As Vector3
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            Dim c As Vector2 = body._sweep.c
            Dim a As Single = body._sweep.a
            Dim vector2 As Vector2 = body2._sweep.c
            Dim angle As Single = body2._sweep.a
            Dim num3 As Single = 0!
            Dim num4 As Single = 0!
            Dim flag As Boolean = False
            Dim num5 As Single = 0!
            Dim mat As New Mat22(a)
            Dim mat2 As New Mat22(angle)
            Dim vector3 As Vector2 = MathUtils.Multiply(mat, (Me._localAnchor1 - MyBase._localCenter1))
            Dim vector4 As Vector2 = MathUtils.Multiply(mat2, (Me._localAnchor2 - MyBase._localCenter2))
            Dim vector5 As Vector2 = (((vector2 + vector4) - c) - vector3)
            If Me._enableLimit Then
                Me._axis = MathUtils.Multiply(mat, Me._localXAxis1)
                Me._a1 = MathUtils.Cross((vector5 + vector3), Me._axis)
                Me._a2 = MathUtils.Cross(vector4, Me._axis)
                Dim num8 As Single = Vector2.Dot(Me._axis, vector5)
                If (Math.Abs((Me._upperTranslation - Me._lowerTranslation)) < (2.0! * Settings.b2_linearSlop)) Then
                    num5 = MathUtils.Clamp(num8, -Settings.b2_maxLinearCorrection, Settings.b2_maxLinearCorrection)
                    num3 = Math.Abs(num8)
                    flag = True
                ElseIf (num8 <= Me._lowerTranslation) Then
                    num5 = MathUtils.Clamp(((num8 - Me._lowerTranslation) + Settings.b2_linearSlop), -Settings.b2_maxLinearCorrection, 0!)
                    num3 = (Me._lowerTranslation - num8)
                    flag = True
                ElseIf (num8 >= Me._upperTranslation) Then
                    num5 = MathUtils.Clamp(((num8 - Me._upperTranslation) - Settings.b2_linearSlop), 0!, Settings.b2_maxLinearCorrection)
                    num3 = (num8 - Me._upperTranslation)
                    flag = True
                End If
            End If
            Me._perp = MathUtils.Multiply(mat, Me._localYAxis1)
            Me._s1 = MathUtils.Cross((vector5 + vector3), Me._perp)
            Me._s2 = MathUtils.Cross(vector4, Me._perp)
            Dim vector7 As New Vector2(Vector2.Dot(Me._perp, vector5), ((angle - a) - Me._refAngle))
            num3 = Math.Max(num3, Math.Abs(vector7.X))
            num4 = Math.Abs(vector7.Y)
            If flag Then
                Dim num9 As Single = MyBase._invMass1
                Dim num10 As Single = MyBase._invMass2
                Dim num11 As Single = MyBase._invI1
                Dim num12 As Single = MyBase._invI2
                Dim num13 As Single = (((num9 + num10) + ((num11 * Me._s1) * Me._s1)) + ((num12 * Me._s2) * Me._s2))
                Dim num14 As Single = ((num11 * Me._s1) + (num12 * Me._s2))
                Dim num15 As Single = (((num11 * Me._s1) * Me._a1) + ((num12 * Me._s2) * Me._a2))
                Dim num16 As Single = (num11 + num12)
                Dim num17 As Single = ((num11 * Me._a1) + (num12 * Me._a2))
                Dim num18 As Single = (((num9 + num10) + ((num11 * Me._a1) * Me._a1)) + ((num12 * Me._a2) * Me._a2))
                Me._K.col1 = New Vector3(num13, num14, num15)
                Me._K.col2 = New Vector3(num14, num16, num17)
                Me._K.col3 = New Vector3(num15, num17, num18)
                Dim b As New Vector3(-vector7.X, -vector7.Y, -num5)
                vector6 = Me._K.Solve33(b)
            Else
                Dim num19 As Single = MyBase._invMass1
                Dim num20 As Single = MyBase._invMass2
                Dim num21 As Single = MyBase._invI1
                Dim num22 As Single = MyBase._invI2
                Dim num23 As Single = (((num19 + num20) + ((num21 * Me._s1) * Me._s1)) + ((num22 * Me._s2) * Me._s2))
                Dim num24 As Single = ((num21 * Me._s1) + (num22 * Me._s2))
                Dim num25 As Single = (num21 + num22)
                Me._K.col1 = New Vector3(num23, num24, 0!)
                Me._K.col2 = New Vector3(num24, num25, 0!)
                Dim vector10 As Vector2 = Me._K.Solve22(-vector7)
                vector6.X = vector10.X
                vector6.Y = vector10.Y
                vector6.Z = 0!
            End If
            Dim vector8 As Vector2 = ((vector6.X * Me._perp) + (vector6.Z * Me._axis))
            Dim num6 As Single = (((vector6.X * Me._s1) + vector6.Y) + (vector6.Z * Me._a1))
            Dim num7 As Single = (((vector6.X * Me._s2) + vector6.Y) + (vector6.Z * Me._a2))
            c = (c - (MyBase._invMass1 * vector8))
            a = (a - (MyBase._invI1 * num6))
            vector2 = (vector2 + (MyBase._invMass2 * vector8))
            angle = (angle + (MyBase._invI2 * num7))
            body._sweep.c = c
            body._sweep.a = a
            body2._sweep.c = vector2
            body2._sweep.a = angle
            body.SynchronizeTransform()
            body2.SynchronizeTransform()
            Return ((num3 <= Settings.b2_linearSlop) AndAlso (num4 <= Settings.b2_angularSlop))
        End Function

        Friend Overrides Sub SolveVelocityConstraints(ByRef [step] As TimeStep)
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            Dim vector As Vector2 = body._linearVelocity
            Dim num As Single = body._angularVelocity
            Dim vector2 As Vector2 = body2._linearVelocity
            Dim num2 As Single = body2._angularVelocity
            If (Me._enableMotor AndAlso (Me._limitState <> LimitState.Equal)) Then
                Dim num3 As Single = ((Vector2.Dot(Me._axis, (vector2 - vector)) + (Me._a2 * num2)) - (Me._a1 * num))
                Dim num4 As Single = (Me._motorMass * (Me._motorSpeed - num3))
                Dim num5 As Single = Me._motorImpulse
                Dim high As Single = ([step].dt * Me._maxMotorForce)
                Me._motorImpulse = MathUtils.Clamp((Me._motorImpulse + num4), -high, high)
                num4 = (Me._motorImpulse - num5)
                Dim vector4 As Vector2 = (num4 * Me._axis)
                Dim num7 As Single = (num4 * Me._a1)
                Dim num8 As Single = (num4 * Me._a2)
                vector = (vector - (MyBase._invMass1 * vector4))
                num = (num - (MyBase._invI1 * num7))
                vector2 = (vector2 + (MyBase._invMass2 * vector4))
                num2 = (num2 + (MyBase._invI2 * num8))
            End If
            Dim vector3 As New Vector2(((Vector2.Dot(Me._perp, (vector2 - vector)) + (Me._s2 * num2)) - (Me._s1 * num)), (num2 - num))
            If (Me._enableLimit AndAlso (Me._limitState > LimitState.Inactive)) Then
                Dim num9 As Single = ((Vector2.Dot(Me._axis, (vector2 - vector)) + (Me._a2 * num2)) - (Me._a1 * num))
                Dim vector5 As New Vector3(vector3.X, vector3.Y, num9)
                Dim vector6 As Vector3 = Me._impulse
                Dim vector7 As Vector3 = Me._K.Solve33(-vector5)
                Me._impulse = (Me._impulse + vector7)
                If (Me._limitState = LimitState.AtLower) Then
                    Me._impulse.Z = Math.Max(Me._impulse.Z, 0!)
                ElseIf (Me._limitState = LimitState.AtUpper) Then
                    Me._impulse.Z = Math.Min(Me._impulse.Z, 0!)
                End If
                Dim b As Vector2 = (-vector3 - ((Me._impulse.Z - vector6.Z) * New Vector2(Me._K.col3.X, Me._K.col3.Y)))
                Dim vector9 As Vector2 = (Me._K.Solve22(b) + New Vector2(vector6.X, vector6.Y))
                Me._impulse.X = vector9.X
                Me._impulse.Y = vector9.Y
                vector7 = (Me._impulse - vector6)
                Dim vector10 As Vector2 = ((vector7.X * Me._perp) + (vector7.Z * Me._axis))
                Dim num10 As Single = (((vector7.X * Me._s1) + vector7.Y) + (vector7.Z * Me._a1))
                Dim num11 As Single = (((vector7.X * Me._s2) + vector7.Y) + (vector7.Z * Me._a2))
                vector = (vector - (MyBase._invMass1 * vector10))
                num = (num - (MyBase._invI1 * num10))
                vector2 = (vector2 + (MyBase._invMass2 * vector10))
                num2 = (num2 + (MyBase._invI2 * num11))
            Else
                Dim vector11 As Vector2 = Me._K.Solve22(-vector3)
                Me._impulse.X = (Me._impulse.X + vector11.X)
                Me._impulse.Y = (Me._impulse.Y + vector11.Y)
                Dim vector12 As Vector2 = (vector11.X * Me._perp)
                Dim num12 As Single = ((vector11.X * Me._s1) + vector11.Y)
                Dim num13 As Single = ((vector11.X * Me._s2) + vector11.Y)
                vector = (vector - (MyBase._invMass1 * vector12))
                num = (num - (MyBase._invI1 * num12))
                vector2 = (vector2 + (MyBase._invMass2 * vector12))
                num2 = (num2 + (MyBase._invI2 * num13))
            End If
            body._linearVelocity = vector
            body._angularVelocity = num
            body2._linearVelocity = vector2
            body2._angularVelocity = num2
        End Sub


        ' Fields
        Public _a1 As Single
        Public _a2 As Single
        Public _axis As Vector2
        Public _enableLimit As Boolean
        Public _enableMotor As Boolean
        Public _impulse As Vector3
        Public _K As Mat33
        Public _limitState As LimitState
        Public _localAnchor1 As Vector2
        Public _localAnchor2 As Vector2
        Public _localXAxis1 As Vector2
        Public _localYAxis1 As Vector2
        Public _lowerTranslation As Single
        Public _maxMotorForce As Single
        Public _motorImpulse As Single
        Public _motorMass As Single
        Public _motorSpeed As Single
        Public _perp As Vector2
        Public _refAngle As Single
        Public _s1 As Single
        Public _s2 As Single
        Public _upperTranslation As Single
    End Class
End Namespace

