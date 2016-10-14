Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    Public Class DynamicTree

        ' Methods
        Public Sub New()
            Me._nodes = New DynamicTreeNode(Me._nodeCapacity - 1) {}
            Dim i As Integer
            For i = 0 To (Me._nodeCapacity - 1) - 1
                Me._nodes(i).ParentOrNext = (i + 1)
            Next i
            Me._nodes((Me._nodeCapacity - 1)).ParentOrNext = DynamicTree.NullNode
            Me._freeList = 0
            Me._path = 0
        End Sub

        Private Function AllocateNode() As Integer
            If (Me._freeList = DynamicTree.NullNode) Then
                Debug.Assert((Me._nodeCount = Me._nodeCapacity))
                Dim nodeArray As DynamicTreeNode() = Me._nodes
                Me._nodeCapacity = (Me._nodeCapacity * 2)
                Me._nodes = New DynamicTreeNode(Me._nodeCapacity - 1) {}
                Array.Copy(nodeArray, Me._nodes, Me._nodeCount)
                Dim i As Integer
                For i = Me._nodeCount To (Me._nodeCapacity - 1) - 1
                    Me._nodes(i).ParentOrNext = (i + 1)
                Next i
                Me._nodes((Me._nodeCapacity - 1)).ParentOrNext = DynamicTree.NullNode
                Me._freeList = Me._nodeCount
            End If
            Dim index As Integer = Me._freeList
            Me._freeList = Me._nodes(index).ParentOrNext
            Me._nodes(index).ParentOrNext = DynamicTree.NullNode
            Me._nodes(index).Child1 = DynamicTree.NullNode
            Me._nodes(index).Child2 = DynamicTree.NullNode
            Me._nodeCount += 1
            Return index
        End Function

        Public Function ComputeHeight() As Integer
            Return Me.ComputeHeight(Me._root)
        End Function

        Private Function ComputeHeight(ByVal nodeId As Integer) As Integer
            If (nodeId = DynamicTree.NullNode) Then
                Return 0
            End If
            Debug.Assert(((0 <= nodeId) AndAlso (nodeId < Me._nodeCapacity)))
            Dim node As DynamicTreeNode = Me._nodes(nodeId)
            Dim num As Integer = Me.ComputeHeight(node.Child1)
            Dim num2 As Integer = Me.ComputeHeight(node.Child2)
            Return (1 + Math.Max(num, num2))
        End Function

        Public Function CreateProxy(ByRef aabb As AABB, ByVal userData As Integer) As Integer
            Dim index As Integer = Me.AllocateNode
            Dim vector As New Vector2(Settings.b2_aabbExtension, Settings.b2_aabbExtension)
            Me._nodes(index).AABB.LowerBound = (aabb.LowerBound - vector)
            Me._nodes(index).AABB.UpperBound = (aabb.UpperBound + vector)
            Me._nodes(index).Userdata = userData
            Me.InsertLeaf(index)
            Return index
        End Function

        Public Sub DestroyProxy(ByVal proxyId As Integer)
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._nodeCapacity)))
            Debug.Assert(Me._nodes(proxyId).IsLeaf)
            Me.RemoveLeaf(proxyId)
            Me.FreeNode(proxyId)
        End Sub

        Private Sub FreeNode(ByVal nodeId As Integer)
            Debug.Assert(((0 <= nodeId) AndAlso (nodeId < Me._nodeCapacity)))
            Debug.Assert((0 < Me._nodeCount))
            Me._nodes(nodeId).ParentOrNext = Me._freeList
            Me._freeList = nodeId
            Me._nodeCount -= 1
        End Sub

        Public Function GetUserData(ByVal proxyId As Integer) As Integer
            If (proxyId < Me._nodeCapacity) Then
                Return Me._nodes(proxyId).Userdata
            End If
            Return 0
        End Function

        Private Sub InsertLeaf(ByVal leaf As Integer)
            If (Me._root = DynamicTree.NullNode) Then
                Me._root = leaf
                Me._nodes(Me._root).ParentOrNext = DynamicTree.NullNode
            Else
                Dim center As Vector2 = Me._nodes(leaf).AABB.Center
                Dim index As Integer = Me._root
                If Not Me._nodes(index).IsLeaf Then
                    Do
                        Dim num4 As Integer = Me._nodes(index).Child1
                        Dim num5 As Integer = Me._nodes(index).Child2
                        Dim vector2 As Vector2 = MathUtils.Abs((Me._nodes(num4).AABB.Center - center))
                        Dim vector3 As Vector2 = MathUtils.Abs((Me._nodes(num5).AABB.Center - center))
                        Dim num6 As Single = (vector2.X + vector2.Y)
                        Dim num7 As Single = (vector3.X + vector3.Y)
                        If (num6 < num7) Then
                            index = num4
                        Else
                            index = num5
                        End If
                    Loop While Not Me._nodes(index).IsLeaf
                End If
                Dim parentOrNext As Integer = Me._nodes(index).ParentOrNext
                Dim num3 As Integer = Me.AllocateNode
                Me._nodes(num3).ParentOrNext = parentOrNext
                Me._nodes(num3).Userdata = 0
                Me._nodes(num3).AABB.Combine(Me._nodes(leaf).AABB, Me._nodes(index).AABB)
                If (parentOrNext <> DynamicTree.NullNode) Then
                    If (Me._nodes(Me._nodes(index).ParentOrNext).Child1 = index) Then
                        Me._nodes(parentOrNext).Child1 = num3
                    Else
                        Me._nodes(parentOrNext).Child2 = num3
                    End If
                    Me._nodes(num3).Child1 = index
                    Me._nodes(num3).Child2 = leaf
                    Me._nodes(index).ParentOrNext = num3
                    Me._nodes(leaf).ParentOrNext = num3
                    Do
                        If Me._nodes(parentOrNext).AABB.Contains(Me._nodes(num3).AABB) Then
                            Exit Do
                        End If
                        Me._nodes(parentOrNext).AABB.Combine(Me._nodes(Me._nodes(parentOrNext).Child1).AABB, Me._nodes(Me._nodes(parentOrNext).Child2).AABB)
                        num3 = parentOrNext
                        parentOrNext = Me._nodes(parentOrNext).ParentOrNext
                    Loop While (parentOrNext <> DynamicTree.NullNode)
                Else
                    Me._nodes(num3).Child1 = index
                    Me._nodes(num3).Child2 = leaf
                    Me._nodes(index).ParentOrNext = num3
                    Me._nodes(leaf).ParentOrNext = num3
                    Me._root = num3
                End If
            End If
        End Sub

        Public Sub MoveProxy(ByVal proxyId As Integer, ByRef aabb As AABB)
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._nodeCapacity)))
            Debug.Assert(Me._nodes(proxyId).IsLeaf)
            If Not Me._nodes(proxyId).AABB.Contains(aabb) Then
                Me.RemoveLeaf(proxyId)
                Dim vector As New Vector2(Settings.b2_aabbExtension, Settings.b2_aabbExtension)
                Me._nodes(proxyId).AABB.LowerBound = (aabb.LowerBound - vector)
                Me._nodes(proxyId).AABB.UpperBound = (aabb.UpperBound + vector)
                Me.InsertLeaf(proxyId)
            End If
        End Sub

        Public Sub Query(ByVal callback As Action(Of Integer), ByRef aabb As AABB)
            Dim num As Integer = 0
            DynamicTree.stack(num.ValueIncrement) = Me._root
            Do While (num > 0)
                num -= 1
                Dim index As Integer = DynamicTree.stack(num)
                If (index <> DynamicTree.NullNode) Then
                    Dim node As DynamicTreeNode = Me._nodes(index)
                    If AABB.TestOverlap(node.AABB, aabb) Then
                        If node.IsLeaf Then
                            callback.Invoke(node.Userdata)
                        Else
                            Debug.Assert(((num + 1) < DynamicTree.k_stackSize))
                            DynamicTree.stack(num.ValueIncrement) = node.Child1
                            DynamicTree.stack(num.ValueIncrement) = node.Child2
                        End If
                    End If
                End If
            Loop
        End Sub

        Public Sub RayCast(ByVal callback As RayCastCallback, ByRef input As RayCastInput)
            Dim vector As Vector2 = input.p1
            Dim vector2 As Vector2 = input.p2
            Dim vec As Vector2 = (vector2 - vector)
            Debug.Assert((vec.LengthSquared > 0!))
            Extension.Normalize(vec)
            Dim v As Vector2 = MathUtils.Cross(CSng(1.0!), vec)
            Dim vector5 As Vector2 = MathUtils.Abs(v)
            Dim maxFraction As Single = input.maxFraction
            Dim b As New AABB
            Dim vector6 As Vector2 = (vector + (maxFraction * (vector2 - vector)))
            b.LowerBound = Vector2.Min(vector, vector6)
            b.UpperBound = Vector2.Max(vector, vector6)
            Dim num2 As Integer = 0
            DynamicTree.stack(num2.ValueIncrement) = Me._root
            Do While (num2 > 0)
                num2 -= 1
                Dim index As Integer = DynamicTree.stack(num2)
                If (index <> DynamicTree.NullNode) Then
                    Dim node As DynamicTreeNode = Me._nodes(index)
                    If AABB.TestOverlap(node.AABB, b) Then
                        Dim center As Vector2 = node.AABB.Center
                        Dim extents As Vector2 = node.AABB.Extents
                        Dim num4 As Single = (Math.Abs(Vector2.Dot(v, (vector - center))) - Vector2.Dot(vector5, extents))
                        If (num4 <= 0!) Then
                            If node.IsLeaf Then
                                Dim input2 As RayCastInput
                                Dim output As RayCastOutput
                                input2.p1 = input.p1
                                input2.p2 = input.p2
                                input2.maxFraction = maxFraction
                                callback.Invoke(output, input2, node.Userdata)
                                If output.hit Then
                                    If (output.fraction = 0!) Then
                                        Exit Do
                                    End If
                                    maxFraction = output.fraction
                                    Dim vector9 As Vector2 = (vector + (maxFraction * (vector2 - vector)))
                                    b.LowerBound = Vector2.Min(vector, vector9)
                                    b.UpperBound = Vector2.Max(vector, vector9)
                                End If
                            Else
                                Debug.Assert(((num2 + 1) < DynamicTree.k_stackSize))
                                DynamicTree.stack(num2.ValueIncrement) = node.Child1
                                DynamicTree.stack(num2.ValueIncrement) = node.Child2
                            End If
                        End If
                    End If
                End If
            Loop
        End Sub

        Public Sub Rebalance(ByVal iterations As Integer)
            If (Me._root <> DynamicTree.NullNode) Then
                Dim i As Integer
                For i = 0 To iterations - 1
                    Dim index As Integer = Me._root
                    Dim j As Integer = 0
                    Do While Not Me._nodes(index).IsLeaf
                        index = If(((((Me._path) >> j) And 1) = 0), Me._nodes(index).Child1, Me._nodes(index).Child1)
                        j = ((j + 1) And &H1F)
                    Loop
                    Me._path += 1
                    Me.RemoveLeaf(index)
                    Me.InsertLeaf(index)
                Next i
            End If
        End Sub

        Private Sub RemoveLeaf(ByVal leaf As Integer)
            If (leaf = Me._root) Then
                Me._root = DynamicTree.NullNode
            Else
                Dim num3 As Integer
                Dim parentOrNext As Integer = Me._nodes(leaf).ParentOrNext
                Dim index As Integer = Me._nodes(parentOrNext).ParentOrNext
                If (Me._nodes(parentOrNext).Child1 = leaf) Then
                    num3 = Me._nodes(parentOrNext).Child2
                Else
                    num3 = Me._nodes(parentOrNext).Child1
                End If
                If (index <> DynamicTree.NullNode) Then
                    If (Me._nodes(index).Child1 = parentOrNext) Then
                        Me._nodes(index).Child1 = num3
                    Else
                        Me._nodes(index).Child2 = num3
                    End If
                    Me._nodes(num3).ParentOrNext = index
                    Me.FreeNode(parentOrNext)
                    Do While (index <> DynamicTree.NullNode)
                        Dim aabb As AABB = Me._nodes(index).AABB
                        Me._nodes(index).AABB.Combine(Me._nodes(Me._nodes(index).Child1).AABB, Me._nodes(Me._nodes(index).Child2).AABB)
                        If aabb.Contains(Me._nodes(index).AABB) Then
                            Exit Do
                        End If
                        index = Me._nodes(index).ParentOrNext
                    Loop
                Else
                    Me._root = num3
                    Me._nodes(num3).ParentOrNext = DynamicTree.NullNode
                    Me.FreeNode(parentOrNext)
                End If
            End If
        End Sub


        ' Fields
        Private _freeList As Integer
        Private _nodeCapacity As Integer = &H10
        Private _nodeCount As Integer = 0
        Private _nodes As DynamicTreeNode()
        Private _path As Integer
        Private _root As Integer = DynamicTree.NullNode
        Private Shared k_stackSize As Integer = &H80
        Friend Shared NullNode As Integer = -1
        Private Shared stack As Integer() = New Integer(DynamicTree.k_stackSize - 1) {}

        ' Nested Types
        Public Delegate Sub RayCastCallback(<Out> ByRef output As RayCastOutput, ByRef input As RayCastInput, ByVal userData As Integer)
    End Class
End Namespace

