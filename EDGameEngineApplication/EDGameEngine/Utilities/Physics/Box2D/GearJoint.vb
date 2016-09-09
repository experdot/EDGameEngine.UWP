Imports System
Imports System.Diagnostics
Imports System.Numerics

Namespace Global.Box2D
    Public Class GearJoint
        Inherits Joint
        ' Methods
        Friend Sub New(ByVal def As GearJointDef)
            MyBase.New(def)
            Dim jointAngle As Single
            Dim jointTranslation As Single
            Dim jointType As JointType = def.joint1.JointType
            Dim type2 As JointType = def.joint2.JointType
            Debug.Assert(((jointType = JointType.Revolute) OrElse (jointType = JointType.Prismatic)))
            Debug.Assert(((type2 = JointType.Revolute) OrElse (type2 = JointType.Prismatic)))
            Debug.Assert(def.joint1.GetBody1.IsStatic)
            Debug.Assert(def.joint2.GetBody1.IsStatic)
            Me._revolute1 = Nothing
            Me._prismatic1 = Nothing
            Me._revolute2 = Nothing
            Me._prismatic2 = Nothing
            Me._ground1 = def.joint1.GetBody1
            MyBase._bodyA = def.joint1.GetBody2
            If (jointType = JointType.Revolute) Then
                Me._revolute1 = DirectCast(def.joint1, RevoluteJoint)
                Me._groundAnchor1 = Me._revolute1._localAnchor1
                Me._localAnchor1 = Me._revolute1._localAnchor2
                jointAngle = Me._revolute1.GetJointAngle
            Else
                Me._prismatic1 = DirectCast(def.joint1, PrismaticJoint)
                Me._groundAnchor1 = Me._prismatic1._localAnchor1
                Me._localAnchor1 = Me._prismatic1._localAnchor2
                jointAngle = Me._prismatic1.GetJointTranslation
            End If
            Me._ground2 = def.joint2.GetBody1
            MyBase._bodyB = def.joint2.GetBody2
            If (type2 = JointType.Revolute) Then
                Me._revolute2 = DirectCast(def.joint2, RevoluteJoint)
                Me._groundAnchor2 = Me._revolute2._localAnchor1
                Me._localAnchor2 = Me._revolute2._localAnchor2
                jointTranslation = Me._revolute2.GetJointAngle
            Else
                Me._prismatic2 = DirectCast(def.joint2, PrismaticJoint)
                Me._groundAnchor2 = Me._prismatic2._localAnchor1
                Me._localAnchor2 = Me._prismatic2._localAnchor2
                jointTranslation = Me._prismatic2.GetJointTranslation
            End If
            Me._ratio = def.ratio
            Me._ant = (jointAngle + (Me._ratio * jointTranslation))
            Me._impulse = 0!
        End Sub

        Public Overrides Function GetAnchor1() As Vector2
            Return MyBase._bodyA.GetWorldPoint(Me._localAnchor1)
        End Function

        Public Overrides Function GetAnchor2() As Vector2
            Return MyBase._bodyB.GetWorldPoint(Me._localAnchor2)
        End Function

        Public Function GetRatio() As Single
            Return Me._ratio
        End Function

        Public Overrides Function GetReactionForce(ByVal inv_dt As Single) As Vector2
            Dim vector As Vector2 = (Me._impulse * Me._J.linear2)
            Return (inv_dt * vector)
        End Function

        Public Overrides Function GetReactionTorque(ByVal inv_dt As Single) As Single
            Dim form As XForm
            MyBase._bodyB.GetXForm(form)
            Dim a As Vector2 = MathUtils.Multiply(form.R, (Me._localAnchor2 - MyBase._bodyB.GetLocalCenter))
            Dim b As Vector2 = (Me._impulse * Me._J.linear2)
            Dim num As Single = ((Me._impulse * Me._J.angular2) - MathUtils.Cross(a, b))
            Return (inv_dt * num)
        End Function

        Friend Overrides Sub InitVelocityConstraints(ByRef [step] As TimeStep)
            Dim body As Body = Me._ground1
            Dim body2 As Body = Me._ground2
            Dim body3 As Body = MyBase._bodyA
            Dim body4 As Body = MyBase._bodyB
            Dim num As Single = 0!
            Me._J.SetZero
            If (Me._revolute1 IsNot Nothing) Then
                Me._J.angular1 = -1!
                num = (num + body3._invI)
            Else
                Dim form As XForm
                Dim form2 As XForm
                body3.GetXForm(form)
                body.GetXForm(form2)
                Dim b As Vector2 = MathUtils.Multiply(form2.R, Me._prismatic1._localXAxis1)
                Dim num2 As Single = MathUtils.Cross(MathUtils.Multiply(form.R, (Me._localAnchor1 - body3.GetLocalCenter)), b)
                Me._J.linear1 = -b
                Me._J.angular1 = -num2
                num = (num + (body3._invMass + ((body3._invI * num2) * num2)))
            End If
            If (Me._revolute2 IsNot Nothing) Then
                Me._J.angular2 = -Me._ratio
                num = (num + ((Me._ratio * Me._ratio) * body4._invI))
            Else
                Dim form3 As XForm
                Dim form4 As XForm
                body.GetXForm(form3)
                body4.GetXForm(form4)
                Dim vector3 As Vector2 = MathUtils.Multiply(form3.R, Me._prismatic2._localXAxis1)
                Dim num3 As Single = MathUtils.Cross(MathUtils.Multiply(form4.R, (Me._localAnchor2 - body4.GetLocalCenter)), vector3)
                Me._J.linear2 = (-Me._ratio * vector3)
                Me._J.angular2 = (-Me._ratio * num3)
                num = (num + ((Me._ratio * Me._ratio) * (body4._invMass + ((body4._invI * num3) * num3))))
            End If
            Debug.Assert((num > 0!))
            Me._mass = (1! / num)
            If [step].warmStarting Then
                body3._linearVelocity = (body3._linearVelocity + ((body3._invMass * Me._impulse) * Me._J.linear1))
                body3._angularVelocity = (body3._angularVelocity + ((body3._invI * Me._impulse) * Me._J.angular1))
                body4._linearVelocity = (body4._linearVelocity + ((body4._invMass * Me._impulse) * Me._J.linear2))
                body4._angularVelocity = (body4._angularVelocity + ((body4._invI * Me._impulse) * Me._J.angular2))
            Else
                Me._impulse = 0!
            End If
        End Sub

        Friend Overrides Function SolvePositionConstraints(ByVal baumgarte As Single) As Boolean
            Dim jointAngle As Single
            Dim jointTranslation As Single
            Dim num As Single = 0!
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            If (Me._revolute1 IsNot Nothing) Then
                jointAngle = Me._revolute1.GetJointAngle
            Else
                jointAngle = Me._prismatic1.GetJointTranslation
            End If
            If (Me._revolute2 IsNot Nothing) Then
                jointTranslation = Me._revolute2.GetJointAngle
            Else
                jointTranslation = Me._prismatic2.GetJointTranslation
            End If
            Dim num4 As Single = (Me._ant - (jointAngle + (Me._ratio * jointTranslation)))
            Dim num5 As Single = (Me._mass * -num4)
            body._sweep.c = (body._sweep.c + ((body._invMass * num5) * Me._J.linear1))
            body._sweep.a = (body._sweep.a + ((body._invI * num5) * Me._J.angular1))
            body2._sweep.c = (body2._sweep.c + ((body2._invMass * num5) * Me._J.linear2))
            body2._sweep.a = (body2._sweep.a + ((body2._invI * num5) * Me._J.angular2))
            body.SynchronizeTransform
            body2.SynchronizeTransform
            Return (num < Settings.b2_linearSlop)
        End Function

        Friend Overrides Sub SolveVelocityConstraints(ByRef [step] As TimeStep)
            Dim body As Body = MyBase._bodyA
            Dim body2 As Body = MyBase._bodyB
            Dim num As Single = Me._J.Compute(body._linearVelocity, body._angularVelocity, body2._linearVelocity, body2._angularVelocity)
            Dim num2 As Single = (Me._mass * -num)
            Me._impulse = (Me._impulse + num2)
            body._linearVelocity = (body._linearVelocity + ((body._invMass * num2) * Me._J.linear1))
            body._angularVelocity = (body._angularVelocity + ((body._invI * num2) * Me._J.angular1))
            body2._linearVelocity = (body2._linearVelocity + ((body2._invMass * num2) * Me._J.linear2))
            body2._angularVelocity = (body2._angularVelocity + ((body2._invI * num2) * Me._J.angular2))
        End Sub


        ' Fields
        Friend _ant As Single
        Friend _ground1 As Body
        Friend _ground2 As Body
        Friend _groundAnchor1 As Vector2
        Friend _groundAnchor2 As Vector2
        Friend _impulse As Single
        Friend _J As Jacobian
        Friend _localAnchor1 As Vector2
        Friend _localAnchor2 As Vector2
        Friend _mass As Single
        Friend _prismatic1 As PrismaticJoint
        Friend _prismatic2 As PrismaticJoint
        Friend _ratio As Single
        Friend _revolute1 As RevoluteJoint
        Friend _revolute2 As RevoluteJoint
    End Class
End Namespace

