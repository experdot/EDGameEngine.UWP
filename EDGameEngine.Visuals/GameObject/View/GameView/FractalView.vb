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
        Dim tempVertex As ColorVertex
        While Target.Vertexs.TryDequeue(tempVertex)
            DrawingSession.DrawRectangle(tempVertex.Position.X, tempVertex.Position.Y, 1, 1, tempVertex.Color)
        End While
    End Sub
End Class
