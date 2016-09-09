Imports System

Namespace Global.Box2D
    Public Class GearJointDef
        Inherits JointDef
        ' Methods
        Public Sub New()
            MyBase.type = JointType.Gear
            Me.joint1 = Nothing
            Me.joint2 = Nothing
            Me.ratio = 1!
        End Sub


        ' Fields
        Public joint1 As Joint
        Public joint2 As Joint
        Public ratio As Single
    End Class
End Namespace

