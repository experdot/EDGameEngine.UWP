
Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Public Structure WorldManifold
        Public _normal As Vector2
        Public _points As FixedArray2(Of Vector2)
        Public Sub New(ByRef manifold As Manifold, ByRef xfA As XForm, ByVal radiusA As Single, ByRef xfB As XForm, ByVal radiusB As Single)
            Me._normal = Vector2.Zero
            Me._points = New FixedArray2(Of Vector2)
            If (Not manifold._pointCount = 0) Then
                Select Case manifold._type
                    Case ManifoldType.Circles
                        Dim vector As Vector2 = MathUtils.Multiply(xfA, manifold._localPoint)
                        Dim vector2 As Vector2 = MathUtils.Multiply(xfB, manifold._points.Item(0).LocalPoint)
                        Dim vec As New Vector2(1.0!, 0!)
                        If (Vector2.DistanceSquared(vector, vector2) > (Settings.b2_FLT_EPSILON * Settings.b2_FLT_EPSILON)) Then
                            vec = (vector2 - vector)
                            Extension.Normalize(vec)
                        End If
                        Me._normal = vec
                        Dim vector4 As Vector2 = (vector + (radiusA * vec))
                        Dim vector5 As Vector2 = (vector2 - (radiusB * vec))
                        Me._points.Item(0) = (0.5! * (vector4 + vector5))
                        Exit Select
                    Case ManifoldType.FaceA
                        Dim vector6 As Vector2 = MathUtils.Multiply(xfA.R, manifold._localPlaneNormal)
                        Dim vector7 As Vector2 = MathUtils.Multiply(xfA, manifold._localPoint)
                        Me._normal = vector6
                        Dim i As Integer
                        For i = 0 To manifold._pointCount - 1
                            Dim vector8 As Vector2 = MathUtils.Multiply(xfB, manifold._points.Item(i).LocalPoint)
                            Dim vector9 As Vector2 = (vector8 + ((radiusA - Vector2.Dot((vector8 - vector7), vector6)) * vector6))
                            Dim vector10 As Vector2 = (vector8 - (radiusB * vector6))
                            Me._points.Item(i) = (0.5! * (vector9 + vector10))
                        Next i
                        Exit Select
                    Case ManifoldType.FaceB
                        Dim vector11 As Vector2 = MathUtils.Multiply(xfB.R, manifold._localPlaneNormal)
                        Dim vector12 As Vector2 = MathUtils.Multiply(xfB, manifold._localPoint)
                        Me._normal = -vector11
                        Dim j As Integer
                        For j = 0 To manifold._pointCount - 1
                            Dim vector13 As Vector2 = MathUtils.Multiply(xfA, manifold._points.Item(j).LocalPoint)
                            Dim vector14 As Vector2 = (vector13 - (radiusA * vector11))
                            Dim vector15 As Vector2 = (vector13 + ((radiusB - Vector2.Dot((vector13 - vector12), vector11)) * vector11))
                            Me._points.Item(j) = (0.5! * (vector14 + vector15))
                        Next j
                        Exit Select
                End Select
            End If
        End Sub
    End Structure
End Namespace

