Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    Public MustInherit Class Joint
        
        ' Methods
        Protected Sub New(ByVal def As JointDef)
            Me._type = def.type
            Me._bodyA = def.body1
            Me._bodyB = def.body2
            Me._collideConnected = def.collideConnected
            Me._userData = def.userData
            Me._edgeA = New JointEdge
            Me._edgeB = New JointEdge
        End Sub

        Friend Sub ComputeXForm(<Out> ByRef xf As XForm, ByVal center As Vector2, ByVal localCenter As Vector2, ByVal angle As Single)
            xf = New XForm
            xf.R.SetValue(angle)
            xf.Position = (center - MathUtils.Multiply(xf.R, localCenter))
        End Sub

        Friend Shared Function Create(ByVal def As JointDef) As Joint
            Select Case def.type
                Case JointType.Revolute
                    Return New RevoluteJoint(DirectCast(def, RevoluteJointDef))
                Case JointType.Prismatic
                    Return New PrismaticJoint(DirectCast(def, PrismaticJointDef))
                Case JointType.Distance
                    Return New DistanceJoint(DirectCast(def, DistanceJointDef))
                Case JointType.Pulley
                    Return New PulleyJoint(DirectCast(def, PulleyJointDef))
                Case JointType.Mouse
                    Return New MouseJoint(DirectCast(def, MouseJointDef))
                Case JointType.Gear
                    Return New GearJoint(DirectCast(def, GearJointDef))
                Case JointType.Line
                    Return New LineJoint(DirectCast(def, LineJointDef))
            End Select
            Debug.Assert(False)
            Return Nothing
        End Function

        Public MustOverride Function GetAnchor1() As Vector2

        Public MustOverride Function GetAnchor2() As Vector2

        Public Function GetBody1() As Body
            Return Me._bodyA
        End Function

        Public Function GetBody2() As Body
            Return Me._bodyB
        End Function

        Public Function GetNext() As Joint
            Return Me._next
        End Function

        Public MustOverride Function GetReactionForce(ByVal inv_dt As Single) As Vector2

        Public MustOverride Function GetReactionTorque(ByVal inv_dt As Single) As Single

        Public Function GetUserData() As Object
            Return Me._userData
        End Function

        Friend MustOverride Sub InitVelocityConstraints(ByRef [step] As TimeStep)

        Public Sub SetUserData(ByVal data As Object)
            Me._userData = data
        End Sub

        Friend MustOverride Function SolvePositionConstraints(ByVal baumgarte As Single) As Boolean

        Friend MustOverride Sub SolveVelocityConstraints(ByRef [step] As TimeStep)


        ' Properties
        Public ReadOnly Property JointType As JointType
            Get
                Return Me._type
            End Get
        End Property


        ' Fields
        Friend _bodyA As Body
        Friend _bodyB As Body
        Friend _collideConnected As Boolean
        Friend _edgeA As JointEdge
        Friend _edgeB As JointEdge
        Friend _invI1 As Single
        Friend _invI2 As Single
        Friend _invMass1 As Single
        Friend _invMass2 As Single
        Friend _islandFlag As Boolean
        Friend _localCenter1 As Vector2
        Friend _localCenter2 As Vector2
        Friend _next As Joint
        Friend _prev As Joint
        Friend _type As JointType
        Friend _userData As Object
    End Class
End Namespace

