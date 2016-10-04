Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Public Structure XForm
        Public Position As Vector2
        Public R As Mat22
        Public Sub New(ByVal position As Vector2, ByRef r As Mat22)
            Me.Position = position
            Me.R = r
        End Sub

        Public Sub SetIdentity()
            Me.Position = Vector2.Zero
            Me.R.SetIdentity()
        End Sub

        Public Sub SetValue(ByVal p As Vector2, ByVal angle As Single)
            Me.Position = p
            Me.R.SetValue(angle)
        End Sub

        Public Function GetAngle() As Single
            Return CType(Math.Atan2(CType(Me.R.col1.Y, Double), CType(Me.R.col1.X, Double)), Single)
        End Function
    End Structure
End Namespace

