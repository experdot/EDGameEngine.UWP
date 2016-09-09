Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    Public Class CircleShape
        Inherits Shape
        ' Methods
        Public Sub New()
            MyBase.ShapeType = ShapeType.Circle
            MyBase._radius = 0!
            Me._p = Vector2.Zero
        End Sub

        Public Overrides Function Clone() As Shape
            Return New CircleShape With {
                .ShapeType = MyBase.ShapeType,
                ._radius = MyBase._radius,
                ._p = Me._p
            }
        End Function

        Public Overrides Sub ComputeAABB(<Out> ByRef aabb As AABB, ByRef transform As XForm)
            Dim vector As Vector2 = (transform.Position + MathUtils.Multiply(transform.R, Me._p))
            aabb.lowerBound = New Vector2((vector.X - MyBase._radius), (vector.Y - MyBase._radius))
            aabb.upperBound = New Vector2((vector.X + MyBase._radius), (vector.Y + MyBase._radius))
        End Sub

        Public Overrides Sub ComputeMass(<Out> ByRef massData As MassData, ByVal density As Single)
            massData.mass = (((density * Settings.b2_pi) * MyBase._radius) * MyBase._radius)
            massData.center = Me._p
            massData.i = (massData.mass * (((0.5! * MyBase._radius) * MyBase._radius) + Vector2.Dot(Me._p, Me._p)))
        End Sub

        Public Overrides Function GetSupport(ByVal d As Vector2) As Integer
            Return 0
        End Function

        Public Overrides Function GetSupportVertex(ByVal d As Vector2) As Vector2
            Return Me._p
        End Function

        Public Overrides Function GetVertex(ByVal index As Integer) As Vector2
            Debug.Assert((index = 0))
            Return Me._p
        End Function

        Public Overrides Function GetVertexCount() As Integer
            Return 1
        End Function

        Public Overrides Function TestPoint(ByRef transform As XForm, ByVal p As Vector2) As Boolean
            Dim vector As Vector2 = (transform.Position + MathUtils.Multiply(transform.R, Me._p))
            Dim vector2 As Vector2 = (p - vector)
            Return (Vector2.Dot(vector2, vector2) <= (MyBase._radius * MyBase._radius))
        End Function

        Public Overrides Function TestSegment(ByRef transform As XForm, <Out> ByRef lambda As Single, <Out> ByRef normal As Vector2, ByRef segment As Segment, ByVal maxLambda As Single) As SegmentCollide
            lambda = 0!
            normal = Numerics.Vector2.Zero
            Dim vector As Vector2 = (transform.Position + MathUtils.Multiply(transform.R, Me._p))
            Dim vector2 As Vector2 = (segment.p1 - vector)
            Dim num As Single = (Vector2.Dot(vector2, vector2) - (MyBase._radius * MyBase._radius))
            If (num < 0!) Then
                Return SegmentCollide.StartsInside
            End If
            Dim vector3 As Vector2 = (segment.p2 - segment.p1)
            Dim num2 As Single = Vector2.Dot(vector2, vector3)
            Dim num3 As Single = Vector2.Dot(vector3, vector3)
            Dim num4 As Single = ((num2 * num2) - (num3 * num))
            If ((num4 >= 0!) AndAlso (num3 >= Settings.b2_FLT_EPSILON)) Then
                Dim num5 As Single = -(num2 + CType(Math.Sqrt(num4), Single))
                If ((0! <= num5) AndAlso (num5 <= (maxLambda * num3))) Then
                    num5 = (num5 / num3)
                    lambda = num5
                    normal = (vector2 + (num5 * vector3))
                    Extension.Normalize(normal)
                    Return SegmentCollide.Hit
                End If
            End If
            Return SegmentCollide.Miss
        End Function


        ' Fields
        Public _p As Vector2
    End Class
End Namespace

