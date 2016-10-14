Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    ''' <summary>
    ''' 2x2矩阵
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure Mat22
        ''' <summary>
        ''' 列向量1
        ''' </summary>
        Public Column1 As Vector2
        ''' <summary>
        ''' 列向量2
        ''' </summary>
        Public Column2 As Vector2

        Public Sub New(ByVal c1 As Vector2, ByVal c2 As Vector2)
            Me.Column1 = c1
            Me.Column2 = c2
        End Sub
        Public Sub New(ByVal a11 As Single, ByVal a12 As Single, ByVal a21 As Single, ByVal a22 As Single)
            Me.Column1 = New Vector2(a11, a21)
            Me.Column2 = New Vector2(a12, a22)
        End Sub

        Public Sub New(ByVal angle As Single)
            Dim num As Single = CType(Math.Cos(CType(angle, Double)), Single)
            Dim num2 As Single = CType(Math.Sin(CType(angle, Double)), Single)
            Me.Column1 = New Vector2(num, num2)
            Me.Column2 = New Vector2(-num2, num)
        End Sub
        ''' <summary>
        ''' 从指定的两个列向量初始化当前<see cref="Mat22"/>对象
        ''' </summary>
        Public Sub CreateFromVectors(ByVal c1 As Vector2, ByVal c2 As Vector2)
            Me.Column1 = c1
            Me.Column2 = c2
        End Sub
        ''' <summary>
        ''' 从指定的旋转角度初始化当前<see cref="Mat22"/>对象
        ''' </summary>
        Public Sub CreateFromAngle(ByVal angle As Single)
            Dim num As Single = CType(Math.Cos(CType(angle, Double)), Single)
            Dim num2 As Single = CType(Math.Sin(CType(angle, Double)), Single)
            Column1.X = num
            Me.Column2.X = -num2
            Me.Column1.Y = num2
            Me.Column2.Y = num
        End Sub
        ''' <summary>
        ''' 设置当前<see cref="Mat22"/>对象的为标准矩阵
        ''' </summary>
        Public Sub SetIdentity()
            Me.Column1.X = 1.0!
            Me.Column2.X = 0!
            Me.Column1.Y = 0!
            Me.Column2.Y = 1.0!
        End Sub
        ''' <summary>
        ''' 设置当前<see cref="Mat22"/>对象为零矩阵
        ''' </summary>
        Public Sub SetZero()
            Me.Column1.X = 0!
            Me.Column2.X = 0!
            Me.Column1.Y = 0!
            Me.Column2.Y = 0!

        End Sub
        ''' <summary>
        ''' 返回当前<see cref="Mat22"/>对象的旋转角度
        ''' </summary>
        ''' <returns></returns>
        Public Function GetAngle() As Single
            Return CType(Math.Atan2(CType(Me.Column1.Y, Double), CType(Me.Column1.X, Double)), Single)
        End Function
        ''' <summary>
        ''' 返回当前<see cref="Mat22"/>对象的逆矩阵
        ''' </summary>
        Public Function GetInverse() As Mat22
            Dim x As Single = Me.Column1.X
            Dim num2 As Single = Me.Column2.X
            Dim y As Single = Me.Column1.Y
            Dim num4 As Single = Me.Column2.Y
            Dim num5 As Single = ((x * num4) - (num2 * y))
            Debug.Assert((Not num5 = 0!))
            num5 = (1.0! / num5)
            Return New Mat22(New Vector2((num5 * num4), (-num5 * y)), New Vector2((-num5 * num2), (num5 * x)))
        End Function
        ''' <summary>
        '''  返回当前矩阵与指定的<see cref="Vector2"/>对象相乘的结果
        ''' </summary>
        Public Function Solve(ByVal b As Vector2) As Vector2
            Dim x As Single = Me.Column1.X
            Dim num2 As Single = Me.Column2.X
            Dim y As Single = Me.Column1.Y
            Dim num4 As Single = Me.Column2.Y
            Dim num5 As Single = ((x * num4) - (num2 * y))
            Debug.Assert((Not num5 = 0!))
            num5 = (1.0! / num5)
            Return New Vector2((num5 * ((num4 * b.X) - (num2 * b.Y))), (num5 * ((x * b.Y) - (y * b.X))))
        End Function
        ''' <summary>
        '''  返回两个<see cref="Vector2"/>对象相加的结果
        ''' </summary>
        Public Shared Sub Add(ByRef A As Mat22, ByRef B As Mat22, <Out> ByRef Result As Mat22)
            Result = New Mat22((A.Column1 + B.Column1), (A.Column2 + B.Column2))
        End Sub
    End Structure
End Namespace

