Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure Simplex
        Friend _v As FixedArray3(Of SimplexVertex)
        Friend _count As Integer
        Friend Sub ReadCache(ByRef cache As SimplexCache, ByVal shapeA As Shape, ByRef transformA As XForm, ByVal shapeB As Shape, ByRef transformB As XForm)
            Debug.Assert(((0 <= cache.count) AndAlso (cache.count <= 3)))
            Me._count = cache.count
            Dim i As Integer
            For i = 0 To Me._count - 1
                Dim vertex As SimplexVertex = Me._v.Item(i)
                vertex.indexA = cache.indexA.Item(i)
                vertex.indexB = cache.indexB.Item(i)
                Dim v As Vector2 = shapeA.GetVertex(vertex.indexA)
                Dim vector2 As Vector2 = shapeB.GetVertex(vertex.indexB)
                vertex.wA = MathUtils.Multiply(transformA, v)
                vertex.wB = MathUtils.Multiply(transformB, vector2)
                vertex.w = (vertex.wB - vertex.wA)
                vertex.a = 0!
                Me._v.Item(i) = vertex
            Next i
            If (Me._count > 1) Then
                Dim metric As Single = cache.metric
                Dim num3 As Single = Me.GetMetric
                If (((num3 < (0.5! * metric)) OrElse ((2.0! * metric) < num3)) OrElse (num3 < Settings.b2_FLT_EPSILON)) Then
                    Me._count = 0
                End If
            End If
            If (Me._count = 0) Then
                Dim vertex2 As SimplexVertex = Me._v.Item(0)
                vertex2.indexA = 0
                vertex2.indexB = 0
                Dim vector3 As Vector2 = shapeA.GetVertex(0)
                Dim vector4 As Vector2 = shapeB.GetVertex(0)
                vertex2.wA = MathUtils.Multiply(transformA, vector3)
                vertex2.wB = MathUtils.Multiply(transformB, vector4)
                vertex2.w = (vertex2.wB - vertex2.wA)
                Me._v.Item(0) = vertex2
                Me._count = 1
            End If
        End Sub

        Friend Sub WriteCache(ByRef cache As SimplexCache)
            cache.metric = Me.GetMetric
            cache.count = CType(Me._count, UInt16)
            Dim i As Integer
            For i = 0 To Me._count - 1
                cache.indexA.Item(i) = CType(Me._v.Item(i).indexA, Byte)
                cache.indexB.Item(i) = CType(Me._v.Item(i).indexB, Byte)
            Next i
        End Sub

        Friend Function GetSearchDirection() As Vector2
            Select Case Me._count
                Case 1
                    Return -Me._v.Item(0).w
                Case 2
                    Dim a As Vector2 = (Me._v.Item(1).w - Me._v.Item(0).w)
                    If (MathUtils.Cross(a, -Me._v.Item(0).w) > 0!) Then
                        Return MathUtils.Cross(CSng(1.0!), a)
                    End If
                    Return MathUtils.Cross(a, CSng(1.0!))
            End Select
            Debug.Assert(False)
            Return Vector2.Zero
        End Function

        Friend Function GetClosestPoint() As Vector2
            Select Case Me._count
                Case 0
                    Debug.Assert(False)
                    Return Vector2.Zero
                Case 1
                    Return Me._v.Item(0).w
                Case 2
                    Return ((Me._v.Item(0).a * Me._v.Item(0).w) + (Me._v.Item(1).a * Me._v.Item(1).w))
                Case 3
                    Return Vector2.Zero
            End Select
            Debug.Assert(False)
            Return Vector2.Zero
        End Function

        Friend Sub GetWitnessPoints(<Out> ByRef pA As Vector2, <Out> ByRef pB As Vector2)
            Select Case Me._count
                Case 0
                    pA = Vector2.Zero
                    pB = Vector2.Zero
                    Debug.Assert(False)
                    Exit Select
                Case 1
                    pA = Me._v.Item(0).wA
                    pB = Me._v.Item(0).wB
                    Exit Select
                Case 2
                    pA = ((Me._v.Item(0).a * Me._v.Item(0).wA) + (Me._v.Item(1).a * Me._v.Item(1).wA))
                    pB = ((Me._v.Item(0).a * Me._v.Item(0).wB) + (Me._v.Item(1).a * Me._v.Item(1).wB))
                    Exit Select
                Case 3
                    pA = (((Me._v.Item(0).a * Me._v.Item(0).wA) + (Me._v.Item(1).a * Me._v.Item(1).wA)) + (Me._v.Item(2).a * Me._v.Item(2).wA))
                    pB = pA
                    Exit Select
                Case Else
                    Throw New Exception
            End Select
        End Sub

        Friend Function GetMetric() As Single
            Select Case Me._count
                Case 0
                    Debug.Assert(False)
                    Return 0!
                Case 1
                    Return 0!
                Case 2
                    Dim vector As Vector2 = (Me._v.Item(0).w - Me._v.Item(1).w)
                    Return vector.Length
                Case 3
                    Return MathUtils.Cross((Me._v.Item(1).w - Me._v.Item(0).w), (Me._v.Item(2).w - Me._v.Item(0).w))
            End Select
            Debug.Assert(False)
            Return 0!
        End Function

        Friend Sub Solve2()
            Dim w As Vector2 = Me._v.Item(0).w
            Dim vector2 As Vector2 = Me._v.Item(1).w
            Dim vector3 As Vector2 = (vector2 - w)
            Dim num As Single = -Vector2.Dot(w, vector3)
            If (num <= 0!) Then
                Dim vertex3 As SimplexVertex = Me._v.Item(0)
                vertex3.a = 1.0!
                Me._v.Item(0) = vertex3
                Me._count = 1
            Else
                Dim num2 As Single = Vector2.Dot(vector2, vector3)
                If (num2 <= 0!) Then
                    Dim vertex4 As SimplexVertex = Me._v.Item(1)
                    vertex4.a = 1.0!
                    Me._v.Item(1) = vertex4
                    Me._count = 1
                    Me._v.Item(0) = Me._v.Item(1)
                Else
                    Dim num3 As Single = (1.0! / (num2 + num))
                    Dim vertex As SimplexVertex = Me._v.Item(0)
                    Dim vertex2 As SimplexVertex = Me._v.Item(1)
                    vertex.a = (num2 * num3)
                    vertex2.a = (num * num3)
                    Me._v.Item(0) = vertex
                    Me._v.Item(1) = vertex2
                    Me._count = 2
                End If
            End If
        End Sub

        Friend Sub Solve3()
            Dim w As Vector2 = Me._v.Item(0).w
            Dim a As Vector2 = Me._v.Item(1).w
            Dim b As Vector2 = Me._v.Item(2).w
            Dim vector4 As Vector2 = (a - w)
            Dim num As Single = Vector2.Dot(w, vector4)
            Dim num3 As Single = Vector2.Dot(a, vector4)
            Dim num4 As Single = -num
            Dim vector5 As Vector2 = (b - w)
            Dim num5 As Single = Vector2.Dot(w, vector5)
            Dim num7 As Single = Vector2.Dot(b, vector5)
            Dim num8 As Single = -num5
            Dim vector6 As Vector2 = (b - a)
            Dim num9 As Single = Vector2.Dot(a, vector6)
            Dim num11 As Single = Vector2.Dot(b, vector6)
            Dim num12 As Single = -num9
            Dim num13 As Single = MathUtils.Cross(vector4, vector5)
            Dim num14 As Single = (num13 * MathUtils.Cross(a, b))
            Dim num15 As Single = (num13 * MathUtils.Cross(b, w))
            Dim num16 As Single = (num13 * MathUtils.Cross(w, a))
            If ((num4 <= 0!) AndAlso (num8 <= 0!)) Then
                Dim vertex4 As SimplexVertex = Me._v.Item(0)
                vertex4.a = 1.0!
                Me._v.Item(0) = vertex4
                Me._count = 1
            ElseIf (((num3 > 0!) AndAlso (num4 > 0!)) AndAlso (num16 <= 0!)) Then
                Dim num18 As Single = (1.0! / (num3 + num4))
                Dim vertex5 As SimplexVertex = Me._v.Item(0)
                Dim vertex6 As SimplexVertex = Me._v.Item(1)
                vertex5.a = (num3 * num18)
                vertex6.a = (num3 * num18)
                Me._v.Item(0) = vertex5
                Me._v.Item(1) = vertex6
                Me._count = 2
            ElseIf (((num7 > 0!) AndAlso (num8 > 0!)) AndAlso (num15 <= 0!)) Then
                Dim num19 As Single = (1.0! / (num7 + num8))
                Dim vertex7 As SimplexVertex = Me._v.Item(0)
                Dim vertex8 As SimplexVertex = Me._v.Item(2)
                vertex7.a = (num7 * num19)
                vertex8.a = (num8 * num19)
                Me._v.Item(0) = vertex7
                Me._v.Item(2) = vertex8
                Me._count = 2
                Me._v.Item(1) = Me._v.Item(2)
            ElseIf ((num3 <= 0!) AndAlso (num12 <= 0!)) Then
                Dim vertex9 As SimplexVertex = Me._v.Item(1)
                vertex9.a = 1.0!
                Me._v.Item(1) = vertex9
                Me._count = 1
                Me._v.Item(0) = Me._v.Item(1)
            ElseIf ((num7 <= 0!) AndAlso (num11 <= 0!)) Then
                Dim vertex10 As SimplexVertex = Me._v.Item(2)
                vertex10.a = 1.0!
                Me._v.Item(2) = vertex10
                Me._count = 1
                Me._v.Item(0) = Me._v.Item(2)
            ElseIf (((num11 > 0!) AndAlso (num12 > 0!)) AndAlso (num14 <= 0!)) Then
                Dim num20 As Single = (1.0! / (num11 + num12))
                Dim vertex11 As SimplexVertex = Me._v.Item(1)
                Dim vertex12 As SimplexVertex = Me._v.Item(2)
                vertex11.a = (num11 * num20)
                vertex12.a = (num12 * num20)
                Me._v.Item(1) = vertex11
                Me._v.Item(2) = vertex12
                Me._count = 2
                Me._v.Item(0) = Me._v.Item(2)
            Else
                Dim num17 As Single = (1.0! / ((num14 + num15) + num16))
                Dim vertex As SimplexVertex = Me._v.Item(0)
                Dim vertex2 As SimplexVertex = Me._v.Item(1)
                Dim vertex3 As SimplexVertex = Me._v.Item(2)
                vertex.a = (num14 * num17)
                vertex2.a = (num15 * num17)
                vertex3.a = (num16 * num17)
                Me._v.Item(0) = vertex
                Me._v.Item(1) = vertex2
                Me._v.Item(2) = vertex3
                Me._count = 3
            End If
        End Sub
    End Structure
End Namespace

