Imports System
Imports System.Diagnostics

Namespace Global.Box2D
    Public Class TimeOfImpact
        
        ' Methods
        Public Shared Function CalculateTimeOfImpact(ByRef input As TOIInput, ByVal shapeA As Shape, ByVal shapeB As Shape) As Single
            Dim cache As SimplexCache
            Dim input2 As DistanceInput
            Dim form As XForm
            Dim form2 As XForm
            Dim output As DistanceOutput
            TimeOfImpact.b2_toiCalls += 1
            Dim sweepA As Sweep = input.sweepA
            Dim sweepB As Sweep = input.sweepB
            Debug.Assert((sweepA.t0 = sweepB.t0))
            Debug.Assert(((1.0! - sweepA.t0) > Settings.b2_FLT_EPSILON))
            Dim num As Single = (shapeA.Radius + shapeB.Radius)
            Dim tolerance As Single = input.tolerance
            Dim alpha As Single = 0!
            Dim num4 As Integer = &H3E8
            Dim num5 As Integer = 0
            Dim num6 As Single = 0!
            input2.useRadii = False
Label_0082:
            sweepA.GetTransform(form, alpha)
            sweepB.GetTransform(form2, alpha)
            input2.TransformA = form
            input2.TransformB = form2
            Distance.ComputeDistance(output, cache, input2, shapeA, shapeB)
            If (output.Distance <= 0!) Then
                alpha = 1.0!
            Else
                Dim [function] As New SeparationFunction(cache, shapeA, form, shapeB, form2)
                Dim num7 As Single = [function].Evaluate(form, form2)
                If (num7 <= 0!) Then
                    alpha = 1.0!
                Else
                    If (num5 = 0) Then
                        If (num7 > num) Then
                            num6 = Math.Max((num - tolerance), (0.75! * num))
                        Else
                            num6 = Math.Max((num7 - tolerance), (0.02! * num))
                        End If
                    End If
                    If ((num7 - num6) < (0.5! * tolerance)) Then
                        If (num5 = 0) Then
                            alpha = 1.0!
                        End If
                    Else
                        Dim num8 As Single = alpha
                        Dim num9 As Single = alpha
                        Dim num10 As Single = 1.0!
                        Dim num11 As Single = num7
                        sweepA.GetTransform(form, num10)
                        sweepB.GetTransform(form2, num10)
                        Dim num12 As Single = [function].Evaluate(form, form2)
                        If (num12 >= num6) Then
                            alpha = 1.0!
                        Else
                            Dim num13 As Integer = 0
                            Do
                                Dim num14 As Single
                                If ((num13 And 1) > 0) Then
                                    num14 = (num9 + (((num6 - num11) * (num10 - num9)) / (num12 - num11)))
                                Else
                                    num14 = (0.5! * (num9 + num10))
                                End If
                                sweepA.GetTransform(form, num14)
                                sweepB.GetTransform(form2, num14)
                                Dim num15 As Single = [function].Evaluate(form, form2)
                                If (Math.Abs((num15 - num6)) < (0.025! * tolerance)) Then
                                    num8 = num14
                                    Exit Do
                                End If
                                If (num15 > num6) Then
                                    num9 = num14
                                    num11 = num15
                                Else
                                    num10 = num14
                                    num12 = num15
                                End If
                                num13 += 1
                                TimeOfImpact.b2_toiRootIters += 1
                            Loop While (Not num13 = 50)
                            TimeOfImpact.b2_toiMaxRootIters = Math.Max(TimeOfImpact.b2_toiMaxRootIters, num13)
                            If (num8 >= ((1.0! + (100.0! * Settings.b2_FLT_EPSILON)) * alpha)) Then
                                alpha = num8
                                num5 += 1
                                TimeOfImpact.b2_toiIters += 1
                                If (num5 <> num4) Then
                                    GoTo Label_0082
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            TimeOfImpact.b2_toiMaxIters = Math.Max(TimeOfImpact.b2_toiMaxIters, num5)
            Return alpha
        End Function


        ' Fields
        Private Shared b2_toiCalls As Integer
        Private Shared b2_toiIters As Integer
        Private Shared b2_toiMaxIters As Integer
        Private Shared b2_toiMaxRootIters As Integer
        Private Shared b2_toiRootIters As Integer
    End Class
End Namespace

