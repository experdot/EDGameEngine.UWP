Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Public Structure Mat22
        Public col1 As Vector2
        Public col2 As Vector2
        Public Sub New(ByVal c1 As Vector2, ByVal c2 As Vector2)
            Me.col1 = c1
            Me.col2 = c2
        End Sub

        Public Sub New(ByVal a11 As Single, ByVal a12 As Single, ByVal a21 As Single, ByVal a22 As Single)
            Me.col1 = New Vector2(a11, a21)
            Me.col2 = New Vector2(a12, a22)
        End Sub

        Public Sub New(ByVal angle As Single)
            Dim num As Single = CType(Math.Cos(CType(angle, Double)), Single)
            Dim num2 As Single = CType(Math.Sin(CType(angle, Double)), Single)
            Me.col1 = New Vector2(num, num2)
            Me.col2 = New Vector2(-num2, num)
        End Sub

        Public Sub SetValue(ByVal c1 As Vector2, ByVal c2 As Vector2)
            Me.col1 = c1
            Me.col2 = c2
        End Sub

        Public Sub SetValue(ByVal angle As Single)
            Dim num As Single = CType(Math.Cos(CType(angle, Double)), Single)
            Dim num2 As Single = CType(Math.Sin(CType(angle, Double)), Single)
            Me.col1.X = num
            Me.col2.X = -num2
            Me.col1.Y = num2
            Me.col2.Y = num
        End Sub

        Public Sub SetIdentity()
            Me.col1.X = 1.0!
            Me.col2.X = 0!
            Me.col1.Y = 0!
            Me.col2.Y = 1.0!
        End Sub

        Public Sub SetZero()
            Me.col1.X = 0!
            Me.col2.X = 0!
            Me.col1.Y = 0!
            Me.col2.Y = 0!
        End Sub

        Public Function GetAngle() As Single
            Return CType(Math.Atan2(CType(Me.col1.Y, Double), CType(Me.col1.X, Double)), Single)
        End Function

        Public Function GetInverse() As Mat22
            Dim x As Single = Me.col1.X
            Dim num2 As Single = Me.col2.X
            Dim y As Single = Me.col1.Y
            Dim num4 As Single = Me.col2.Y
            Dim num5 As Single = ((x * num4) - (num2 * y))
            Debug.Assert((Not num5 = 0!))
            num5 = (1.0! / num5)
            Return New Mat22(New Vector2((num5 * num4), (-num5 * y)), New Vector2((-num5 * num2), (num5 * x)))
        End Function

        Public Function Solve(ByVal b As Vector2) As Vector2
            Dim x As Single = Me.col1.X
            Dim num2 As Single = Me.col2.X
            Dim y As Single = Me.col1.Y
            Dim num4 As Single = Me.col2.Y
            Dim num5 As Single = ((x * num4) - (num2 * y))
            Debug.Assert((Not num5 = 0!))
            num5 = (1.0! / num5)
            Return New Vector2((num5 * ((num4 * b.X) - (num2 * b.Y))), (num5 * ((x * b.Y) - (y * b.X))))
        End Function

        Public Shared Sub Add(ByRef A As Mat22, ByRef B As Mat22, <Out> ByRef R As Mat22)
            R = New Mat22((A.col1 + B.col1), (A.col2 + B.col2))
        End Sub
    End Structure
End Namespace

