
Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    ''' <summary>
    ''' 表示一个多边形形状
    ''' </summary>
    Public Class PolygonShape
        Inherits Shape
        ''' <summary>
        ''' 获取或设置当前<see cref="PolygonShape"/>对象的质心
        ''' </summary>
        Public Centroid As Vector2
        ''' <summary>
        ''' 获取或设置当前<see cref="PolygonShape"/>对象的顶点数量
        ''' </summary>
        Public VertexCount As Integer
        ''' <summary>
        ''' 获取或设置当前<see cref="PolygonShape"/>对象顶点集合
        ''' </summary>
        Public Vertices As FixedArray8(Of Vector2)
        ''' <summary>
        ''' 获取或设置当前<see cref="PolygonShape"/>对象的边线法向量
        ''' </summary>
        Public Normals As FixedArray8(Of Vector2)

        Public Sub New()
            MyBase.ShapeType = ShapeType.Polygon
            MyBase.Radius = Settings.b2_polygonRadius
            Me.VertexCount = 0
            Me.Centroid = Vector2.Zero
        End Sub
        ''' <summary>
        ''' 返回当前<see cref="PolygonShape"/>对象的克隆体
        ''' </summary>
        Public Overrides Function Clone() As Shape
            Return New PolygonShape With {
                .ShapeType = MyBase.ShapeType,
                .Radius = MyBase.Radius,
                .VertexCount = Me.VertexCount,
                .Centroid = Me.Centroid,
                .Vertices = Me.Vertices,
                .Normals = Me.Normals
            }
        End Function
        ''' <summary>
        ''' 计算当前<see cref="PolygonShape"/>对象的AABB
        ''' </summary>
        Public Overrides Sub ComputeAABB(<Out> ByRef aabb As AABB, ByRef xf As XForm)
            Dim vector As Vector2 = MathUtils.Multiply(xf, Me.Vertices.Item(0))
            Dim vector2 As Vector2 = vector
            Dim i As Integer
            For i = 1 To Me.VertexCount - 1
                Dim vector4 As Vector2 = MathUtils.Multiply(xf, Me.Vertices.Item(i))
                vector = Vector2.Min(vector, vector4)
                vector2 = Vector2.Max(vector2, vector4)
            Next i
            Dim vector3 As New Vector2(MyBase.Radius, MyBase.Radius)
            aabb.LowerBound = (vector - vector3)
            aabb.UpperBound = (vector2 + vector3)
        End Sub
        ''' <summary>
        ''' 计算当前<see cref="PolygonShape"/>对象的质心
        ''' </summary>
        Private Shared Function ComputeCentroid(ByRef vs As FixedArray8(Of Vector2), ByVal count As Integer) As Vector2
            Debug.Assert((count >= 2))
            Dim vector As New Vector2(0!, 0!)
            Dim num As Single = 0!
            If (count = 2) Then
                Return (0.5! * (vs.Item(0) + vs.Item(1)))
            End If
            Dim vector2 As New Vector2(0!, 0!)
            Dim num2 As Single = 0.3333333!
            Dim i As Integer
            For i = 0 To count - 1
                Dim vector4 As Vector2 = vector2
                Dim vector5 As Vector2 = vs.Item(i)
                Dim vector6 As Vector2 = If(((i + 1) < count), vs.Item((i + 1)), vs.Item(0))
                Dim a As Vector2 = (vector5 - vector4)
                Dim b As Vector2 = (vector6 - vector4)
                Dim num4 As Single = MathUtils.Cross(a, b)
                Dim num5 As Single = (0.5! * num4)
                num = (num + num5)
                vector = (vector + ((num5 * num2) * ((vector4 + vector5) + vector6)))
            Next i
            Debug.Assert((num > Settings.b2_FLT_EPSILON))
            Return (vector * (1.0! / num))
        End Function
        ''' <summary>
        ''' 计算当前<see cref="PolygonShape"/>对象的质量
        ''' </summary>
        Public Overrides Sub ComputeMass(<Out> ByRef massData As MassData, ByVal density As Single)
            Debug.Assert((Me.VertexCount >= 3))
            Dim vector As New Vector2(0!, 0!)
            Dim num As Single = 0!
            Dim num2 As Single = 0!
            Dim vector2 As New Vector2(0!, 0!)
            Dim num3 As Single = 0.3333333!
            Dim i As Integer
            For i = 0 To Me.VertexCount - 1
                Dim vector3 As Vector2 = vector2
                Dim vector4 As Vector2 = Me.Vertices.Item(i)
                Dim vector5 As Vector2 = If(((i + 1) < Me.VertexCount), Me.Vertices.Item((i + 1)), Me.Vertices.Item(0))
                Dim a As Vector2 = (vector4 - vector3)
                Dim b As Vector2 = (vector5 - vector3)
                Dim num5 As Single = MathUtils.Cross(a, b)
                Dim num6 As Single = (0.5! * num5)
                num = (num + num6)
                vector = (vector + ((num6 * num3) * ((vector3 + vector4) + vector5)))
                Dim x As Single = vector3.X
                Dim y As Single = vector3.Y
                Dim num9 As Single = a.X
                Dim num10 As Single = a.Y
                Dim num11 As Single = b.X
                Dim num12 As Single = b.Y
                Dim num13 As Single = ((num3 * ((0.25! * (((num9 * num9) + (num11 * num9)) + (num11 * num11))) + ((x * num9) + (x * num11)))) + ((0.5! * x) * x))
                Dim num14 As Single = ((num3 * ((0.25! * (((num10 * num10) + (num12 * num10)) + (num12 * num12))) + ((y * num10) + (y * num12)))) + ((0.5! * y) * y))
                num2 = (num2 + (num5 * (num13 + num14)))
            Next i
            massData.Mass = (density * num)
            Debug.Assert((num > Settings.b2_FLT_EPSILON))
            vector = (vector * (1.0! / num))
            massData.Centroid = vector
            massData.InertiaMoment = (density * num2)
        End Sub
        ''' <summary>
        ''' 返回指定方向上支持向量的索引
        ''' </summary>
        Public Overrides Function GetSupport(ByVal direction As Vector2) As Integer
            Dim num As Integer = 0
            Dim num2 As Single = Vector2.Dot(Me.Vertices.Item(0), direction)
            Dim i As Integer
            For i = 1 To Me.VertexCount - 1
                Dim num4 As Single = Vector2.Dot(Me.Vertices.Item(i), direction)
                If (num4 > num2) Then
                    num = i
                    num2 = num4
                End If
            Next i
            Return num
        End Function
        ''' <summary>
        ''' 返回指定方向上支持向量的顶点
        ''' </summary>
        Public Overrides Function GetSupportVertex(ByVal direction As Vector2) As Vector2
            Dim num As Integer = 0
            Dim num2 As Single = Vector2.Dot(Me.Vertices.Item(0), direction)
            Dim i As Integer
            For i = 1 To Me.VertexCount - 1
                Dim num4 As Single = Vector2.Dot(Me.Vertices.Item(i), direction)
                If (num4 > num2) Then
                    num = i
                    num2 = num4
                End If
            Next i
            Return Me.Vertices.Item(num)
        End Function
        ''' <summary>
        ''' 返回当前<see cref="PolygonShape"/>对象指定索引的顶点
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        Public Overrides Function GetVertex(ByVal index As Integer) As Vector2
            Debug.Assert(((0 <= index) AndAlso (index < Me.VertexCount)))
            Return Me.Vertices.Item(index)
        End Function
        ''' <summary>
        ''' 返回当前<see cref="PolygonShape"/>对象的顶点数量
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function GetVertexCount() As Integer
            Return Me.VertexCount
        End Function

        Public Sub SetValue(ByVal vertices As Vector2(), ByVal count As Integer)
            Debug.Assert(((2 <= count) AndAlso (count <= Settings.b2_maxPolygonVertices)))
            Me.VertexCount = count
            Dim i As Integer
            For i = 0 To Me.VertexCount - 1
                Me.Vertices.Item(i) = vertices(i)
            Next i
            Dim j As Integer
            For j = 0 To Me.VertexCount - 1
                Dim num3 As Integer = j
                Dim num4 As Integer = If(((j + 1) < Me.VertexCount), (j + 1), 0)
                Dim a As Vector2 = (Me.Vertices.Item(num4) - Me.Vertices.Item(num3))
                Debug.Assert((a.LengthSquared > (Settings.b2_FLT_EPSILON * Settings.b2_FLT_EPSILON)))
                Dim vec As Vector2 = MathUtils.Cross(a, CSng(1.0!))
                Extension.Normalize(vec)
                Me.Normals.Item(j) = vec
            Next j
            Me.Centroid = PolygonShape.ComputeCentroid(Me.Vertices, Me.VertexCount)
        End Sub
        ''' <summary>
        ''' 设置当前<see cref="PolygonShape"/>对象为矩形盒子
        ''' </summary>
        Public Sub SetAsBox(ByVal hx As Single, ByVal hy As Single)
            Me.VertexCount = 4
            Me.Vertices.Item(0) = New Vector2(-hx, -hy)
            Me.Vertices.Item(1) = New Vector2(hx, -hy)
            Me.Vertices.Item(2) = New Vector2(hx, hy)
            Me.Vertices.Item(3) = New Vector2(-hx, hy)
            Me.Normals.Item(0) = New Vector2(0!, -1.0!)
            Me.Normals.Item(1) = New Vector2(1.0!, 0!)
            Me.Normals.Item(2) = New Vector2(0!, 1.0!)
            Me.Normals.Item(3) = New Vector2(-1.0!, 0!)
            Me.Centroid = Vector2.Zero
        End Sub
        ''' <summary>
        ''' 设置当前<see cref="PolygonShape"/>对象为矩形盒子
        ''' </summary>
        Public Sub SetAsBox(ByVal hx As Single, ByVal hy As Single, ByVal center As Vector2, ByVal angle As Single)
            Me.VertexCount = 4
            Me.Vertices.Item(0) = New Vector2(-hx, -hy)
            Me.Vertices.Item(1) = New Vector2(hx, -hy)
            Me.Vertices.Item(2) = New Vector2(hx, hy)
            Me.Vertices.Item(3) = New Vector2(-hx, hy)
            Me.Normals.Item(0) = New Vector2(0!, -1.0!)
            Me.Normals.Item(1) = New Vector2(1.0!, 0!)
            Me.Normals.Item(2) = New Vector2(0!, 1.0!)
            Me.Normals.Item(3) = New Vector2(-1.0!, 0!)
            Me.Centroid = center
            Dim t As New XForm With {
                .Position = center
            }
            t.RoateMatrix.CreateFromAngle(angle)
            Dim i As Integer
            For i = 0 To Me.VertexCount - 1
                Me.Vertices.Item(i) = MathUtils.Multiply(t, Me.Vertices.Item(i))
                Me.Normals.Item(i) = MathUtils.Multiply(t.RoateMatrix, Me.Normals.Item(i))
            Next i
        End Sub
        ''' <summary>
        ''' 设置当前<see cref="PolygonShape"/>对象为边线
        ''' </summary>
        Public Sub SetAsEdge(ByVal v1 As Vector2, ByVal v2 As Vector2)
            Me.VertexCount = 2
            Me.Vertices.Item(0) = v1
            Me.Vertices.Item(1) = v2
            Me.Centroid = (0.5! * (v1 + v2))
            Dim vec As Vector2 = MathUtils.Cross((v2 - v1), CSng(1.0!))
            Extension.Normalize(vec)
            Me.Normals.Item(0) = vec
            Me.Normals.Item(1) = -Me.Normals.Item(0)
        End Sub
        ''' <summary>
        ''' 返回指定点是否在当前<see cref="PolygonShape"/>对象的形状半径内
        ''' </summary>
        Public Overrides Function TestPoint(ByRef xf As XForm, ByVal p As Vector2) As Boolean
            Dim vector As Vector2 = MathUtils.MultiplyT(xf.RoateMatrix, (p - xf.Position))
            Dim i As Integer
            For i = 0 To Me.VertexCount - 1
                If (Vector2.Dot(Me.Normals.Item(i), (vector - Me.Vertices.Item(i))) > 0!) Then
                    Return False
                End If
            Next i
            Return True
        End Function
        ''' <summary>
        ''' 尝试切割当前形状
        ''' </summary>
        Public Overrides Function TestSegment(ByRef xf As XForm, <Out> ByRef lambda As Single, <Out> ByRef normal As Vector2, ByRef segment As Segment, ByVal maxLambda As Single) As SegmentCollide
            lambda = 0!
            normal = Vector2.Zero
            Dim num As Single = 0!
            Dim num2 As Single = maxLambda
            Dim vector As Vector2 = MathUtils.MultiplyT(xf.RoateMatrix, (segment.Point1 - xf.Position))
            Dim vector3 As Vector2 = (MathUtils.MultiplyT(xf.RoateMatrix, (segment.Point2 - xf.Position)) - vector)
            Dim num3 As Integer = -1
            Dim i As Integer
            For i = 0 To Me.VertexCount - 1
                Dim num5 As Single = Vector2.Dot(Me.Normals.Item(i), (Me.Vertices.Item(i) - vector))
                Dim num6 As Single = Vector2.Dot(Me.Normals.Item(i), vector3)
                If (num6 = 0!) Then
                    If (num5 < 0!) Then
                        Return SegmentCollide.Miss
                    End If
                ElseIf ((num6 < 0!) AndAlso (num5 < (num * num6))) Then
                    num = (num5 / num6)
                    num3 = i
                ElseIf ((num6 > 0!) AndAlso (num5 < (num2 * num6))) Then
                    num2 = (num5 / num6)
                End If
                If (num2 < num) Then
                    Return SegmentCollide.Miss
                End If
            Next i
            Debug.Assert(((0! <= num) AndAlso (num <= maxLambda)))
            If (num3 >= 0) Then
                lambda = num
                normal = MathUtils.Multiply(xf.RoateMatrix, Me.Normals.Item(num3))
                Return SegmentCollide.Hit
            End If
            lambda = 0!
            Return SegmentCollide.StartsInside
        End Function

    End Class
End Namespace

