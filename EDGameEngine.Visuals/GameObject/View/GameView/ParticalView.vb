Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class ParticalView
    Inherits TypedGameView(Of ParticalsBase)
    Public Sub New(Target As ParticalsBase)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        DrawPartical(DrawingSession)
    End Sub
    Private Sub DrawPartical(DS As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DS)
            Using Dl = cmdList.CreateDrawingSession
                For i = 0 To Target.Particals.Count - 1
                    Dl.FillCircle(Target.Particals(i).Location, Target.Particals(i).Size, Target.Particals(i).Color)
                    'Dim border As Single = Target.Particals(i).ImageSize * 5 + Target.Particals(i).Age * 2
                    'Dl.DrawImage(DirectCast(Target.Scene, Scene).ImageManager.GetResource(ImageResourceID.SmokePartial1),
                    '             New Rect(Target.Particals(i).Location.X - border, Target.Particals(i).Location.Y - border, border * 2, border * 2),
                    '             New Rect(0, 0, 192, 192), 0.15)
                Next
            End Using
            Using blur1 = New Effects.GaussianBlurEffect() With {.Source = cmdList, .BlurAmount = 3}
                'DS.DrawImage(blur1)
                DS.DrawImage(cmdList)
            End Using
        End Using
    End Sub
End Class
