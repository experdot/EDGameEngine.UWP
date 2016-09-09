Imports System
Imports System.Diagnostics
Imports System.Numerics

Namespace Global.Box2D
    Public Class PulleyJointDef
        Inherits JointDef
        ' Methods
        Public Sub New()
            MyBase.type = JointType.Pulley
            Me.groundAnchor1 = New Vector2(-1!, 1!)
            Me.groundAnchor2 = New Vector2(1!, 1!)
            Me.localAnchor1 = New Vector2(-1!, 0!)
            Me.localAnchor2 = New Vector2(1!, 0!)
            Me.length1 = 0!
            Me.maxLength1 = 0!
            Me.length2 = 0!
            Me.maxLength2 = 0!
            Me.ratio = 1!
            MyBase.collideConnected = True
        End Sub

        Public Sub Initialize(ByVal b1 As Body, ByVal b2 As Body, ByVal ga1 As Vector2, ByVal ga2 As Vector2, ByVal anchor1 As Vector2, ByVal anchor2 As Vector2, ByVal r As Single)
            MyBase.body1 = b1
            MyBase.body2 = b2
            Me.groundAnchor1 = ga1
            Me.groundAnchor2 = ga2
            Me.localAnchor1 = MyBase.body1.GetLocalPoint(anchor1)
            Me.localAnchor2 = MyBase.body2.GetLocalPoint(anchor2)
            Me.length1 = (anchor1 - ga1).Length
            Me.length2 = (anchor2 - ga2).Length
            Me.ratio = r
            Debug.Assert((Me.ratio > Settings.b2_FLT_EPSILON))
            Dim num As Single = (Me.length1 + (Me.ratio * Me.length2))
            Me.maxLength1 = (num - (Me.ratio * PulleyJointDef.b2_minPulleyLength))
            Me.maxLength2 = ((num - PulleyJointDef.b2_minPulleyLength) / Me.ratio)
        End Sub


        ' Fields
        Friend Shared b2_minPulleyLength As Single = 2!
        Public groundAnchor1 As Vector2
        Public groundAnchor2 As Vector2
        Public length1 As Single
        Public length2 As Single
        Public localAnchor1 As Vector2
        Public localAnchor2 As Vector2
        Public maxLength1 As Single
        Public maxLength2 As Single
        Public ratio As Single
    End Class
End Namespace

