Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure Sweep
        Public localCenter As Vector2
        Public c0 As Vector2
        Public c As Vector2
        Public a0 As Single
        Public a As Single
        Public t0 As Single
        Public Sub GetTransform(<Out> ByRef xf As XForm, ByVal alpha As Single)
            xf = New XForm
            xf.Position = (((1! - alpha) * Me.c0) + (alpha * Me.c))
            Dim angle As Single = (((1! - alpha) * Me.a0) + (alpha * Me.a))
            xf.R.SetValue(angle)
            xf.Position = (xf.Position - MathUtils.Multiply(xf.R, Me.localCenter))
        End Sub

        Public Sub Advance(ByVal t As Single)
            If ((Me.t0 < t) AndAlso ((1! - Me.t0) > Settings.b2_FLT_EPSILON)) Then
                Dim num As Single = ((t - Me.t0) / (1! - Me.t0))
                Me.c0 = (((1! - num) * Me.c0) + (num * Me.c))
                Me.a0 = (((1! - num) * Me.a0) + (num * Me.a))
                Me.t0 = t
            End If
        End Sub
    End Structure
End Namespace

