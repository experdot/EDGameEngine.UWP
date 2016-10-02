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
        Static ColorArr() As Color = Target.Image.GetPixelColors
        Static Image As ICanvasImage = Target.Scene.ImageManager.GetResource(ImageResourceID.YellowFlower1)
        Static col As Color
        Static SubVec As Vector2
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                If Target.CurrentList.Count > 0 Then
                    For i = 0 To Target.CurrentList.Count - 1
                        SubVec = Target.CurrentList(i)
                        col = ColorArr(CInt(Target.ImageSize.Width * SubVec.Y + SubVec.X))
                        col.A = CByte(Target.Alpha)
                        'Dl.FillCircle(SubVec, 4, Color.FromArgb(128, col.R, col.G, col.B))
                        'Dl.FillRectangle(New Rect(SubVec.X - Target.PenSizeList(i) / 2, SubVec.Y - Target.PenSizeList(i) / 2, Target.PenSizeList(i), Target.PenSizeList(i)), col)
                        Dl.FillCircle(SubVec, Target.PenSizeList(i), col)
                    Next
                    Target.CurrentList.Clear()
                End If
            End Using
            DrawingSession.DrawImage(cmdList)
        End Using
    End Sub
End Class
