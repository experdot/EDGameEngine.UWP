Imports System

Namespace Global.Box2D
    Public Class ContactEdge
        
        ' Fields
        Public Contact As Contact
        Public [Next] As ContactEdge
        Public Other As Body
        Public Prev As ContactEdge
    End Class
End Namespace

