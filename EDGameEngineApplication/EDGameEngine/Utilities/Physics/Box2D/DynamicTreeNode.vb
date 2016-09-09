Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure DynamicTreeNode
        Friend aabb As AABB
        Friend userData As Integer
        Friend parentOrNext As Integer
        Friend child1 As Integer
        Friend child2 As Integer
        Friend Function IsLeaf() As Boolean
            Return (Me.child1 = DynamicTree.NullNode)
        End Function
    End Structure
End Namespace

