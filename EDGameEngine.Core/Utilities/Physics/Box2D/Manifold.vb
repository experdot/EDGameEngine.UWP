Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure Manifold
        Public _points As FixedArray2(Of ManifoldPoint)
        Public _localPlaneNormal As Vector2
        Public _localPoint As Vector2
        Public _type As ManifoldType
        Public _pointCount As Integer
    End Structure
End Namespace

