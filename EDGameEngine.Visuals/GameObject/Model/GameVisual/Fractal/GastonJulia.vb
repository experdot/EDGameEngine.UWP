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
    Public Property Vertexs As New List(Of ColorVertex） Implements IFractal.Vertexs

    Public Overrides Sub StartEx()
        Me.GameComponents.Effects.Add(New GhostEffect() With {.SourceRect = New Rect(0, 0, Scene.Width, Scene.Height)})
    End Sub

    Public Overrides Sub UpdateEx()
        If Vertexs.Count > 0 Then Exit Sub

        Static xstart As Double = -1
        Static ystart As Double = -1
        Static a As Double = -Rnd.NextDouble
        Static b As Double = Rnd.NextDouble

        Dim pWidth As Integer = CInt(Scene.Width)
        Dim pHeight As Integer = CInt(Scene.Height)
        Dim pStep As Double = 2 / pHeight
        Dim tc As Integer = 0

        Dim x, y, x0, y0, x1, y1 As Double

        For x0 = xstart To 1 Step pStep
            For y0 = ystart To 1 Step pStep
                x = x0 : y = y0
                If x0 * x0 + y0 * y0 < 1 Then
                    Dim tempColor = Color.FromArgb(CByte(255 - Math.Sqrt(x0 * x0 + y0 * y0) * 200),
                                                   CByte(x0 * x0 * 200),
                                                   CByte(y0 * y0 * 200),
                                                   CByte(Math.Sqrt(x0 * x0 + y0 * y0) * 200))
                    Vertexs.Add(New ColorVertex(New Vector2(CSng(pWidth / 2 + x0 * (pHeight / 2)), CSng(pHeight / 2 - y0 * (pHeight / 2))), tempColor))
                    tc += 1
                    For n = 1 To 20
                        x1 = x * x - y * y + a
                        y1 = 2 * x * y + b
                        x = x1
                        y = y1
                        If (x * x + y * y) < 1 Then
                            tempColor = Color.FromArgb(CByte(200 + (n - 10) * 5),
                                                       CByte(x * x * 160 + n * 1),
                                                       CByte(y * y * 160 + n * 2),
                                                       CByte(Math.Sqrt(x * x + y * y) * 160 + n * 3))
                            Vertexs.Add(New ColorVertex(New Vector2(CSng(pWidth / 2 + x0 * (pHeight / 2)), CSng(pHeight / 2 - y0 * (pHeight / 2))), tempColor))
                            tc += 1
                        End If
                    Next
                End If
            Next
            If tc > 5000 Then
                xstart = x
                ystart = y
                Exit Sub
            End If
            ystart = -1
        Next
    End Sub
End Class
