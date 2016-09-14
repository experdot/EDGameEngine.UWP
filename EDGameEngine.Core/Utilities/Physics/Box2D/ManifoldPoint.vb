Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure ManifoldPoint
        Public LocalPoint As Vector2
        Public NormalImpulse As Single
        Public TangentImpulse As Single
        Public Id As ContactID
    End Structure
End Namespace

