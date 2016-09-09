Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure TimeStep
        Public dt As Single
        Public inv_dt As Single
        Public dtRatio As Single
        Public velocityIterations As Integer
        Public positionIterations As Integer
        Public warmStarting As Boolean
    End Structure
End Namespace

