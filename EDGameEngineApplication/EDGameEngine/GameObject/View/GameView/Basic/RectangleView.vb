Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class RectangleView
    Inherits TypedGameView(Of VisualRectangle)
    Public Sub New(Target As VisualRectangle)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                DrawRectangle(Dl)
            End Using
            DrawingSession.DrawImage(cmdList)
        End Using
    End Sub
    Public Sub DrawRectangle(DS As CanvasDrawingSession)
        DS.FillRectangle(Target.Rect, Colors.Black)
        DS.FillRectangle(New Rect(10, 10, 10, 10), Colors.Black)
        DS.FillRectangle(New Rect(10, 10, 20, 10), Colors.Black)
        DS.FillRectangle(New Rect(30, 30, 10, 20), Colors.Black)
        DS.FillRectangle(New Rect(50, 60, 10, 10), Colors.Black)
        DS.FillRectangle(New Rect(130, 120, 50, 50), Colors.Black)
        DS.FillRectangle(New Rect(400, 400, 10, 10), Colors.Black)
        DS.FillCircle(New Vector2(50, 320), 20, Colors.Black)
    End Sub
End Class