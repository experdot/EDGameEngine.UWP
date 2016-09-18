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
        Static col As Color
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                If Target.CurrentList.Count > 0 Then
                    Dim SubVec As Vector2
                    For i = 0 To Target.CurrentList.Count - 1
                        SubVec = Target.CurrentList(i)
                        col = ColorArr(CInt(Target.ImageSize.Width * SubVec.Y + SubVec.X))
                        'Dl.FillCircle(SubVec, 4, Color.FromArgb(128, col.R, col.G, col.B))
                        'Dl.FillRectangle(New Rect(SubVec.X - size / 2, SubVec.Y - size / 2, size, size), Color.FromArgb(Target.Alpha, col.R, col.G, col.B))
                        Dl.FillCircle(SubVec, Target.PenSizeList(i), Color.FromArgb(CByte(Target.Alpha), col.R, col.G, col.B))
                    Next
                End If
            End Using
            DrawingSession.DrawImage(cmdList)
        End Using
    End Sub
End Class
