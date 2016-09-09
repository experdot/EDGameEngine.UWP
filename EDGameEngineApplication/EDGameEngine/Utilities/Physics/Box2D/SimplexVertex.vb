Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure SimplexVertex
        Public wA As Vector2
        Public wB As Vector2
        Public w As Vector2
        Public a As Single
        Public indexA As Integer
        Public indexB As Integer
    End Structure
End Namespace

