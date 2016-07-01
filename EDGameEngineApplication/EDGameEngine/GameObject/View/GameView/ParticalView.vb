Imports System.Numerics
Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Public Class ParticalView
    Inherits TypedGameView(Of ParticalManager)
    Public Sub New(Target As ParticalManager)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession.Device)
            Using Dl = cmdList.CreateDrawingSession
                For i = 0 To Target.Particals.Count - 1
                    Dl.FillCircle(Target.Particals(i).Location, Target.Particals(i).Size, Target.Particals(i).Color)
                    'For Each SubPoint In Target.Particals(i).Points
                    '    Dim index = Target.Particals(i).Points.IndexOf(SubPoint)
                    '    Dim count = Target.Particals(i).Points.Count
                    '    Dl.FillCircle(SubPoint, Target.Particals(i).Size * index / count, Target.Particals(i).Color)
                    'Next
                    'Dim border As Single = Target.Particals(i).ImageSize * 50 + Target.Particals(i).Age * 2
                    'Dl.DrawImage(DirectCast(Target.Scene, Scene).ImageManager.GetResource(ImageResourceID.SmokePartial1),
                    '             New Rect(Target.Particals(i).Location.X - border, Target.Particals(i).Location.Y - border, border * 2, border * 2),
                    '             New Rect(0, 0, 192, 192), 0.15)
                Next
            End Using
            Dim es = Effector.Transform2D(cmdList, Target.Transform)
            Using blur1 = New Effects.GaussianBlurEffect() With {.Source = es, .BlurAmount = 5}
                DrawingSession.DrawImage(blur1)
                DrawingSession.DrawImage(es)
            End Using
        End Using
    End Sub
End Class
