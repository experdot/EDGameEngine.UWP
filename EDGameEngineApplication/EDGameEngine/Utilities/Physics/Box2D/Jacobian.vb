Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure Jacobian
        Public linear1 As Vector2
        Public angular1 As Single
        Public linear2 As Vector2
        Public angular2 As Single
        Public Sub SetZero()
            Me.linear1 = Vector2.Zero
            Me.angular1 = 0!
            Me.linear2 = Vector2.Zero
            Me.angular2 = 0!
        End Sub

        Public Sub SetValue(ByVal x1 As Vector2, ByVal a1 As Single, ByVal x2 As Vector2, ByVal a2 As Single)
            Me.linear1 = x1
            Me.angular1 = a1
            Me.linear2 = x2
            Me.angular2 = a2
        End Sub

        Public Function Compute(ByVal x1 As Vector2, ByVal a1 As Single, ByVal x2 As Vector2, ByVal a2 As Single) As Single
            Return (((Vector2.Dot(Me.linear1, x1) + (Me.angular1 * a1)) + Vector2.Dot(Me.linear2, x2)) + (Me.angular2 * a2))
        End Function
    End Structure
End Namespace

