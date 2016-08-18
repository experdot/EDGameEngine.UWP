Imports Microsoft.Graphics.Canvas
Imports System.Numerics
Imports Windows.UI

Public Class AutoDrawView
    Inherits TypedGameView(Of AutoDrawModel)
    Public Sub New(Target As AutoDrawModel)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Static effector As New Effector
        Static ColorArr() As Color = Target.Image.GetPixelColors
        Static Split As Integer
        Split = (Split + 1) Mod 256
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                For Each SubVec In Target.CurrentList
                    Dim col = ColorArr(Target.ImageSize.Width * SubVec.Y + SubVec.X)
                    Dim mid = Math.Abs(Target.CurrentList.IndexOf(SubVec) - Target.CurrentList.Count / 2)
                    Dim size = Target.Size * (1 - mid / Target.CurrentList.Count * 2)
                    'Dl.FillCircle(SubVec, 4, Color.FromArgb(128, col.R, col.G, col.B))
                    'Dl.FillRectangle(New Rect(SubVec.X - size / 2, SubVec.Y - size / 2, size, size), Color.FromArgb(Target.Alpha, col.R, col.G, col.B))
                    Dl.FillCircle(SubVec, Target.Size, Color.FromArgb(Target.Alpha, col.R, col.G, col.B))
                Next
                'For Each SubSeq In Target.SeqAI.Sequences
                '    For Each SubVec In SubSeq.Points
                '        Dl.FillCircle(SubVec, 1, Colors.Black)
                '    Next
                'Next
                'Dl.DrawImage(BitmapPixelHelper.GetThresholdImageRaw(Dl, Target.Image, Split))
            End Using
            'DrawingSession.DrawImage(cmdList)
            DrawingSession.DrawImage(effector.Ghost(cmdList, DrawingSession, Vector2.Zero, Target.Image.Bounds, 1))

            'For Each SubVec In Target.CurrentList
            '    DrawingSession.FillCircle(SubVec, 1, Color.FromArgb(128, 0, 0, 0))
            'Next
            Using shadow = New Effects.ShadowEffect With {.Source = cmdList}
                DrawingSession.DrawImage(shadow)
            End Using
        End Using
    End Sub
End Class
