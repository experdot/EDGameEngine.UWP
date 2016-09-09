Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure Filter
        Public categoryBits As UInt16
        Public maskBits As UInt16
        Public groupIndex As Short
    End Structure
End Namespace

