Imports System
Imports System.Diagnostics
Imports System.Numerics

Namespace Global.Box2D
    Public Class DistanceJoint
        Inherits Joint
        ' Methods
        Friend Sub New(ByVal def As DistanceJointDef)
            MyBase.New(def)
            Me._localAnchor1 = def.localAnchor1
            Me._localAnchor2 = def.localAnchor2
            Me._length = def.length
            Me._frequencyHz = def.frequencyHz
            Me._dampingRatio = def.dampingRatio
            Me._impulse = 0!
            Me._gamma = 0!
            Me._bias = 0!
        End Sub

        Public Overrides Function GetAnchor1() As Vector2
            Return MyBase._bodyA.GetWorldPoint(Me._localAnchor1)
        End Function

        Public Overrides Function GetAnchor2() As Vector2
            Return MyBase._bodyB.GetWorldPoint(Me._localAnchor2)
        End Function

        Public Overrides Function GetReactionForce(ByVal inv_dt As Single) As Vector2
            Return ((inv_dt * Me._impulse) * Me._u)
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
            Me._u = (((body2._sweep.c + vector2) - body._sweep.c) - a)
            Dim num As Single = Me._u.Length
            If (num > Settings.b2_linearSlop) Then
                Me._u = (Me._u * (1! / num))
            Else
                Me._u = New Vector2(0!, 0!)
            End If
            Dim num2 As Single = MathUtils.Cross(a, Me._u)
            Dim num3 As Single = MathUtils.Cross(vector2, Me._u)
            Dim num4 As Single = (((body._invMass + ((body._invI * num2) * num2)) + body2._invMass) + ((body2._invI * num3) * num3))
            Debug.Assert((num4 > Settings.b2_FLT_EPSILON))
            Me._mass = (1! / num4)
            If (Me._frequencyHz > 0!) Then
                Dim num5 As Single = (num - Me._length)
                Dim num6 As Single = ((2! * Settings.b2_pi) * Me._frequencyHz)
                Dim num7 As Single = (((2! * Me._mass) * Me._dampingRatio) * num6)
                Dim num8 As Single = ((Me._mass * num6) * num6)
                Me._gamma = (1! / ([step].dt * (num7 + ([step].dt * num8))))
                Me._bias = (((num5 * [step].dt) * num8) * Me._gamma)
                Me._mass = (1! / (num4 + Me._gamma))
            End If
            If [step].warmStarting Then
                Me._impulse = (Me._impulse * [step].dtRatio)
                Dim b As Vector2 = (Me._impulse * Me._u)
                body._linearVelocity = (body._linearVelocity - (body._invMass * b))
                body._angularVelocity = (body._angularVelocity - (body._invI * MathUtils.Cross(a, b)))
                body2._linearVelocity = (body2._linearVelocity + (body2._invMass * b))
                body2._angularVelocity = (body2._angularVelocity + (body2._invI * MathUtils.Cross(vector2, b)))
            Else
                Me._impulse = 0!
            End If
        End Sub

        Friend Overrides Function SolvePositionConstraints(ByVal baumgarte As Single) As Boolean
            Dim form As XForm
            Dim form2 As XForm
            If (Me._frequencyHz > 0!) Then
                Return True
            End If
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            body.GetXForm(form)
            body2.GetXForm(form2)
            Dim a As Vector2 = MathUtils.Multiply(form.RoateMatrix, (Me._localAnchor1 - body.GetLocalCenter))
            Dim vector2 As Vector2 = MathUtils.Multiply(form2.RoateMatrix, (Me._localAnchor2 - body2.GetLocalCenter))
            Dim vector3 As Vector2 = (((body2._sweep.c + vector2) - body._sweep.c) - a)
            Dim num As Single = vector3.Length
            vector3 = (vector3 / num)
            Dim num2 As Single = (num - Me._length)
            num2 = MathUtils.Clamp(num2, -Settings.b2_maxLinearCorrection, Settings.b2_maxLinearCorrection)
            Dim num3 As Single = (-Me._mass * num2)
            Me._u = vector3
            Dim b As Vector2 = (num3 * Me._u)
            body._sweep.c = (body._sweep.c - (body._invMass * b))
            body._sweep.a = (body._sweep.a - (body._invI * MathUtils.Cross(a, b)))
            body2._sweep.c = (body2._sweep.c + (body2._invMass * b))
            body2._sweep.a = (body2._sweep.a + (body2._invI * MathUtils.Cross(vector2, b)))
            body.SynchronizeTransform
            body2.SynchronizeTransform
            Return (Math.Abs(num2) < Settings.b2_linearSlop)
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
            Dim vector3 As Vector2 = (body._linearVelocity + MathUtils.Cross(body._angularVelocity, a))
            Dim vector4 As Vector2 = (body2._linearVelocity + MathUtils.Cross(body2._angularVelocity, vector2))
            Dim num As Single = Vector2.Dot(Me._u, (vector4 - vector3))
            Dim num2 As Single = (-Me._mass * ((num + Me._bias) + (Me._gamma * Me._impulse)))
            Me._impulse = (Me._impulse + num2)
            Dim b As Vector2 = (num2 * Me._u)
            body._linearVelocity = (body._linearVelocity - (body._invMass * b))
            body._angularVelocity = (body._angularVelocity - (body._invI * MathUtils.Cross(a, b)))
            body2._linearVelocity = (body2._linearVelocity + (body2._invMass * b))
            body2._angularVelocity = (body2._angularVelocity + (body2._invI * MathUtils.Cross(vector2, b)))
        End Sub


        ' Fields
        Friend _bias As Single
        Friend _dampingRatio As Single
        Friend _frequencyHz As Single
        Friend _gamma As Single
        Friend _impulse As Single
        Friend _length As Single
        Friend _localAnchor1 As Vector2
        Friend _localAnchor2 As Vector2
        Friend _mass As Single
        Friend _u As Vector2
    End Class
End Namespace

