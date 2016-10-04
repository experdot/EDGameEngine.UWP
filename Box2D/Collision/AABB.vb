Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    ''' <summary>
    ''' AABB包围盒
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure AABB
        ''' <summary>
        ''' 下边界
        ''' </summary>
        Public Property LowerBound As Vector2
        ''' <summary>
        ''' 上边界
        ''' </summary>
        Public Property UpperBound As Vector2
        ''' <summary>
        ''' 是否有效
        ''' </summary>
        Public ReadOnly Property IsValid As Boolean
            Get
                Dim vector As Vector2 = (Me.UpperBound - Me.LowerBound)
                Return ((((vector.X >= 0!) AndAlso (vector.Y >= 0!)) AndAlso MathUtils.IsValid(Me.LowerBound)) AndAlso MathUtils.IsValid(Me.UpperBound))
            End Get
        End Property
        ''' <summary>
        ''' 中心
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Center As Vector2
            Get
                Return (0.5! * (Me.LowerBound + Me.UpperBound))
            End Get
        End Property
        ''' <summary>
        ''' 区间
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Extents As Vector2
            Get
                Return (0.5! * (Me.UpperBound - Me.LowerBound))
            End Get
        End Property
        ''' <summary>
        ''' 合并两个<see cref="AABB"/>,并将合并结果设置为当前的<see cref="AABB"/>对象
        ''' </summary>
        Public Sub Combine(ByRef aabb1 As AABB, ByRef aabb2 As AABB)
            Me.LowerBound = Vector2.Min(aabb1.LowerBound, aabb2.LowerBound)
            Me.UpperBound = Vector2.Max(aabb1.UpperBound, aabb2.UpperBound)
        End Sub
        ''' <summary>
        ''' 返回当前的<see cref="AABB"/>对象是否包含指定的<see cref="AABB"/>
        ''' </summary>
        Public Function Contains(ByRef aabb As AABB) As Boolean
            Dim flag As Boolean = True
            Return ((((flag AndAlso (Me.LowerBound.X <= aabb.LowerBound.X)) AndAlso (Me.LowerBound.Y <= aabb.LowerBound.Y)) AndAlso (aabb.UpperBound.X <= Me.UpperBound.X)) AndAlso (aabb.UpperBound.Y <= Me.UpperBound.Y))
        End Function
        ''' <summary>
        ''' 返回指定的两个<see cref="AABB"/>对象是否重叠
        ''' </summary>
        Public Shared Function TestOverlap(ByRef a As AABB, ByRef b As AABB) As Boolean
            Dim vector As Vector2 = (b.LowerBound - a.UpperBound)
            Dim vector2 As Vector2 = (a.LowerBound - b.UpperBound)
            If ((vector.X > 0!) OrElse (vector.Y > 0!)) Then
                Return False
            End If
            If ((vector2.X > 0!) OrElse (vector2.Y > 0!)) Then
                Return False
            End If
            Return True
        End Function
        ''' <summary>
        ''' 光线投射
        ''' </summary>
        Public Sub RayCast(<Out> ByRef output As RayCastOutput, ByRef input As RayCastInput)
            output = New RayCastOutput
            Dim num As Single = -Settings.b2_FLT_MAX
            Dim num2 As Single = Settings.b2_FLT_MAX
            output.hit = False
            Dim vector As Vector2 = input.p1
            Dim v As Vector2 = (input.p2 - input.p1)
            Dim vector3 As Vector2 = MathUtils.Abs(v)
            Dim vector4 As Vector2 = Vector2.Zero
            Dim i As Integer
            For i = 0 To 2 - 1
                Dim num4 As Single = If((i = 0), vector3.X, vector3.Y)
                Dim num5 As Single = If((i = 0), Me.LowerBound.X, Me.LowerBound.Y)
                Dim num6 As Single = If((i = 0), Me.UpperBound.X, Me.UpperBound.Y)
                Dim num7 As Single = If((i = 0), vector.X, vector.Y)
                If (num4 < Settings.b2_FLT_EPSILON) Then
                    If ((num7 < num5) OrElse (num6 < num7)) Then
                        Return
                    End If
                Else
                    Dim num8 As Single = If((i = 0), v.X, v.Y)
                    Dim num9 As Single = (1.0! / num8)
                    Dim a As Single = ((num5 - num7) * num9)
                    Dim b As Single = ((num6 - num7) * num9)
                    Dim num12 As Single = -1.0!
                    If (a > b) Then
                        MathUtils.Swap(Of Single)(a, b)
                        num12 = 1.0!
                    End If
                    If (a > num) Then
                        If (i = 0) Then
                            vector4.X = num12
                        Else
                            vector4.Y = num12
                        End If
                        num = a
                    End If
                    num2 = Math.Min(num2, b)
                    If (num > num2) Then
                        Return
                    End If
                End If
            Next i
            If ((num >= 0!) AndAlso (input.maxFraction >= num)) Then
                output.fraction = num
                output.normal = vector4
                output.hit = True
            End If
        End Sub
    End Structure
End Namespace

