Imports System.Numerics
Imports EDGameEngine.Components.Effect
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 朱利亚集
''' </summary>
Public Class GastonJulia
    Inherits GameBody
    Implements IFractal
    Public Property Vertexs As New Concurrent.ConcurrentQueue(Of Vertex） Implements IFractal.Vertexs

    Public Overrides Sub StartEx()
        Me.GameComponents.Effects.Add(New GhostEffect)
    End Sub

    Public Overrides Sub UpdateEx()
        If Vertexs.Count > 0 Then Exit Sub

        Static Radius As Double = 0.05
        Static Distance As Double = Radius * 2
        Static Squared As Double = Radius * Radius
        Static XStart As Double = -Radius
        Static YStart As Double = -Radius
        Static XUpper As Double = Radius
        Static YUpper As Double = Radius

        Static A As Double = 0.356888 '+ Rnd.NextDouble * 0.2F
        Static B As Double = -0.645411 '- Rnd.NextDouble * 0.2F
        Static C As Double = 0
        Static D As Double = 0

        Static Iteration As Integer = 880

        Dim actualWidth As Integer = CInt(Scene.Width)
        Dim actualHeight As Integer = CInt(Scene.Height)

        Dim w As Integer = CInt(Math.Min(actualWidth, actualHeight))
        Dim h As Integer = CInt(Math.Min(actualWidth, actualHeight))

        Dim wStep As Double = Distance / w
        Dim hStep As Double = Distance / h
        Dim count As Integer = 0

        Dim x, y, x0, y0, x1, y1 As Double
        For x0 = XStart To XUpper Step wStep
            For y0 = YStart To YUpper Step hStep
                x = x0
                y = y0
                C = x0 / wStep * CDbl(0.000000008)
                D = y0 / hStep * CDbl(0.000000008)
                If x0 * x0 + y0 * y0 < Squared Then
                    count += 1
                    For n = 1 To Iteration
                        x1 = x * x - y * y + A + C
                        y1 = 2 * x * y + B + D
                        x = x1
                        y = y1

                        If (x * x + y * y) > 4 OrElse n = Iteration Then
                            'Dim tempColor = Color.FromArgb(255,
                            '                               CByte((x * x * 255) * Math.Pow((n) / (Iteration * 1.2), 0.002)),
                            '                               CByte((y * y * 255) * Math.Pow((n) / (Iteration * 1.4), 0.003)),
                            '                               CByte((Math.Abs(x * y) * 255) * Math.Pow((n) / (Iteration * 1.6), 0.004)))
                            Dim tempColor = Color.FromArgb(255,
                                                           CByte(255 * Math.Pow(Math.Abs((n - 80) / (800)), 3)),
                                                           CByte(255 * Math.Sin(Math.Abs((n - 80) / (800)))),
                                                           CByte(255 * Math.Pow(Math.Abs((n - 80) / (800)), 0.5)))
                            Vertexs.Enqueue(New Vertex() With {.Position = New Vector2(CSng(actualWidth / 2 + x0 / wStep),
                                                                                       CSng(actualHeight / 2 - y0 / hStep)),
                                                              .Color = tempColor,
                                                              .Size = 1})
                            count += 1
                            Exit For
                        End If
                    Next
                End If
            Next
            If count > 5000 Then
                XStart = x0
                YStart = y0
                Exit Sub
            End If
            YStart = -1
        Next

    End Sub
End Class
