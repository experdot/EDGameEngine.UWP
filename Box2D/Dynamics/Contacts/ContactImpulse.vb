Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure ContactImpulse
        Public normalImpulses As FixedArray2(Of Single)
        Public tangentImpulses As FixedArray2(Of Single)
    End Structure
End Namespace

