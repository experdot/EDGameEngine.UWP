Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Friend Class LineView
    Inherits TypedGameView(Of VisualLine)
    Public Sub New(Target As VisualLine)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession.Device)
            Using Dl = cmdList.CreateDrawingSession
                DrawLine(Dl)
            End Using
            Using blur1 = New Effects.GaussianBlurEffect() With {.Source = cmdList, .BlurAmount = 3}
                DrawingSession.DrawImage(blur1)
                DrawingSession.DrawImage(cmdList)
            End Using
        End Using
    End Sub

    Public Sub DrawLine(DS As CanvasDrawingSession)
        If Target.Points.Count > 1 Then
            For i = 0 To Target.Points.Count - 2
                DS.DrawLine(Target.Points(0), Target.Points(1), Colors.Black)
            Next
        End If
    End Sub
End Class
