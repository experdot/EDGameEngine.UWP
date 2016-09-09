Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure ContactID
        ' Fields
        <FieldOffset(0)> _
        Public Features As Features
        <FieldOffset(0)> _
        Public Key As UInt32
    End Structure
End Namespace

