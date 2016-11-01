Imports Microsoft.Graphics.Canvas
Imports System.Numerics
Imports Windows.UI
Imports EDGameEngine.Core

Public Class AutoDrawView
    Inherits TypedGameView(Of AutoDrawModel)
    Public Sub New(Target As AutoDrawModel)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Dim SubPoint As Point
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                If Target.CurrentPoints.Count > 0 Then
                    For i = 0 To Target.CurrentPoints.Count - 1
                        SubPoint = Target.CurrentPoints(i)
                        Dl.FillCircle(SubPoint.Position, SubPoint.Size, SubPoint.Color)
                    Next
                    Target.CurrentPoints.Clear()
                End If
            End Using
            DrawingSession.DrawImage(cmdList)
        End Using
    End Sub
End Class
