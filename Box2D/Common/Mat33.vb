Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure Mat33
        Public col1 As Vector3
        Public col2 As Vector3
        Public col3 As Vector3
        Public Sub New(ByVal c1 As Vector3, ByVal c2 As Vector3, ByVal c3 As Vector3)
            Me.col1 = c1
            Me.col2 = c2
            Me.col3 = c3
        End Sub

        Public Sub SetZero()
            Me.col1 = Vector3.Zero
            Me.col2 = Vector3.Zero
            Me.col3 = Vector3.Zero
        End Sub

        Public Function Solve33(ByVal b As Vector3) As Vector3
            Dim num As Single = Vector3.Dot(Me.col1, Vector3.Cross(Me.col2, Me.col3))
            Debug.Assert((Not num = 0!))
            num = (1! / num)
            Return New Vector3((num * Vector3.Dot(b, Vector3.Cross(Me.col2, Me.col3))), (num * Vector3.Dot(Me.col1, Vector3.Cross(b, Me.col3))), (num * Vector3.Dot(Me.col1, Vector3.Cross(Me.col2, b))))
        End Function

        Public Function Solve22(ByVal b As Vector2) As Vector2
            Dim x As Single = Me.col1.X
            Dim num2 As Single = Me.col2.X
            Dim y As Single = Me.col1.Y
            Dim num4 As Single = Me.col2.Y
            Dim num5 As Single = ((x * num4) - (num2 * y))
            Debug.Assert((Not num5 = 0!))
            num5 = (1! / num5)
            Return New Vector2((num5 * ((num4 * b.X) - (num2 * b.Y))), (num5 * ((x * b.Y) - (y * b.X))))
        End Function
    End Structure
End Namespace

