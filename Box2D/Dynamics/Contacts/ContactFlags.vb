Imports System

Namespace Global.Box2D
    <Flags> _
    Public Enum ContactFlags
        ' Fields
        Filter = &H20
        Island = 4
        None = 0
        NonSolid = 1
        Slow = 2
        Toi = 8
        Touch = &H10
    End Enum
End Namespace

