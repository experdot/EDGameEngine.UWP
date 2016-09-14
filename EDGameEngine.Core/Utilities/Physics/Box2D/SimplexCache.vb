Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SimplexCache
        Public metric As Single
        Public count As UInt16
        Public indexA As FixedArray3(Of Byte)
        Public indexB As FixedArray3(Of Byte)
    End Structure
End Namespace

