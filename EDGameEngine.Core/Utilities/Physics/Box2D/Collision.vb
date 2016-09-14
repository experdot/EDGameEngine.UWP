Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    Public Class Collision

        ' Methods
        Public Shared Function ClipSegmentToLine(<Out> ByRef vOut As FixedArray2(Of ClipVertex), ByRef vIn As FixedArray2(Of ClipVertex), ByVal normal As Vector2, ByVal offset As Single) As Integer
            vOut = New FixedArray2(Of ClipVertex)
            Dim num As Integer = 0
            Dim num2 As Single = (Vector2.Dot(normal, vIn.Item(0).v) - offset)
            Dim num3 As Single = (Vector2.Dot(normal, vIn.Item(1).v) - offset)
            If (num2 <= 0!) Then
                vOut.Item(num) = vIn.Item(0)
                num += 1
            End If
            If (num3 <= 0!) Then
                vOut.Item(num) = vIn.Item(1)
                num += 1
            End If
            If ((num2 * num3) < 0!) Then
                Dim num4 As Single = (num2 / (num2 - num3))
                Dim vertex As ClipVertex = vOut.Item(num)
                vertex.v = (vIn.Item(0).v + (num4 * (vIn.Item(1).v - vIn.Item(0).v)))
                If (num2 > 0!) Then
                    vertex.id = vIn.Item(0).id
                Else
                    vertex.id = vIn.Item(1).id
                End If
                vOut.Item(num) = vertex
                num += 1
            End If
            Return num
        End Function

        Public Shared Sub CollideCircles(ByRef manifold As Manifold, ByVal circle1 As CircleShape, ByRef xf1 As XForm, ByVal circle2 As CircleShape, ByRef xf2 As XForm)
            manifold._pointCount = 0
            Dim vector As Vector2 = MathUtils.Multiply(xf1, circle1._p)
            Dim vector3 As Vector2 = (MathUtils.Multiply(xf2, circle2._p) - vector)
            Dim num As Single = Vector2.Dot(vector3, vector3)
            Dim num2 As Single = (circle1._radius + circle2._radius)
            If (num <= (num2 * num2)) Then
                manifold._type = ManifoldType.Circles
                manifold._localPoint = circle1._p
                manifold._localPlaneNormal = Vector2.Zero
                manifold._pointCount = 1
                Dim point As ManifoldPoint = manifold._points.Item(0)
                point.LocalPoint = circle2._p
                point.Id.Key = 0
                manifold._points.Item(0) = point
            End If
        End Sub

        Public Shared Sub CollidePolygonAndCircle(ByRef manifold As Manifold, ByVal polygon As PolygonShape, ByRef xf1 As XForm, ByVal circle As CircleShape, ByRef xf2 As XForm)
            manifold._pointCount = 0
            Dim v As Vector2 = MathUtils.Multiply(xf2, circle._p)
            Dim vector2 As Vector2 = MathUtils.MultiplyT(xf1, v)
            Dim num As Integer = 0
            Dim num2 As Single = -Settings.b2_FLT_MAX
            Dim num3 As Single = (polygon._radius + circle._radius)
            Dim num4 As Integer = polygon._vertexCount
            Dim i As Integer
            For i = 0 To num4 - 1
                Dim num10 As Single = Vector2.Dot(polygon._normals.Item(i), (vector2 - polygon._vertices.Item(i)))
                If (num10 > num3) Then
                    Return
                End If
                If (num10 > num2) Then
                    num2 = num10
                    num = i
                End If
            Next i
            Dim num5 As Integer = num
            Dim num6 As Integer = If(((num5 + 1) < num4), (num5 + 1), 0)
            Dim vector3 As Vector2 = polygon._vertices.Item(num5)
            Dim vector4 As Vector2 = polygon._vertices.Item(num6)
            If (num2 < Settings.b2_FLT_EPSILON) Then
                manifold._pointCount = 1
                manifold._type = ManifoldType.FaceA
                manifold._localPlaneNormal = polygon._normals.Item(num)
                manifold._localPoint = (0.5! * (vector3 + vector4))
                Dim point As ManifoldPoint = manifold._points.Item(0)
                point.LocalPoint = circle._p
                point.Id.Key = 0
                manifold._points.Item(0) = point
            Else
                Dim num7 As Single = Vector2.Dot((vector2 - vector3), (vector4 - vector3))
                Dim num8 As Single = Vector2.Dot((vector2 - vector4), (vector3 - vector4))
                If (num7 <= 0!) Then
                    If (Vector2.DistanceSquared(vector2, vector3) <= (num3 * num3)) Then
                        manifold._pointCount = 1
                        manifold._type = ManifoldType.FaceA
                        manifold._localPlaneNormal = (vector2 - vector3)
                        Extension.Normalize(manifold._localPlaneNormal)
                        manifold._localPoint = vector3
                        Dim point2 As ManifoldPoint = manifold._points.Item(0)
                        point2.LocalPoint = circle._p
                        point2.Id.Key = 0
                        manifold._points.Item(0) = point2
                    End If
                ElseIf (num8 <= 0!) Then
                    If (Vector2.DistanceSquared(vector2, vector4) <= (num3 * num3)) Then
                        manifold._pointCount = 1
                        manifold._type = ManifoldType.FaceA
                        manifold._localPlaneNormal = (vector2 - vector4)
                        Extension.Normalize(manifold._localPlaneNormal)
                        manifold._localPoint = vector4
                        Dim point3 As ManifoldPoint = manifold._points.Item(0)
                        point3.LocalPoint = circle._p
                        point3.Id.Key = 0
                        manifold._points.Item(0) = point3
                    End If
                Else
                    Dim vector5 As Vector2 = (0.5! * (vector3 + vector4))
                    If (Vector2.Dot((vector2 - vector5), polygon._normals.Item(num5)) <= num3) Then
                        manifold._pointCount = 1
                        manifold._type = ManifoldType.FaceA
                        manifold._localPlaneNormal = polygon._normals.Item(num5)
                        manifold._localPoint = vector5
                        Dim point4 As ManifoldPoint = manifold._points.Item(0)
                        point4.LocalPoint = circle._p
                        point4.Id.Key = 0
                        manifold._points.Item(0) = point4
                    End If
                End If
            End If
        End Sub

        Public Shared Sub CollidePolygons(ByRef manifold As Manifold, ByVal polyA As PolygonShape, ByRef xfA As XForm, ByVal polyB As PolygonShape, ByRef xfB As XForm)
            manifold._pointCount = 0
            Dim num As Single = (polyA._radius + polyB._radius)
            Dim edgeIndex As Integer = 0
            Dim num3 As Single = Collision.FindMaxSeparation(edgeIndex, polyA, xfA, polyB, xfB)
            If (num3 <= num) Then
                Dim num4 As Integer = 0
                Dim num5 As Single = Collision.FindMaxSeparation(num4, polyB, xfB, polyA, xfA)
                If (num5 <= num) Then
                    Dim shape As PolygonShape
                    Dim shape2 As PolygonShape
                    Dim form As XForm
                    Dim form2 As XForm
                    Dim num6 As Integer
                    Dim num7 As Byte
                    Dim array As FixedArray2(Of ClipVertex)
                    Dim array2 As FixedArray2(Of ClipVertex)
                    Dim array3 As FixedArray2(Of ClipVertex)
                    Dim num8 As Single = 0.98!
                    Dim num9 As Single = 0.001!
                    If (num5 > ((num8 * num3) + num9)) Then
                        shape = polyB
                        shape2 = polyA
                        form = xfB
                        form2 = xfA
                        num6 = num4
                        manifold._type = ManifoldType.FaceB
                        num7 = 1
                    Else
                        shape = polyA
                        shape2 = polyB
                        form = xfA
                        form2 = xfB
                        num6 = edgeIndex
                        manifold._type = ManifoldType.FaceA
                        num7 = 0
                    End If
                    Collision.FindIncidentEdge(array, shape, form, num6, shape2, form2)
                    Dim num10 As Integer = shape._vertexCount
                    Dim v As Vector2 = shape._vertices.Item(num6)
                    Dim vector2 As Vector2 = If(((num6 + 1) < num10), shape._vertices.Item((num6 + 1)), shape._vertices.Item(0))
                    Dim a As Vector2 = (vector2 - v)
                    Dim vec As Vector2 = MathUtils.Cross(a, CSng(1.0!))
                    Extension.Normalize(vec)
                    Dim vector5 As Vector2 = (0.5! * (v + vector2))
                    Dim vector6 As Vector2 = MathUtils.Multiply(form.R, (vector2 - v))
                    Extension.Normalize(vector6)
                    Dim vector7 As Vector2 = MathUtils.Cross(vector6, CSng(1.0!))
                    v = MathUtils.Multiply(form, v)
                    vector2 = MathUtils.Multiply(form, vector2)
                    Dim num11 As Single = Vector2.Dot(vector7, v)
                    Dim offset As Single = -Vector2.Dot(vector6, v)
                    Dim num13 As Single = Vector2.Dot(vector6, vector2)
                    If ((Collision.ClipSegmentToLine(array2, array, -vector6, offset) >= 2) AndAlso (Collision.ClipSegmentToLine(array3, array2, vector6, num13) >= 2)) Then
                        manifold._localPlaneNormal = vec
                        manifold._localPoint = vector5
                        Dim num15 As Integer = 0
                        Dim i As Integer
                        For i = 0 To Settings.b2_maxManifoldPoints - 1
                            Dim num17 As Single = (Vector2.Dot(vector7, array3.Item(i).v) - num11)
                            If (num17 <= num) Then
                                Dim point As ManifoldPoint = manifold._points.Item(num15)
                                point.LocalPoint = MathUtils.MultiplyT(form2, array3.Item(i).v)
                                point.Id = array3.Item(i).id
                                point.Id.Features.Flip = num7
                                manifold._points.Item(num15) = point
                                num15 += 1
                            End If
                        Next i
                        manifold._pointCount = num15
                    End If
                End If
            End If
        End Sub

        Private Shared Function EdgeSeparation(ByVal poly1 As PolygonShape, ByRef xf1 As XForm, ByVal edge1 As Integer, ByVal poly2 As PolygonShape, ByRef xf2 As XForm) As Single
            Dim num As Integer = poly1._vertexCount
            Dim num2 As Integer = poly2._vertexCount
            Debug.Assert(((0 <= edge1) AndAlso (edge1 < num)))
            Dim v As Vector2 = MathUtils.Multiply(xf1.R, poly1._normals.Item(edge1))
            Dim vector2 As Vector2 = MathUtils.MultiplyT(xf2.R, v)
            Dim num3 As Integer = 0
            Dim num4 As Single = Settings.b2_FLT_MAX
            Dim i As Integer
            For i = 0 To num2 - 1
                Dim num7 As Single = Vector2.Dot(poly2._vertices.Item(i), vector2)
                If (num7 < num4) Then
                    num4 = num7
                    num3 = i
                End If
            Next i
            Dim vector3 As Vector2 = MathUtils.Multiply(xf1, poly1._vertices.Item(edge1))
            Return Vector2.Dot((MathUtils.Multiply(xf2, poly2._vertices.Item(num3)) - vector3), v)
        End Function

        Private Shared Sub FindIncidentEdge(<Out> ByRef c As FixedArray2(Of ClipVertex), ByVal poly1 As PolygonShape, ByRef xf1 As XForm, ByVal edge1 As Integer, ByVal poly2 As PolygonShape, ByRef xf2 As XForm)
            c = New FixedArray2(Of ClipVertex)
            Dim num As Integer = poly1._vertexCount
            Dim num2 As Integer = poly2._vertexCount
            Debug.Assert(((0 <= edge1) AndAlso (edge1 < num)))
            Dim vector As Vector2 = MathUtils.MultiplyT(xf2.R, MathUtils.Multiply(xf1.R, poly1._normals.Item(edge1)))
            Dim num3 As Integer = 0
            Dim num4 As Single = Settings.b2_FLT_MAX
            Dim i As Integer
            For i = 0 To num2 - 1
                Dim num8 As Single = Vector2.Dot(vector, poly2._normals.Item(i))
                If (num8 < num4) Then
                    num4 = num8
                    num3 = i
                End If
            Next i
            Dim num5 As Integer = num3
            Dim num6 As Integer = If(((num5 + 1) < num2), (num5 + 1), 0)
            Dim vertex As ClipVertex = c.Item(0)
            vertex.v = MathUtils.Multiply(xf2, poly2._vertices.Item(num5))
            vertex.id.Features.ReferenceEdge = CType(edge1, Byte)
            vertex.id.Features.IncidentEdge = CType(num5, Byte)
            vertex.id.Features.IncidentVertex = 0
            c.Item(0) = vertex
            Dim vertex2 As ClipVertex = c.Item(1)
            vertex2.v = MathUtils.Multiply(xf2, poly2._vertices.Item(num6))
            vertex2.id.Features.ReferenceEdge = CType(edge1, Byte)
            vertex2.id.Features.IncidentEdge = CType(num6, Byte)
            vertex2.id.Features.IncidentVertex = 1
            c.Item(1) = vertex2
        End Sub

        Private Shared Function FindMaxSeparation(<Out> ByRef edgeIndex As Integer, ByVal poly1 As PolygonShape, ByRef xf1 As XForm, ByVal poly2 As PolygonShape, ByRef xf2 As XForm) As Single
            Dim num9 As Integer
            Dim num10 As Single
            Dim num11 As Integer
            edgeIndex = -1
            Dim num As Integer = poly1._vertexCount
            Dim v As Vector2 = (MathUtils.Multiply(xf2, poly2._centroid) - MathUtils.Multiply(xf1, poly1._centroid))
            Dim vector2 As Vector2 = MathUtils.MultiplyT(xf1.R, v)
            Dim num2 As Integer = 0
            Dim num3 As Single = -Settings.b2_FLT_MAX
            Dim i As Integer
            For i = 0 To num - 1
                Dim num13 As Single = Vector2.Dot(poly1._normals.Item(i), vector2)
                If (num13 > num3) Then
                    num3 = num13
                    num2 = i
                End If
            Next i
            Dim num4 As Single = Collision.EdgeSeparation(poly1, xf1, num2, poly2, xf2)
            Dim num5 As Integer = If(((num2 - 1) >= 0), (num2 - 1), (num - 1))
            Dim num6 As Single = Collision.EdgeSeparation(poly1, xf1, num5, poly2, xf2)
            Dim num7 As Integer = If(((num2 + 1) < num), (num2 + 1), 0)
            Dim num8 As Single = Collision.EdgeSeparation(poly1, xf1, num7, poly2, xf2)
            If ((num6 > num4) AndAlso (num6 > num8)) Then
                num11 = -1
                num9 = num5
                num10 = num6
            ElseIf (num8 > num4) Then
                num11 = 1
                num9 = num7
                num10 = num8
            Else
                edgeIndex = num2
                Return num4
            End If
Label_0115:
            If (num11 = -1) Then
                num2 = If(((num9 - 1) >= 0), (num9 - 1), (num - 1))
            Else
                num2 = If(((num9 + 1) < num), (num9 + 1), 0)
            End If
            num4 = Collision.EdgeSeparation(poly1, xf1, num2, poly2, xf2)
            If (num4 > num10) Then
                num9 = num2
                num10 = num4
                GoTo Label_0115
            End If
            edgeIndex = num9
            Return num10
        End Function

        Public Shared Sub GetPointStates(<Out> ByRef state1 As FixedArray2(Of PointState), <Out> ByRef state2 As FixedArray2(Of PointState), ByRef manifold1 As Manifold, ByRef manifold2 As Manifold)
            state1 = New FixedArray2(Of PointState)
            state2 = New FixedArray2(Of PointState)
            Dim i As Integer
            For i = 0 To manifold1._pointCount - 1
                Dim id As ContactID = manifold1._points.Item(i).Id
                state1.Item(i) = PointState.Remove
                Dim k As Integer
                For k = 0 To manifold2._pointCount - 1
                    If (manifold2._points.Item(k).Id.Key = id.Key) Then
                        state1.Item(i) = PointState.Persist
                        Exit For
                    End If
                Next k
            Next i
            Dim j As Integer
            For j = 0 To manifold2._pointCount - 1
                Dim tid2 As ContactID = manifold2._points.Item(j).Id
                state2.Item(j) = PointState.Add
                Dim m As Integer
                For m = 0 To manifold1._pointCount - 1
                    If (manifold1._points.Item(m).Id.Key = tid2.Key) Then
                        state2.Item(j) = PointState.Persist
                        Exit For
                    End If
                Next m
            Next j
        End Sub

    End Class
End Namespace

