Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure DistanceOutput
        Public pointA As Vector2
        Public pointB As Vector2
        Public distance As Single
        Public iterations As Integer
    End Structure
End Namespace

