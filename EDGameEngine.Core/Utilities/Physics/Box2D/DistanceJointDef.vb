Imports System
Imports System.Numerics

Namespace Global.Box2D
    Public Class DistanceJointDef
        Inherits JointDef
        ' Methods
        Public Sub New()
            MyBase.type = JointType.Distance
            Me.localAnchor1 = New Vector2(0!, 0!)
            Me.localAnchor2 = New Vector2(0!, 0!)
            Me.length = 1!
            Me.frequencyHz = 0!
            Me.dampingRatio = 0!
        End Sub

        Public Sub Initialize(ByVal b1 As Body, ByVal b2 As Body, ByVal anchor1 As Vector2, ByVal anchor2 As Vector2)
            MyBase.body1 = b1
            MyBase.body2 = b2
            Me.localAnchor1 = MyBase.body1.GetLocalPoint(anchor1)
            Me.localAnchor2 = MyBase.body2.GetLocalPoint(anchor2)
            Me.length = (anchor2 - anchor1).Length
        End Sub


        ' Fields
        Public dampingRatio As Single
        Public frequencyHz As Single
        Public length As Single
        Public localAnchor1 As Vector2
        Public localAnchor2 As Vector2
    End Class
End Namespace

