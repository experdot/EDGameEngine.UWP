Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure RayCastOutput
        Public normal As Vector2
        Public fraction As Single
        Public hit As Boolean
    End Structure
End Namespace

