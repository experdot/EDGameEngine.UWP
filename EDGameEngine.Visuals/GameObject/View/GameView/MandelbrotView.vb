Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class MandelbrotView
    Inherits TypedGameView(Of Mandelbrot)
    Public Sub New(Target As Mandelbrot)
        MyBase.New(Target)
    End Sub

    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        If Target.Vertexs.Count > 0 Then
            For Each SubVertex In Target.Vertexs
                DrawingSession.DrawRectangle(SubVertex.Position.X, SubVertex.Position.Y, 1, 1, SubVertex.Color)
            Next
            Target.Vertexs.Clear()
        End If
        'Static a(,) As Double = {{0, 0, 0, 0, 0, 0, 0, 0},
        '                      {0, 0.195, -0.488, 0.344, 0.433, 0.4431, 0.2452, 0.25},
        '                      {0, 0.462, 0.414, -0.252, 0.361, 0.2511, 0.5692, 0.25},
        '                      {0, -0.058, -0.07, 0.453, -0.111, 0.5976, 0.0969, 0.25},
        '                      {0, -0.035, 0.07, -0.469, -0.022, 0.4884, 0.5069, 0.2},
        '                      {0, -0.637, 0, 0, 0.501, 0.8562, 0.2513, 0.05}}

        'Static x0 As Double = 1, y0 As Double = 1, x1 As Double = 0, y1 As Double = 0
        'Static iStart As Integer = 1
        'Dim count As Integer = 0
        'For i = iStart To 10000
        '    DrawFac(DrawingSession, a, x0, y0, x1, y1)
        '    count += 1
        '    If count > 100 Then '每帧循环100次
        '        'iStart = i + 1
        '        'Exit For
        '    End If
        'Next
    End Sub

    Private Sub DrawFac(DrawingSession As CanvasDrawingSession, a(,) As Double, ByRef x0 As Double, ByRef y0 As Double, ByRef x1 As Double, ByRef y1 As Double)
        Dim r = GameBody.Rnd.NextDouble
        If r <= 0.25 Then
            x1 = a(1, 1) * x0 + a(1, 2) * y0 + a(1, 5)
            y1 = a(1, 3) * x0 + a(1, 4) * y0 + a(1, 6)
        ElseIf r > 0.25 And r <= 0.5 Then
            x1 = a(2, 1) * x0 + a(2, 2) * y0 + a(2, 5)
            y1 = a(2, 3) * x0 + a(2, 4) * y0 + a(2, 6)
        ElseIf r > 0.5 And r <= 0.75 Then
            x1 = a(3, 1) * x0 + a(3, 2) * y0 + a(3, 5)
            y1 = a(3, 3) * x0 + a(3, 4) * y0 + a(3, 6)
        ElseIf r > 0.75 And r <= 0.95 Then
            x1 = a(4, 1) * x0 + a(4, 2) * y0 + a(4, 5)
            y1 = a(4, 3) * x0 + a(4, 4) * y0 + a(4, 6)
        ElseIf r > 0.95 And r <= 1 Then
            x1 = a(5, 1) * x0 + a(5, 2) * y0 + a(5, 5)
            y1 = a(5, 3) * x0 + a(5, 4) * y0 + a(5, 6)
        End If
        x0 = x1 : y0 = y1
        Dim tempCol As Color = Color.FromArgb(255, CByte(x0 * x0 * 200 * r), CByte(y0 * y0 * 200 * r), CByte(x0 * y0 * 255))
        Dim tempVer = New ColorVertex(
                            New Vector2(CSng(Target.Scene.Width * 0.75 - x1 * Target.Scene.Height), CSng(Target.Scene.Height - y1 * Target.Scene.Height)）,
                            tempCol)
        DrawingSession.DrawRectangle(tempVer.Position.X, tempVer.Position.Y, 1, 1, tempVer.Color)
    End Sub
End Class
