Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Public Structure AABB
        Public lowerBound As Vector2
        Public upperBound As Vector2
        Public Function IsValid() As Boolean
            Dim vector As Vector2 = (Me.upperBound - Me.lowerBound)
            Return ((((vector.X >= 0!) AndAlso (vector.Y >= 0!)) AndAlso MathUtils.IsValid(Me.lowerBound)) AndAlso MathUtils.IsValid(Me.upperBound))
        End Function

        Public Function GetCenter() As Vector2
            Return (0.5! * (Me.lowerBound + Me.upperBound))
        End Function

        Public Function GetExtents() As Vector2
            Return (0.5! * (Me.upperBound - Me.lowerBound))
        End Function

        Public Sub Combine(ByRef aabb1 As AABB, ByRef aabb2 As AABB)
            Me.lowerBound = Vector2.Min(aabb1.lowerBound, aabb2.lowerBound)
            Me.upperBound = Vector2.Max(aabb1.upperBound, aabb2.upperBound)
        End Sub

        Public Function Contains(ByRef aabb As AABB) As Boolean
            Dim flag As Boolean = True
            Return ((((flag AndAlso (Me.lowerBound.X <= aabb.lowerBound.X)) AndAlso (Me.lowerBound.Y <= aabb.lowerBound.Y)) AndAlso (aabb.upperBound.X <= Me.upperBound.X)) AndAlso (aabb.upperBound.Y <= Me.upperBound.Y))
        End Function

        Public Shared Function TestOverlap(ByRef a As AABB, ByRef b As AABB) As Boolean
            Dim vector As Vector2 = (b.lowerBound - a.upperBound)
            Dim vector2 As Vector2 = (a.lowerBound - b.upperBound)
            If ((vector.X > 0!) OrElse (vector.Y > 0!)) Then
                Return False
            End If
            If ((vector2.X > 0!) OrElse (vector2.Y > 0!)) Then
                Return False
            End If
            Return True
        End Function

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
                Dim num5 As Single = If((i = 0), Me.lowerBound.X, Me.lowerBound.Y)
                Dim num6 As Single = If((i = 0), Me.upperBound.X, Me.upperBound.Y)
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

