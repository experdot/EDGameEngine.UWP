Imports System.Numerics
Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class LineView
    Inherits TypedGameView(Of VisualLine)
    Public Sub New(Target As VisualLine)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                DrawLine(Dl)
            End Using
            DrawingSession.DrawImage(cmdList)
        End Using
    End Sub
    Public Sub DrawLine(DS As CanvasDrawingSession)
        If Target.Points.Count > 1 Then
            For i = 0 To Target.Points.Count - 2
                DS.DrawLine(Target.Points(i), Target.Points(i + 1), Colors.White)
            Next
        End If
    End Sub
End Class
