Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 分形的视图
''' </summary>
Public Class FractalView
    Inherits TypedGameView(Of IFractal)
    Public Sub New(Target As IFractal)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Dim tempPoint As New Point
        While Target.Vertexs.TryDequeue(tempPoint)
            'DrawingSession.DrawCircle(tempPoint.Position, tempPoint.Size, tempPoint.Color)
            Dim size = tempPoint.Size / 2
            DrawingSession.DrawRectangle(New Rect(tempPoint.Position.X - size, tempPoint.Position.Y - size, tempPoint.Size, tempPoint.Size), tempPoint.Color)
        End While
    End Sub
End Class
