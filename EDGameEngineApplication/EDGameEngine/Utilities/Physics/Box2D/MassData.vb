Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Public Structure MassData
        Public mass As Single
        Public center As Vector2
        Public i As Single
    End Structure
End Namespace