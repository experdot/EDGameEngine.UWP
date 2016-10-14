Imports System
Imports System.Diagnostics
Imports System.Numerics

Namespace Global.Box2D
    Public Class MouseJoint
        Inherits Joint
        ' Methods
        Friend Sub New(ByVal def As MouseJointDef)
            MyBase.New(def)
            Dim form As XForm
            MyBase._bodyB.GetXForm(form)
            Me._target = def.target
            Me._localAnchor = MathUtils.MultiplyT(form, Me._target)
            Me._maxForce = def.maxForce
            Me._impulse = Vector2.Zero
            Me._frequencyHz = def.frequencyHz
            Me._dampingRatio = def.dampingRatio
            Me._beta = 0!
            Me._gamma = 0!
        End Sub

        Public Overrides Function GetAnchor1() As Vector2
            Return Me._target
        End Function

        Public Overrides Function GetAnchor2() As Vector2
            Return MyBase._bodyB.GetWorldPoint(Me._localAnchor)
        End Function

        Public Overrides Function GetReactionForce(ByVal inv_dt As Single) As Vector2
            Return (inv_dt * Me._impulse)
        End Function

        Public Overrides Function GetReactionTorque(ByVal inv_dt As Single) As Single
            Return (inv_dt * 0!)
        End Function

        Friend Overrides Sub InitVelocityConstraints(ByRef [step] As TimeStep)
            Dim form As XForm
            Dim mat3 As Mat22
            Dim body As Body = MyBase._bodyB
            Dim mass As Single = body.GetMass
            Dim num2 As Single = ((2! * Settings.b2_pi) * Me._frequencyHz)
            Dim num3 As Single = (((2! * mass) * Me._dampingRatio) * num2)
            Dim num4 As Single = (mass * (num2 * num2))
            Debug.Assert(((num3 + ([step].dt * num4)) > Settings.b2_FLT_EPSILON))
            Me._gamma = (1! / ([step].dt * (num3 + ([step].dt * num4))))
            Me._beta = (([step].dt * num4) * Me._gamma)
            body.GetXForm(form)
            Dim a As Vector2 = MathUtils.Multiply(form.RoateMatrix, (Me._localAnchor - body.GetLocalCenter))
            Dim num5 As Single = body._invMass
            Dim num6 As Single = body._invI
            Dim mat As New Mat22(New Vector2(num5, 0!), New Vector2(0!, num5))
            Dim b As New Mat22(New Vector2(((num6 * a.Y) * a.Y), ((-num6 * a.X) * a.Y)), New Vector2(((-num6 * a.X) * a.Y), ((num6 * a.X) * a.X)))
            Mat22.Add(mat, b, mat3)
            mat3.Column1.X = (mat3.Column1.X + Me._gamma)
            mat3.Column2.Y = (mat3.Column2.Y + Me._gamma)
            Me._mass = mat3.GetInverse
            Me._C = ((body._sweep.c + a) - Me._target)
            body._angularVelocity = (body._angularVelocity * 0.98!)
            Me._impulse = (Me._impulse * [step].dtRatio)
            body._linearVelocity = (body._linearVelocity + (num5 * Me._impulse))
            body._angularVelocity = (body._angularVelocity + (num6 * MathUtils.Cross(a, Me._impulse)))
        End Sub

        Public Sub SetTarget(ByVal target As Vector2)
            If MyBase._bodyB.IsSleeping Then
                MyBase._bodyB.WakeUp()
            End If
            Me._target = target
        End Sub

        Friend Overrides Function SolvePositionConstraints(ByVal baumgarte As Single) As Boolean
            Return True
        End Function

        Friend Overrides Sub SolveVelocityConstraints(ByRef [step] As TimeStep)
            Dim form As XForm
            Dim body As Body = MyBase._bodyB
            body.GetXForm(form)
            Dim a As Vector2 = MathUtils.Multiply(form.RoateMatrix, (Me._localAnchor - body.GetLocalCenter))
            Dim vector2 As Vector2 = (body._linearVelocity + MathUtils.Cross(body._angularVelocity, a))
            Dim b As Vector2 = MathUtils.Multiply(Me._mass, -((vector2 + (Me._beta * Me._C)) + (Me._gamma * Me._impulse)))
            Dim vector4 As Vector2 = Me._impulse
            Me._impulse = (Me._impulse + b)
            Dim num As Single = ([step].dt * Me._maxForce)
            If (Me._impulse.LengthSquared > (num * num)) Then
                Me._impulse = (Me._impulse * (num / Me._impulse.Length))
            End If
            b = (Me._impulse - vector4)
            body._linearVelocity = (body._linearVelocity + (body._invMass * b))
            body._angularVelocity = (body._angularVelocity + (body._invI * MathUtils.Cross(a, b)))
        End Sub


        ' Fields
        Public _beta As Single
        Public _C As Vector2
        Public _dampingRatio As Single
        Public _frequencyHz As Single
        Public _gamma As Single
        Public _impulse As Vector2
        Public _localAnchor As Vector2
        Public _mass As Mat22
        Public _maxForce As Single
        Public _target As Vector2
    End Class
End Namespace

