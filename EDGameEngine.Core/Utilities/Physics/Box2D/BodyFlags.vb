Imports System

Namespace Global.Box2D
    <Flags> _
    Public Enum BodyFlags
        ' Fields
        AllowSleep = &H20
        Bullet = &H40
        FixedRotation = &H80
        Frozen = 4
        Island = 8
        None = 0
        Sleep = &H10
    End Enum
End Namespace

