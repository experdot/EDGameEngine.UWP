Imports System
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    Public Class BroadPhase
        
        ' Methods
        Public Sub New()
            Me._queryCallback = New Action(Of Integer)(AddressOf Me.QueryCallback)
            Me._proxyCapacity = &H10
            Me._proxyCount = 0
            Me._proxyPool = New Proxy(Me._proxyCapacity  - 1) {}
            Me._freeProxy = 0
            Dim i As Integer
            For i = 0 To (Me._proxyCapacity - 1) - 1
                Me._proxyPool(i).next = (i + 1)
            Next i
            Me._proxyPool((Me._proxyCapacity - 1)).next = BroadPhase.NullProxy
            Me._pairCapacity = &H10
            Me._pairCount = 0
            Me._pairBuffer = New Pair(Me._pairCapacity  - 1) {}
            Me._moveCapacity = &H10
            Me._moveCount = 0
            Me._moveBuffer = New Integer(Me._moveCapacity  - 1) {}
        End Sub

        Friend Function AllocateProxy() As Integer
            If (Me._freeProxy = BroadPhase.NullProxy) Then
                Debug.Assert((Me._proxyCount = Me._proxyCapacity))
                Dim proxyArray As Proxy() = Me._proxyPool
                Me._proxyCapacity = (Me._proxyCapacity * 2)
                Me._proxyPool = New Proxy(Me._proxyCapacity  - 1) {}
                Array.Copy(proxyArray, Me._proxyPool, Me._proxyCount)
                Me._freeProxy = Me._proxyCount
                Dim i As Integer
                For i = Me._proxyCount To (Me._proxyCapacity - 1) - 1
                    Me._proxyPool(i).next = (i + 1)
                Next i
                Me._proxyPool((Me._proxyCapacity - 1)).next = BroadPhase.NullProxy
            End If
            Dim index As Integer = Me._freeProxy
            Me._freeProxy = Me._proxyPool(index).next
            Me._proxyPool(index).next = BroadPhase.NullProxy
            Me._proxyCount += 1
            Return index
        End Function

        Friend Sub BufferMove(ByVal proxyId As Integer)
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._proxyCapacity)))
            If (Me._moveCount = Me._moveCapacity) Then
                Dim numArray As Integer() = Me._moveBuffer
                Me._moveCapacity = (Me._moveCapacity * 2)
                Me._moveBuffer = New Integer(Me._moveCapacity  - 1) {}
                Array.Copy(numArray, Me._moveBuffer, Me._moveCount)
            End If
            Me._moveBuffer(Me._moveCount) = proxyId
            Me._moveCount += 1
        End Sub

        Public Function ComputeHeight() As Integer
            Return Me._tree.ComputeHeight
        End Function

        Public Function CreateProxy(ByRef aabb As AABB, ByVal userData As Object) As Integer
            Dim index As Integer = Me.AllocateProxy
            Dim proxy As Proxy = Me._proxyPool(index)
            proxy.aabb = aabb
            proxy.treeProxyId = Me._tree.CreateProxy(aabb, index)
            proxy.userData = userData
            Me._proxyPool(index) = proxy
            Me.BufferMove(index)
            Return index
        End Function

        Public Sub DestroyProxy(ByVal proxyId As Integer)
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._proxyCapacity)))
            Me.UnBufferMove(proxyId)
            Me._tree.DestroyProxy(Me._proxyPool(proxyId).treeProxyId)
            Me.FreeProxy(proxyId)
        End Sub

        Friend Sub FreeProxy(ByVal proxyId As Integer)
            Debug.Assert((0 < Me._proxyCount))
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._proxyCapacity)))
            Me._proxyPool(proxyId).next = Me._freeProxy
            Me._freeProxy = proxyId
            Me._proxyCount -= 1
        End Sub

        Public Sub GetAABB(ByVal proxyId As Integer, <Out> ByRef aabb As AABB)
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._proxyCapacity)))
            aabb = Me._proxyPool(proxyId).aabb
        End Sub

        Public Function GetUserData(ByVal proxyId As Integer) As Object
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._proxyCapacity)))
            Return Me._proxyPool(proxyId).userData
        End Function

        Public Sub MoveProxy(ByVal proxyId As Integer, ByRef aabb As AABB)
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._proxyCapacity)))
            Dim proxy As Proxy = Me._proxyPool(proxyId)
            proxy.aabb = aabb
            Me._tree.MoveProxy(proxy.treeProxyId, aabb)
            Me.BufferMove(proxyId)
            Me._proxyPool(proxyId) = proxy
        End Sub

        Public Sub Query(Of T)(ByVal callback As Func(Of T, Boolean), ByRef aabb As AABB)
            Me._tree.Query(Function(userData)
                               Debug.Assert(((0 <= userData) AndAlso (userData < Me._proxyCapacity)))
                               callback.Invoke(DirectCast(Me._proxyPool(userData).userData, T))
                               Return 0
                           End Function, aabb)
        End Sub

        Friend Sub QueryCallback(ByVal proxyId As Integer)
            Debug.Assert(((0 <= proxyId) AndAlso (proxyId < Me._proxyCapacity)))
            If ((proxyId <> Me._queryProxyId) AndAlso AABB.TestOverlap(Me._proxyPool(proxyId).aabb, Me._proxyPool(Me._queryProxyId).aabb)) Then
                If (Me._pairCount = Me._pairCapacity) Then
                    Dim pairArray As Pair() = Me._pairBuffer
                    Me._pairCapacity = (Me._pairCapacity * 2)
                    Me._pairBuffer = New Pair(Me._pairCapacity  - 1) {}
                    Array.Copy(pairArray, Me._pairBuffer, Me._pairCount)
                End If
                Me._pairBuffer(Me._pairCount).proxyIdA = Math.Min(proxyId, Me._queryProxyId)
                Me._pairBuffer(Me._pairCount).proxyIdB = Math.Max(proxyId, Me._queryProxyId)
                Me._pairCount += 1
            End If
        End Sub

        Friend Sub UnBufferMove(ByVal proxyId As Integer)
            Dim i As Integer
            For i = 0 To Me._moveCount - 1
                If (Me._moveBuffer(i) = proxyId) Then
                    Me._moveBuffer(i) = BroadPhase.NullProxy
                    Exit For
                End If
            Next i
        End Sub

        Public Sub UpdatePairs(Of T)(ByVal callback As Action(Of T, T))
            Me._pairCount = 0
            Dim i As Integer
            For i = 0 To Me._moveCount - 1
                Me._queryProxyId = Me._moveBuffer(i)
                If (Me._queryProxyId <> BroadPhase.NullProxy) Then
                    Me._tree.Query(Me._queryCallback, Me._proxyPool(Me._queryProxyId).aabb)
                End If
            Next i
            Me._moveCount = 0
            Array.Sort(Of Pair)(Me._pairBuffer, 0, Me._pairCount)
            Dim index As Integer = 0
            Do While (index < Me._pairCount)
                Dim pair As Pair = Me._pairBuffer(index)
                Dim proxy As Proxy = Me._proxyPool(pair.proxyIdA)
                Dim proxy2 As Proxy = Me._proxyPool(pair.proxyIdB)
                callback.Invoke(DirectCast(proxy.userData, T), DirectCast(proxy2.userData, T))
                index += 1
                Do While (index < Me._pairCount)
                    Dim pair2 As Pair = Me._pairBuffer(index)
                    If ((pair2.proxyIdA <> pair.proxyIdA) OrElse (pair2.proxyIdB <> pair.proxyIdB)) Then
                        Exit Do
                    End If
                    index += 1
                Loop
            Loop
        End Sub


        ' Properties
        Public ReadOnly Property ProxyCount As Integer
            Get
                Return Me._proxyCount
            End Get
        End Property


        ' Fields
        Friend _freeProxy As Integer
        Friend _moveBuffer As Integer()
        Friend _moveCapacity As Integer
        Friend _moveCount As Integer
        Friend _pairBuffer As Pair()
        Friend _pairCapacity As Integer
        Friend _pairCount As Integer
        Friend _proxyCapacity As Integer
        Friend _proxyCount As Integer
        Friend _proxyPool As Proxy()
        Private _queryCallback As Action(Of Integer)
        Friend _queryProxyId As Integer
        Friend _tree As DynamicTree = New DynamicTree
        Friend Shared NullProxy As Integer = -1
    End Class
End Namespace

