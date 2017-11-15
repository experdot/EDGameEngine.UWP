Imports System.Numerics
Imports EDGameEngine.Components.Effect
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 曼德布罗集
''' </summary>
Public Class Mandelbrot
    Inherits GameBody
    Implements IFractal
    Public Property Vertexs As New Concurrent.ConcurrentQueue(Of Vertex） Implements IFractal.Vertexs

    Public Overrides Sub StartEx()
        Me.GameComponents.Effects.Add(New GhostEffect)
    End Sub

    Public Overrides Sub UpdateEx()
        If Vertexs.Count > 0 Then Exit Sub

        Static astart As Double = -2
        Static bstart As Double = -2

        Dim a, b, x, y, x1, y1 As Double
        Dim tc As Integer = 0
        For a = astart To 2 Step 0.003
            For b = bstart To 2 Step 0.003
                x = a
                y = b
                For n = 1 To 20
                    x1 = x * x - y * y + a
                    y1 = 2 * x * y + b
                    x = x1
                    y = y1
                Next
                If x * x + y * y < 4 Then
                    Dim tempcolor As Color = Color.FromArgb(255, CByte(x * x * 63), CByte(x * x * 63), CByte((x * x + y * y) * 63))
                    Vertexs.Enqueue(New Vertex() With {.Position = New Vector2(CSng(Scene.Width / 2 + a * (Scene.Height / 3)),
                                                                              CSng(Scene.Height / 2 - b * (Scene.Height / 3))),
                                                       .Color = tempcolor})
                End If
                tc += 1
                bstart = -2
            Next

            If tc > 1000 Then
                astart = a
                bstart = b
                Exit Sub
            End If
            bstart = -2
        Next
    End Sub
End Class
