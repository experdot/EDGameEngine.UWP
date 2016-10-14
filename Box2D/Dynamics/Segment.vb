
Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    ''' <summary>
    ''' 用于切割<see cref="Shape"/>对象的割片
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure Segment
        ''' <summary>
        ''' 切割点1
        ''' </summary>
        Public Property Point1 As Vector2
        ''' <summary>
        ''' 切割点2
        ''' </summary>
        Public Property Point2 As Vector2
        Public Function TestSegment(<Out> ByRef lambda As Single, <Out> ByRef normal As Vector2, ByRef segment As Segment, ByVal maxLambda As Single) As Boolean
            lambda = 0!
            normal = Numerics.Vector2.Zero
            Dim vector As Vector2 = segment.Point1
            Dim vector2 As Vector2 = (segment.Point2 - vector)
            Dim a As Vector2 = (Me.Point2 - Me.Point1)
            Dim vec As Vector2 = MathUtils.Cross(a, CSng(1.0!))
            Dim num As Single = (100.0! * Settings.b2_FLT_EPSILON)
            Dim num2 As Single = -Vector2.Dot(vector2, vec)
            If (num2 > num) Then
                Dim vector5 As Vector2 = (vector - Me.Point1)
                Dim num3 As Single = Vector2.Dot(vector5, vec)
                If ((0! <= num3) AndAlso (num3 <= (maxLambda * num2))) Then
                    Dim num4 As Single = ((-vector2.X * vector5.Y) + (vector2.Y * vector5.X))
                    If (((-num * num2) <= num4) AndAlso (num4 <= (num2 * (1.0! + num)))) Then
                        num3 = (num3 / num2)
                        Extension.Normalize(vec)
                        lambda = num3
                        normal = vec
                        Return True
                    End If
                End If
            End If
            Return False
        End Function
    End Structure
End Namespace

