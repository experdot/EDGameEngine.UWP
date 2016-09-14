
Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure PositionSolverManifold
        Friend _normal As Vector2
        Friend _points As FixedArray2(Of Vector2)
        Friend _separations As FixedArray2(Of Single)
        Friend Sub New(ByRef cc As ContactConstraint)
            Dim worldPoint As Vector2
            Dim vector2 As Vector2
            Me._points = New FixedArray2(Of Vector2)
            Me._separations = New FixedArray2(Of Single)
            Me._normal = New Vector2
            Debug.Assert((cc.pointCount > 0))
            Select Case cc.type
                Case ManifoldType.Circles
                    worldPoint = cc.bodyA.GetWorldPoint(cc.localPoint)
                    vector2 = cc.bodyB.GetWorldPoint(cc.points.Item(0).localPoint)
                    If (Vector2.DistanceSquared(worldPoint, vector2) <= (Settings.b2_FLT_EPSILON * Settings.b2_FLT_EPSILON)) Then
                        Me._normal = New Vector2(1.0!, 0!)
                        Exit Select
                    End If
                    Me._normal = (vector2 - worldPoint)
                    Extension.Normalize(Me._normal)
                    Exit Select
                Case ManifoldType.FaceA
                    Me._normal = cc.bodyA.GetWorldVector(cc.localPlaneNormal)
                    Dim vector3 As Vector2 = cc.bodyA.GetWorldPoint(cc.localPoint)
                    Dim i As Integer
                    For i = 0 To cc.pointCount - 1
                        Dim vector4 As Vector2 = cc.bodyB.GetWorldPoint(cc.points.Item(i).localPoint)
                        Me._separations.Item(i) = (Vector2.Dot((vector4 - vector3), Me._normal) - cc.radius)
                        Me._points.Item(i) = vector4
                    Next i
                    Return
                Case ManifoldType.FaceB
                    Me._normal = cc.bodyB.GetWorldVector(cc.localPlaneNormal)
                    Dim vector5 As Vector2 = cc.bodyB.GetWorldPoint(cc.localPoint)
                    Dim j As Integer
                    For j = 0 To cc.pointCount - 1
                        Dim vector6 As Vector2 = cc.bodyA.GetWorldPoint(cc.points.Item(j).localPoint)
                        Me._separations.Item(j) = (Vector2.Dot((vector6 - vector5), Me._normal) - cc.radius)
                        Me._points.Item(j) = vector6
                    Next j
                    Me._normal = -Me._normal
                    Return
                Case Else
                    Return
            End Select
            Me._points.Item(0) = (0.5! * (worldPoint + vector2))
            Me._separations.Item(0) = (Vector2.Dot((vector2 - worldPoint), Me._normal) - cc.radius)
        End Sub
    End Structure
End Namespace

