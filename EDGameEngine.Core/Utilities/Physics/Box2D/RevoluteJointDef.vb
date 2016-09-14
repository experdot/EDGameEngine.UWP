Imports System
Imports System.Numerics

Namespace Global.Box2D
    Public Class RevoluteJointDef
        Inherits JointDef
        ' Methods
        Public Sub New()
            MyBase.type = JointType.Revolute
            Me.localAnchor1 = New Vector2(0!, 0!)
            Me.localAnchor2 = New Vector2(0!, 0!)
            Me.referenceAngle = 0!
            Me.lowerAngle = 0!
            Me.upperAngle = 0!
            Me.maxMotorTorque = 0!
            Me.motorSpeed = 0!
            Me.enableLimit = False
            Me.enableMotor = False
        End Sub

        Public Sub Initialize(ByVal b1 As Body, ByVal b2 As Body, ByVal anchor As Vector2)
            MyBase.body1 = b1
            MyBase.body2 = b2
            Me.localAnchor1 = MyBase.body1.GetLocalPoint(anchor)
            Me.localAnchor2 = MyBase.body2.GetLocalPoint(anchor)
            Me.referenceAngle = (MyBase.body2.Angle - MyBase.body1.Angle)
        End Sub


        ' Fields
        Public enableLimit As Boolean
        Public enableMotor As Boolean
        Public localAnchor1 As Vector2
        Public localAnchor2 As Vector2
        Public lowerAngle As Single
        Public maxMotorTorque As Single
        Public motorSpeed As Single
        Public referenceAngle As Single
        Public upperAngle As Single
    End Class
End Namespace

