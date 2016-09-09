
Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.InteropServices
Imports Box2D.UWPExtensions

Namespace Global.Box2D
    <StructLayout(LayoutKind.Sequential)>
    Public Structure SeparationFunction
        Private _shapeA As Shape
        Private _shapeB As Shape
        Private _type As SeparationFunctionType
        Private _localPoint As Vector2
        Private _axis As Vector2
        Public Sub New(ByRef cache As SimplexCache, ByVal shapeA As Shape, ByRef transformA As XForm, ByVal shapeB As Shape, ByRef transformB As XForm)
            Me._localPoint = Vector2.Zero
            Me._shapeA = shapeA
            Me._shapeB = shapeB
            Dim count As Integer = cache.count
            Debug.Assert(((0 < count) AndAlso (count < 3)))
            If (count = 1) Then
                Me._type = SeparationFunctionType.Points
                Dim vertex As Vector2 = Me._shapeA.GetVertex(cache.indexA.Item(0))
                Dim v As Vector2 = Me._shapeB.GetVertex(cache.indexB.Item(0))
                Dim vector3 As Vector2 = MathUtils.Multiply(transformA, vertex)
                Dim vector4 As Vector2 = MathUtils.Multiply(transformB, v)
                Me._axis = (vector4 - vector3)
                Extension.Normalize(Me._axis)
            ElseIf (cache.indexB.Item(0) = cache.indexB.Item(1)) Then
                Me._type = SeparationFunctionType.FaceA
                Dim vector5 As Vector2 = Me._shapeA.GetVertex(cache.indexA.Item(0))
                Dim vector6 As Vector2 = Me._shapeA.GetVertex(cache.indexA.Item(1))
                Dim vector7 As Vector2 = Me._shapeB.GetVertex(cache.indexB.Item(0))
                Me._localPoint = (0.5! * (vector5 + vector6))
                Me._axis = MathUtils.Cross((vector6 - vector5), CSng(1.0!))
                Extension.Normalize(Me._axis)
                Dim vector8 As Vector2 = MathUtils.Multiply(transformA.R, Me._axis)
                Dim vector9 As Vector2 = MathUtils.Multiply(transformA, Me._localPoint)
                If (Vector2.Dot((MathUtils.Multiply(transformB, vector7) - vector9), vector8) < 0!) Then
                    Me._axis = -Me._axis
                End If
            ElseIf (cache.indexA.Item(0) = cache.indexA.Item(1)) Then
                Me._type = SeparationFunctionType.FaceB
                Dim vector11 As Vector2 = shapeA.GetVertex(cache.indexA.Item(0))
                Dim vector12 As Vector2 = shapeB.GetVertex(cache.indexB.Item(0))
                Dim vector13 As Vector2 = shapeB.GetVertex(cache.indexB.Item(1))
                Me._localPoint = (0.5! * (vector12 + vector13))
                Me._axis = MathUtils.Cross((vector13 - vector12), CSng(1.0!))
                Extension.Normalize(Me._axis)
                Dim vector14 As Vector2 = MathUtils.Multiply(transformB.R, Me._axis)
                Dim vector15 As Vector2 = MathUtils.Multiply(transformB, Me._localPoint)
                If (Vector2.Dot((MathUtils.Multiply(transformA, vector11) - vector15), vector14) < 0!) Then
                    Me._axis = -Me._axis
                End If
            Else
                Dim vector17 As Vector2 = Me._shapeA.GetVertex(cache.indexA.Item(0))
                Dim vector18 As Vector2 = Me._shapeA.GetVertex(cache.indexA.Item(1))
                Dim vector19 As Vector2 = Me._shapeB.GetVertex(cache.indexB.Item(0))
                Dim vector20 As Vector2 = Me._shapeB.GetVertex(cache.indexB.Item(1))
                Dim vector21 As Vector2 = MathUtils.Multiply(transformA, vector17)
                Dim vector22 As Vector2 = MathUtils.Multiply(transformA.R, (vector18 - vector17))
                Dim vector23 As Vector2 = MathUtils.Multiply(transformB, vector19)
                Dim vector24 As Vector2 = MathUtils.Multiply(transformB.R, (vector20 - vector19))
                Dim num4 As Single = Vector2.Dot(vector22, vector22)
                Dim num5 As Single = Vector2.Dot(vector24, vector24)
                Dim vector25 As Vector2 = (vector21 - vector23)
                Dim num6 As Single = Vector2.Dot(vector22, vector25)
                Dim num7 As Single = Vector2.Dot(vector24, vector25)
                Dim num8 As Single = Vector2.Dot(vector22, vector24)
                Dim num9 As Single = ((num4 * num5) - (num8 * num8))
                Dim num10 As Single = 0!
                If (Not num9 = 0!) Then
                    num10 = MathUtils.Clamp(CSng((((num8 * num7) - (num6 * num5)) / num9)), CSng(0!), CSng(1.0!))
                End If
                Dim num11 As Single = (((num8 * num10) + num7) / num5)
                If (num11 < 0!) Then
                    num11 = 0!
                    num10 = MathUtils.Clamp(CSng((-num6 / num4)), CSng(0!), CSng(1.0!))
                ElseIf (num11 > 1.0!) Then
                    num11 = 1.0!
                    num10 = MathUtils.Clamp(CSng(((num8 - num6) / num4)), CSng(0!), CSng(1.0!))
                End If
                Dim vector26 As Vector2 = (vector17 + (num10 * (vector18 - vector17)))
                Dim vector27 As Vector2 = (vector19 + (num11 * (vector20 - vector19)))
                Select Case num10
                    Case 0!, 1.0!
                        Me._type = SeparationFunctionType.FaceB
                        Me._axis = MathUtils.Cross((vector20 - vector19), CSng(1.0!))
                        Extension.Normalize(Me._axis)
                        Me._localPoint = vector27
                        Dim vector28 As Vector2 = MathUtils.Multiply(transformB.R, Me._axis)
                        Dim vector29 As Vector2 = MathUtils.Multiply(transformA, vector26)
                        Dim vector30 As Vector2 = MathUtils.Multiply(transformB, vector27)
                        If (Vector2.Dot((vector29 - vector30), vector28) < 0!) Then
                            Me._axis = -Me._axis
                        End If
                        Return
                End Select
                Me._type = SeparationFunctionType.FaceA
                Me._axis = MathUtils.Cross((vector18 - vector17), CSng(1.0!))
                Extension.Normalize(Me._axis)
                Me._localPoint = vector26
                Dim vector31 As Vector2 = MathUtils.Multiply(transformA.R, Me._axis)
                Dim vector32 As Vector2 = MathUtils.Multiply(transformA, vector26)
                If (Vector2.Dot((MathUtils.Multiply(transformB, vector27) - vector32), vector31) < 0!) Then
                    Me._axis = -Me._axis
                End If
            End If
        End Sub

        Public Function Evaluate(ByRef transformA As XForm, ByRef transformB As XForm) As Single
            Select Case Me._type
                Case SeparationFunctionType.Points
                    Dim d As Vector2 = MathUtils.MultiplyT(transformA.R, Me._axis)
                    Dim vector2 As Vector2 = MathUtils.MultiplyT(transformB.R, -Me._axis)
                    Dim supportVertex As Vector2 = Me._shapeA.GetSupportVertex(d)
                    Dim v As Vector2 = Me._shapeB.GetSupportVertex(vector2)
                    Dim vector5 As Vector2 = MathUtils.Multiply(transformA, supportVertex)
                    Return Vector2.Dot((MathUtils.Multiply(transformB, v) - vector5), Me._axis)
                Case SeparationFunctionType.FaceA
                    Dim vector7 As Vector2 = MathUtils.Multiply(transformA.R, Me._axis)
                    Dim vector8 As Vector2 = MathUtils.Multiply(transformA, Me._localPoint)
                    Dim vector9 As Vector2 = MathUtils.MultiplyT(transformB.R, -vector7)
                    Dim vector10 As Vector2 = Me._shapeB.GetSupportVertex(vector9)
                    Return Vector2.Dot((MathUtils.Multiply(transformB, vector10) - vector8), vector7)
                Case SeparationFunctionType.FaceB
                    Dim vector12 As Vector2 = MathUtils.Multiply(transformB.R, Me._axis)
                    Dim vector13 As Vector2 = MathUtils.Multiply(transformB, Me._localPoint)
                    Dim vector14 As Vector2 = MathUtils.MultiplyT(transformA.R, -vector12)
                    Dim vector15 As Vector2 = Me._shapeA.GetSupportVertex(vector14)
                    Return Vector2.Dot((MathUtils.Multiply(transformA, vector15) - vector13), vector12)
            End Select
            Debug.Assert(False)
            Return 0!
        End Function
    End Structure
End Namespace

