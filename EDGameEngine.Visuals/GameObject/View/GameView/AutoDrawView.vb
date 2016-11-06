Imports Microsoft.Graphics.Canvas
Imports System.Numerics
Imports Windows.UI
Imports EDGameEngine.Core

Public Class AutoDrawView
    Inherits TypedGameView(Of AutoDrawModel)
    Public Sub New(Target As AutoDrawModel)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        For Each SubPoint In Target.CurrentPoints
            drawingSession.FillCircle(SubPoint.Position, SubPoint.Size, SubPoint.Color)
        Next
        Target.CurrentPoints.Clear()
    End Sub
End Class
