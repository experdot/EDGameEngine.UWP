Imports System

Namespace Global.Box2D
    Public Class JointEdge
        
        ' Fields
        Public Joint As Joint
        Public [Next] As JointEdge
        Public Other As Body
        Public Prev As JointEdge
    End Class
End Namespace

