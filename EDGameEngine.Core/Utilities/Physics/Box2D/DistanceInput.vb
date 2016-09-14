Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure DistanceInput
        Public transformA As XForm
        Public transformB As XForm
        Public useRadii As Boolean
    End Structure
End Namespace

