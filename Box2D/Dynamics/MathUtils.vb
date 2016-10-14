Imports System
Imports System.Numerics
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace Global.Box2D

    Public Module MathUtils
        ' Methods
        Public Function Abs(ByVal v As Vector2) As Vector2
            Return New Vector2(Math.Abs(v.X), Math.Abs(v.Y))
        End Function

        Public Function Clamp(ByVal a As Integer, ByVal low As Integer, ByVal high As Integer) As Integer
            Return Math.Max(low, Math.Min(a, high))
        End Function

        Public Function Clamp(ByVal a As Vector2, ByVal low As Vector2, ByVal high As Vector2) As Vector2
            Return Vector2.Max(low, Vector2.Min(a, high))
        End Function

        Public Function Clamp(ByVal a As Single, ByVal low As Single, ByVal high As Single) As Single
            Return Math.Max(low, Math.Min(a, high))
        End Function

        Public Function Cross(ByVal a As Vector2, ByVal b As Vector2) As Single
            Return ((a.X * b.Y) - (a.Y * b.X))
        End Function

        Public Function Cross(ByVal a As Vector2, ByVal s As Single) As Vector2
            Return New Vector2((s * a.Y), (-s * a.X))
        End Function

        Public Function Cross(ByVal s As Single, ByVal a As Vector2) As Vector2
            Return New Vector2((-s * a.Y), (s * a.X))
        End Function

        Public Function InvSqrt(ByVal x As Single) As Single
            Dim converter As New FloatConverter With {
                .x = x
            }
            Dim num As Single = (0.5! * x)
            converter.i = (&H5F3759DF - (converter.i >> 1))
            x = converter.x
            x = (x * (1.5! - ((num * x) * x)))
            Return x
        End Function

        <Extension>
        Public Function IsValid(ByVal x As Vector2) As Boolean
            Return (MathUtils.IsValid(x.X) AndAlso MathUtils.IsValid(x.Y))
        End Function

        Public Function IsValid(ByVal x As Single) As Boolean
            If Single.IsNaN(x) Then
                Return False
            End If
            Return Single.IsInfinity(x)
        End Function

        Public Function Multiply(ByRef A As Mat22, ByVal v As Vector2) As Vector2
            Return New Vector2(((A.Column1.X * v.X) + (A.Column2.X * v.Y)), ((A.Column1.Y * v.X) + (A.Column2.Y * v.Y)))
        End Function

        Public Function Multiply(ByRef T As XForm, ByVal v As Vector2) As Vector2
            Return (T.Position + MathUtils.Multiply(T.RoateMatrix, v))
        End Function

        Public Function MultiplyT(ByRef A As Mat22, ByVal v As Vector2) As Vector2
            Return New Vector2(Vector2.Dot(v, A.Column1), Vector2.Dot(v, A.Column2))
        End Function

        Public Function MultiplyT(ByRef T As XForm, ByVal v As Vector2) As Vector2
            Return MathUtils.MultiplyT(T.RoateMatrix, (v - T.Position))
        End Function

        Public Sub Swap(Of T)(ByRef a As T, ByRef b As T)
            Dim local As T = a
            a = b
            b = local
        End Sub


        ' Nested Types
        <StructLayout(LayoutKind.Explicit)>
        Friend Structure FloatConverter
            ' Fields
            <FieldOffset(0)>
            Public i As Integer
            <FieldOffset(0)>
            Public x As Single
        End Structure
    End Module
End Namespace

