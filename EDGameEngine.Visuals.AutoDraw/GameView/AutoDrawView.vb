Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Windows.UI
Imports System.Numerics
Imports EDGameEngine.Core.Utilities
''' <summary>
''' 自动绘图的视图
''' </summary>
Public Class AutoDrawView
    Inherits TypedCanvasView(Of IAutoDrawModel)
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Static Canvas As New LayerCanvas(drawingSession, Target.ImageSize, Target.LayerCount)
        Using drawing = Canvas.CreateLayerRender()
            Dim point As New VertexWithLayer
            While Target.CurrentPoints.TryDequeue(point)
                If point IsNot Nothing Then
                    drawing.FillCircle(point)
                End If
            End While
            Canvas.OnDraw(drawingSession)
        End Using
    End Sub
End Class
