Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Text
Imports Windows.UI

Public Class RippleView
    Inherits TypedGameView(Of Ripple)
    Public Sub New(Target As Ripple)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Static effector As New Effector()
        Static SrcData = Target.Image.GetPixelColors
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                Dl.DrawImage(BitmapPixelHelper.GetWaveImage(Dl, Target.Image.GetBounds(Dl), SrcData, Target.Buffer1, Target.Buffer2))
            End Using
            DrawingSession.DrawImage(effector.Stream(cmdList))
        End Using
    End Sub
End Class
