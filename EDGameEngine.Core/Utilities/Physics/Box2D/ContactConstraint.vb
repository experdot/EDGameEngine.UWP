Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure ContactConstraint
        Public points As FixedArray2(Of ContactConstraintPoint)
        Public localPlaneNormal As Vector2
        Public localPoint As Vector2
        Public normal As Vector2
        Public normalMass As Mat22
        Public K As Mat22
        Public bodyA As Body
        Public bodyB As Body
        Public type As ManifoldType
        Public radius As Single
        Public friction As Single
        Public restitution As Single
        Public pointCount As Integer
        Public manifold As Manifold
    End Structure
End Namespace

