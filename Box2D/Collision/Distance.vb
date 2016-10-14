
Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    Public Class Distance
        
        ' Methods
        Public Shared Sub ComputeDistance(<Out> ByRef output As DistanceOutput, <Out> ByRef cache As SimplexCache, ByRef input As DistanceInput, ByVal shapeA As Shape, ByVal shapeB As Shape)
            cache = New SimplexCache
            Distance.b2_gjkCalls += 1
            Dim transformA As XForm = input.TransformA
            Dim transformB As XForm = input.TransformB
            Dim simplex As New Simplex
            simplex.ReadCache(cache, shapeA, transformA, shapeB, transformB)
            Dim num As Integer = 20
            Dim array As New FixedArray3(Of Integer)
            Dim array2 As New FixedArray3(Of Integer)
            Dim num2 As Integer = 0
            Dim num3 As Single = simplex.GetClosestPoint.LengthSquared
            Dim num4 As Single = num3
            Dim num5 As Integer = 0
            Do While (num5 < num)
                num2 = simplex._count
                Dim i As Integer
                For i = 0 To num2 - 1
                    array.Item(i) = simplex._v.Item(i).indexA
                    array2.Item(i) = simplex._v.Item(i).indexB
                Next i
                Select Case simplex._count
                    Case 1
                        Exit Select
                    Case 2
                        simplex.Solve2()
                        Exit Select
                    Case 3
                        simplex.Solve3()
                        Exit Select
                    Case Else
                        Debug.Assert(False)
                        Exit Select
                End Select
                If (simplex._count = 3) Then
                    Exit Do
                End If
                num4 = simplex.GetClosestPoint.LengthSquared
                If (num4 >= num3) Then
                End If
                num3 = num4
                Dim searchDirection As Vector2 = simplex.GetSearchDirection
                If (searchDirection.LengthSquared < (Settings.b2_FLT_EPSILON * Settings.b2_FLT_EPSILON)) Then
                    Exit Do
                End If
                Dim vertex As SimplexVertex = simplex._v.Item(simplex._count)
                vertex.indexA = shapeA.GetSupport(MathUtils.MultiplyT(transformA.RoateMatrix, -searchDirection))
                vertex.wA = MathUtils.Multiply(transformA, shapeA.GetVertex(vertex.indexA))
                vertex.indexB = shapeB.GetSupport(MathUtils.MultiplyT(transformB.RoateMatrix, searchDirection))
                vertex.wB = MathUtils.Multiply(transformB, shapeB.GetVertex(vertex.indexB))
                vertex.w = (vertex.wB - vertex.wA)
                simplex._v.Item(simplex._count) = vertex
                num5 += 1
                Distance.b2_gjkIters += 1
                Dim flag As Boolean = False
                Dim j As Integer
                For j = 0 To num2 - 1
                    If ((vertex.indexA = array.Item(j)) AndAlso (vertex.indexB = array2.Item(j))) Then
                        flag = True
                        Exit For
                    End If
                Next j
                If flag Then
                    Exit Do
                End If
                simplex._count += 1
            Loop
            Distance.b2_gjkMaxIters = Math.Max(Distance.b2_gjkMaxIters, num5)
            simplex.GetWitnessPoints(output.PointA, output.PointB)
            output.Distance = (output.PointA - output.PointB).Length
            output.Iterations = num5
            simplex.WriteCache(cache)
            If input.useRadii Then
                Dim num9 As Single = shapeA.Radius
                Dim num10 As Single = shapeB.Radius
                If ((output.Distance > (num9 + num10)) AndAlso (output.Distance > Settings.b2_FLT_EPSILON)) Then
                    output.Distance = (output.Distance - (num9 + num10))
                    Dim vec As Vector2 = (output.PointB - output.PointA)
                    Extension.Normalize(vec)
                    output.PointA = (output.PointA + (num9 * vec))
                    output.PointB = (output.PointB - (num10 * vec))
                Else
                    Dim vector6 As Vector2 = (0.5! * (output.PointA + output.PointB))
                    output.PointA = vector6
                    output.PointB = vector6
                    output.Distance = 0!
                End If
            End If
        End Sub


        ' Fields
        Private Shared b2_gjkCalls As Integer
        Private Shared b2_gjkIters As Integer
        Private Shared b2_gjkMaxIters As Integer
    End Class
End Namespace

