Imports System.Numerics
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 图层
''' </summary>
Public Class Layer
    Inherits LayerBase
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(drawingSession)
            Using dl = cmdList.CreateDrawingSession
                For Each SubGameVisual In GameVisuals
                    SubGameVisual.Presenter.BeginDraw(dl)
                Next
            End Using
            Using trans = Effector.Transform2D(cmdList, Transform)
                drawingSession.DrawImage(trans)
            End Using
        End Using
    End Sub
End Class
