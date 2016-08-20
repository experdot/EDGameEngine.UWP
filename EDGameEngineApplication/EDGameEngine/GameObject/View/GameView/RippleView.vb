Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Text
Imports Windows.UI

Public Class RippleView
    Inherits TypedGameView(Of Ripple)
    Public Sub New(Target As Ripple)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Static SrcData = Target.Image.GetPixelColors
        drawingSession.DrawImage(BitmapPixelHelper.GetWaveImage(drawingSession, Target.Image.GetBounds(drawingSession), SrcData, Target.Buffer1, Target.Buffer2))
    End Sub
End Class
