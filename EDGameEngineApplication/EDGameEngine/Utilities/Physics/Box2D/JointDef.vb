Imports System

Namespace Global.Box2D
    Public Class JointDef
        
        ' Fields
        Public body1 As Body
        Public body2 As Body
        Public collideConnected As Boolean
        Friend type As JointType
        Public userData As Object
    End Class
End Namespace

