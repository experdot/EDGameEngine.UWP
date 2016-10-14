Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    ''' <summary>
    ''' 3x3矩阵
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure Mat33
        ''' <summary>
        ''' 列向量1
        ''' </summary>
        Public Column1 As Vector3
        ''' <summary>
        ''' 列向量2
        ''' </summary>
        Public Column2 As Vector3
        ''' <summary>
        ''' 列向量3
        ''' </summary>
        Public Column3 As Vector3

        Public Sub New(ByVal c1 As Vector3, ByVal c2 As Vector3, ByVal c3 As Vector3)
            Me.Column1 = c1
            Me.Column2 = c2
            Me.Column3 = c3
        End Sub
        ''' <summary>
        ''' 设置当前<see cref="Mat33"/>对象为零矩阵
        ''' </summary>
        Public Sub SetZero()
            Me.Column1 = Vector3.Zero
            Me.Column2 = Vector3.Zero
            Me.Column3 = Vector3.Zero
        End Sub
        ''' <summary>
        ''' 返回当前矩阵与指定的<see cref="Vector3"/>对象相乘的结果
        ''' </summary>
        Public Function Solve33(ByVal b As Vector3) As Vector3
            Dim num As Single = Vector3.Dot(Me.Column1, Vector3.Cross(Me.Column2, Me.Column3))
            Debug.Assert((Not num = 0!))
            num = (1.0! / num)
            Return New Vector3((num * Vector3.Dot(b, Vector3.Cross(Me.Column2, Me.Column3))), (num * Vector3.Dot(Me.Column1, Vector3.Cross(b, Me.Column3))), (num * Vector3.Dot(Me.Column1, Vector3.Cross(Me.Column2, b))))
        End Function
        ''' <summary>
        '''  返回当前矩阵与指定的<see cref="Vector2"/>对象相乘的结果
        ''' </summary>
        Public Function Solve22(ByVal b As Vector2) As Vector2
            Dim x As Single = Me.Column1.X
            Dim num2 As Single = Me.Column2.X
            Dim y As Single = Me.Column1.Y
            Dim num4 As Single = Me.Column2.Y
            Dim num5 As Single = ((x * num4) - (num2 * y))
            Debug.Assert((Not num5 = 0!))
            num5 = (1.0! / num5)
            Return New Vector2((num5 * ((num4 * b.X) - (num2 * b.Y))), (num5 * ((x * b.Y) - (y * b.X))))
        End Function
    End Structure
End Namespace

