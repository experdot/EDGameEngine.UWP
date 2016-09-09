Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure TOIInput
        Public sweepA As Sweep
        Public sweepB As Sweep
        Public tolerance As Single
    End Structure
End Namespace

