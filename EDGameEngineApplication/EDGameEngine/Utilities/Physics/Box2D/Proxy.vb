Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure Proxy
        Public aabb As AABB
        Public userData As Object
        Public treeProxyId As Integer
        Public [next] As Integer
    End Structure
End Namespace

