Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure ContactConstraintPoint
        Public localPoint As Vector2
        Public rA As Vector2
        Public rB As Vector2
        Public normalImpulse As Single
        Public tangentImpulse As Single
        Public normalMass As Single
        Public tangentMass As Single
        Public equalizedMass As Single
        Public velocityBias As Single
    End Structure
End Namespace

