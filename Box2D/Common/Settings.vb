Imports System

Namespace Global.Box2D
    Public Class Settings
        
        ' Methods
        Public Shared Function b2MixFriction(ByVal friction1 As Single, ByVal friction2 As Single) As Single
            Return CType(Math.Sqrt(CType((friction1 * friction2), Double)), Single)
        End Function

        Public Shared Function b2MixRestitution(ByVal restitution1 As Single, ByVal restitution2 As Single) As Single
            Return If((restitution1 > restitution2), restitution1, restitution2)
        End Function


        ' Fields
        Public Shared b2_aabbExtension As Single = 0.1!
        Public Shared b2_angularSleepTolerance As Single = (0.01111111! * Settings.b2_pi)
        Public Shared b2_angularSlop As Single = (0.01111111! * Settings.b2_pi)
        Public Shared b2_contactBaumgarte As Single = 0.2!
        Public Shared b2_FLT_EPSILON As Single = 0.0000001192093!
        Public Shared b2_FLT_MAX As Single = Single.MaxValue
        Public Shared b2_linearSleepTolerance As Single = 0.01!
        Public Shared b2_linearSlop As Single = 0.005!
        Public Shared b2_maxAngularCorrection As Single = (0.04444445! * Settings.b2_pi)
        Public Shared b2_maxLinearCorrection As Single = 0.2!
        Public Shared b2_maxManifoldPoints As Integer = 2
        Public Shared b2_maxPolygonVertices As Integer = 8
        Public Shared b2_maxRotation As Single = (0.5! * Settings.b2_pi)
        Public Shared b2_maxRotationSquared As Single = (Settings.b2_maxRotation * Settings.b2_maxRotation)
        Public Shared b2_maxTOIContactsPerIsland As Integer = &H20
        Public Shared b2_maxTOIJointsPerIsland As Integer = &H20
        Public Shared b2_maxTranslation As Single = 2.0!
        Public Shared b2_maxTranslationSquared As Single = (Settings.b2_maxTranslation * Settings.b2_maxTranslation)
        Public Shared b2_pi As Single = 3.141593!
        Public Shared b2_polygonRadius As Single = (2.0! * Settings.b2_linearSlop)
        Public Shared b2_timeToSleep As Single = 0.5!
        Public Shared b2_velocityThreshold As Single = 1.0!
    End Class
End Namespace

