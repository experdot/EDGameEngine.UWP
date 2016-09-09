Imports System
Imports System.Numerics

Namespace Global.Box2D
    Public Class MouseJointDef
        Inherits JointDef
        ' Methods
        Public Sub New()
            MyBase.type = JointType.Mouse
            Me.target = New Vector2(0!, 0!)
            Me.maxForce = 0!
            Me.frequencyHz = 5!
            Me.dampingRatio = 0.7!
        End Sub


        ' Fields
        Public dampingRatio As Single
        Public frequencyHz As Single
        Public maxForce As Single
        Public target As Vector2
    End Class
End Namespace

