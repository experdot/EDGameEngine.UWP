Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 朱利亚集
''' </summary>
Public Class GastonJulia
    Inherits GameBody
    Implements IFractal
    Public Property Vertexs As New Concurrent.ConcurrentQueue(Of Point） Implements IFractal.Vertexs

    Public Overrides Sub StartEx()
        Me.GameComponents.Effects.Add(New GhostEffect)
    End Sub

    Public Overrides Sub UpdateEx()
        If Vertexs.Count > 0 Then Exit Sub

        Static XStart As Double = -1
        Static YStart As Double = -1
        Static A As Double = -Rnd.NextDouble
        Static B As Double = Rnd.NextDouble

        Dim w As Integer = CInt(Scene.Width)
        Dim h As Integer = CInt(Scene.Height)
        Dim pStep As Double = 2 / h
        Dim tc As Integer = 0

        Dim x, y, x0, y0, x1, y1 As Double
        For x0 = XStart To 1 Step pStep
            For y0 = YStart To 1 Step pStep
                x = x0 : y = y0
                If x0 * x0 + y0 * y0 < 1 Then
                    tc += 1
                    For n = 1 To 15
                        x1 = x * x - y * y + A
                        y1 = 2 * x * y + B
                        x = x1
                        y = y1

                        If (x * x + y * y) < 1 Then
                            Dim tempColor = Color.FromArgb(CByte((1 - x * x - y * y) * 255 Mod 255),
                                                           CByte((x * x * 255 * n * 10) Mod 255),
                                                           CByte((y * y * 255 * n * 30) Mod 255),
                                                           CByte((Math.Abs(x * y) * 255 + n * 50) Mod 255))
                            Vertexs.Enqueue(New Point() With {.Position = New Vector2(CSng(w / 2 + x0 * (h / 2)),
                                                                                      CSng(h / 2 - y0 * (h / 2))),
                                                              .Color = tempColor,
                                                              .Size = CSng((16 - n) / 2)})
                            tc += 1
                        End If
                    Next
                End If
            Next
            If tc > 2000 Then
                XStart = x0
                YStart = y0
                Exit Sub
            End If
            YStart = -1
        Next

    End Sub
End Class
