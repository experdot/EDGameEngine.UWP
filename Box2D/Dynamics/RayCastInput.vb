Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure RayCastInput
        Public p1 As Vector2
        Public p2 As Vector2
        Public maxFraction As Single
    End Structure
End Namespace

