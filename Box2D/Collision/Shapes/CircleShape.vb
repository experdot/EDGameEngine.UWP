Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    ''' <summary>
    ''' 表示一个圆形形状，继承自<see cref="Shape"/> 
    ''' </summary>
    Public Class CircleShape
        Inherits Shape
        ''' <summary>
        ''' 获取或设置当前<see cref="CircleShape"/>对象的位置
        ''' </summary>
        Public Property Position As Vector2

        Public Sub New()
            MyBase.ShapeType = ShapeType.Circle
            MyBase.Radius = 0!
            Me.Position = Vector2.Zero
        End Sub
        ''' <summary>
        ''' 返回当前<see cref="CircleShape"/>对象的克隆体
        ''' </summary>
        Public Overrides Function Clone() As Shape
            Return New CircleShape With {
                .ShapeType = MyBase.ShapeType,
                .Radius = MyBase.Radius,
                .Position = Me.Position
            }
        End Function
        ''' <summary>
        ''' 计算当前<see cref="CircleShape"/>对象的AABB
        ''' </summary>
        Public Overrides Sub ComputeAABB(<Out> ByRef aabb As AABB, ByRef transform As XForm)
            Dim vector As Vector2 = (transform.Position + MathUtils.Multiply(transform.RoateMatrix, Me.Position))
            aabb.LowerBound = New Vector2((vector.X - MyBase.Radius), (vector.Y - MyBase.Radius))
            aabb.UpperBound = New Vector2((vector.X + MyBase.Radius), (vector.Y + MyBase.Radius))
        End Sub
        ''' <summary>
        ''' 计算当前<see cref="CircleShape"/>对象的质量
        ''' </summary>
        Public Overrides Sub ComputeMass(<Out> ByRef massData As MassData, ByVal density As Single)
            massData.Mass = (((density * Settings.b2_pi) * MyBase.Radius) * MyBase.Radius)
            massData.Centroid = Me.Position
            massData.InertiaMoment = (massData.Mass * (((0.5! * MyBase.Radius) * MyBase.Radius) + Vector2.Dot(Me.Position, Me.Position)))
        End Sub
        ''' <summary>
        ''' 返回指定方向上支持向量的索引
        ''' </summary>
        Public Overrides Function GetSupport(ByVal direction As Vector2) As Integer
            Return 0
        End Function
        ''' <summary>
        ''' 返回指定方向上支持向量的顶点
        ''' </summary>
        Public Overrides Function GetSupportVertex(ByVal direction As Vector2) As Vector2
            Return Me.Position
        End Function
        ''' <summary>
        ''' 返回当前<see cref="CircleShape"/>对象指定索引的顶点
        ''' </summary>
        Public Overrides Function GetVertex(ByVal index As Integer) As Vector2
            Debug.Assert((index = 0))
            Return Me.Position
        End Function
        ''' <summary>
        ''' 返回当前<see cref="CircleShape"/>对象的顶点数量
        ''' </summary>
        Public Overrides Function GetVertexCount() As Integer
            Return 1
        End Function
        ''' <summary>
        ''' 返回指定点是否在当前<see cref="CircleShape"/>对象的形状半径内
        ''' </summary>
        Public Overrides Function TestPoint(ByRef transform As XForm, ByVal point As Vector2) As Boolean
            Dim vector As Vector2 = (transform.Position + MathUtils.Multiply(transform.RoateMatrix, Me.Position))
            Dim vector2 As Vector2 = (point - vector)
            Return (Vector2.Dot(vector2, vector2) <= (MyBase.Radius * MyBase.Radius))
        End Function
        ''' <summary>
        ''' 尝试切割当前形状
        ''' </summary>
        Public Overrides Function TestSegment(ByRef transform As XForm, <Out> ByRef lambda As Single, <Out> ByRef normal As Vector2, ByRef segment As Segment, ByVal maxLambda As Single) As SegmentCollide
            lambda = 0!
            normal = Numerics.Vector2.Zero
            Dim vector As Vector2 = (transform.Position + MathUtils.Multiply(transform.RoateMatrix, Me.Position))
            Dim vector2 As Vector2 = (segment.Point1 - vector)
            Dim num As Single = (Vector2.Dot(vector2, vector2) - (MyBase.Radius * MyBase.Radius))
            If (num < 0!) Then
                Return SegmentCollide.StartsInside
            End If
            Dim vector3 As Vector2 = (segment.Point2 - segment.Point1)
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
    End Class
End Namespace

