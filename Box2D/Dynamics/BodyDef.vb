Imports System
Imports System.Numerics

Namespace Global.Box2D
    Public Class BodyDef
        
        ' Methods
        Public Sub New()
            Me.massData.Centroid = Vector2.Zero
            Me.massData.Mass = 0!
            Me.massData.InertiaMoment = 0!
            Me.userData = Nothing
            Me.position = New Vector2(0!, 0!)
            Me.angle = 0!
            Me.linearVelocity = New Vector2(0!, 0!)
            Me.angularVelocity = 0!
            Me.linearDamping = 0!
            Me.angularDamping = 0!
            Me.allowSleep = True
            Me.isSleeping = False
            Me.fixedRotation = False
            Me.isBullet = False
        End Sub


        ' Fields
        Public allowSleep As Boolean
        Public angle As Single
        Public angularDamping As Single
        Public angularVelocity As Single
        Public fixedRotation As Boolean
        Public isBullet As Boolean
        Public isSleeping As Boolean
        Public linearDamping As Single
        Public linearVelocity As Vector2
        Public massData As MassData
        Public position As Vector2
        Public userData As Object
    End Class
End Namespace

