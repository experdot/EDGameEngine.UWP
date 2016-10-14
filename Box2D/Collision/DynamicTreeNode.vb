Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    ''' <summary>
    ''' 动态树节点
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure DynamicTreeNode
        ''' <summary>
        ''' AABB盒
        ''' </summary>
        Friend AABB As AABB
        ''' <summary>
        ''' 用户数据
        ''' </summary>
        Friend Userdata As Integer
        ''' <summary>
        ''' 父节点或邻节点
        ''' </summary>
        Friend ParentOrNext As Integer
        ''' <summary>
        ''' 子节点1
        ''' </summary>
        Friend Child1 As Integer
        ''' <summary>
        ''' 子节点2
        ''' </summary>
        Friend Child2 As Integer
        ''' <summary>
        ''' 是否叶子节点
        ''' </summary>
        ''' <returns></returns>
        Friend ReadOnly Property IsLeaf As Boolean
            Get
                Return (Me.Child1 = DynamicTree.NullNode)
            End Get
        End Property
    End Structure
End Namespace

