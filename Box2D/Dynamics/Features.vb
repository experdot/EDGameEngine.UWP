Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure Features
        Public ReferenceEdge As Byte
        Public IncidentEdge As Byte
        Public IncidentVertex As Byte
        Public Flip As Byte
    End Structure
End Namespace

