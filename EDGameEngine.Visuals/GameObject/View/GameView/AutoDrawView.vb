Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
''' <summary>
''' 自动绘图的视图
''' </summary>
Public Class AutoDrawView
    Inherits TypedGameView(Of AutoDrawModel)
    Public Sub New(Target As AutoDrawModel)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Dim tempPoint As New Point
        While Target.CurrentPoints.TryDequeue(tempPoint)
            drawingSession.FillCircle(tempPoint.Position, tempPoint.Size, tempPoint.Color)
        End While
    End Sub
End Class
