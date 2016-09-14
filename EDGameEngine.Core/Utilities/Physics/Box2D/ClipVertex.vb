Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Public Structure ClipVertex
        Public v As Vector2
        Public id As ContactID
    End Structure
End Namespace