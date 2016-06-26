Imports System.Numerics
Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Public Class ParticalView
    Inherits TypedGameView(Of ParticalManager)
    Public Sub New(Target As ParticalManager)
        MyBase.New(Target)
    End Sub
    Dim radius As Single
    Public Overrides Sub Display(DrawingSession As CanvasDrawingSession)
        radius = (radius + 0.001) Mod （Math.PI * 2)
        Using cmdList = New CanvasCommandList(DrawingSession.Device)
            Using Dl = cmdList.CreateDrawingSession
                Dl.Transform = Matrix3x2.CreateRotation(radius, New Vector2(WorldSpace.SpaceWidth / 2, WorldSpace.SpaceHeight / 2))
                For i = 0 To Target.Particals.Count - 1
                    Dl.FillCircle(Target.Particals(i).Location, Target.Particals(i).Size, Target.Particals(i).Color)
                    'Dim border As Single = Target.Particals(i).ImageSize * 50 + Target.Particals(i).Age * 2
                    'Dl.DrawImage(BaseSpace.ImageManager.GetResource(ImageResourceID.SmokePartial1),
                    '             New Rect(Target.Particals(i).Location.X - border, Target.Particals(i).Location.Y - border, border * 2, border * 2),
                    '             New Rect(0, 0, 192, 192), 0.15)
                Next
            End Using
            Using blur1 = New Effects.GaussianBlurEffect() With {.Source = cmdList, .BlurAmount = 5}
                DrawingSession.DrawImage(blur1)
                DrawingSession.DrawImage(cmdList)
            End Using
        End Using
    End Sub
End Class
