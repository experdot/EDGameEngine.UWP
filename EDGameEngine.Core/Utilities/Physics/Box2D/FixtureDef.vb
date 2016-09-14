Imports System

Namespace Global.Box2D
    Public Class FixtureDef
        
        ' Methods
        Public Sub New()
            Me.filter.categoryBits = 1
            Me.filter.maskBits = &HFFFF
            Me.filter.groupIndex = 0
            Me.isSensor = False
        End Sub


        ' Fields
        Public density As Single = 0!
        Public filter As Filter
        Public friction As Single = 0.2!
        Public isSensor As Boolean
        Public restitution As Single = 0!
        Public shape As Shape = Nothing
        Public userData As Object = Nothing
    End Class
End Namespace

