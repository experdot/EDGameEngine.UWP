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
        If Target.Vertexs.Count > 0 Then
            For Each SubVertex In Target.Vertexs
                DrawingSession.DrawRectangle(SubVertex.Position.X, SubVertex.Position.Y, 1, 1, SubVertex.Color)
            Next
            Target.Vertexs.Clear()
        End If
    End Sub
End Class
