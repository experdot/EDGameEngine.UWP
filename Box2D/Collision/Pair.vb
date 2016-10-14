Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure Pair
        Implements IComparable(Of Pair)
        Public proxyIdA As Integer
        Public proxyIdB As Integer
        Public [Next] As Integer
        Public Function CompareTo(ByVal other As Pair) As Integer Implements IComparable(Of Pair).CompareTo
            If (Me.proxyIdA < other.proxyIdA) Then
                Return -1
            End If
            If (Me.proxyIdA = other.proxyIdA) Then
                If (Me.proxyIdB < other.proxyIdB) Then
                    Return -1
                End If
                If (Me.proxyIdB = other.proxyIdB) Then
                    Return 0
                End If
            End If
            Return 1
        End Function
    End Structure
End Namespace

