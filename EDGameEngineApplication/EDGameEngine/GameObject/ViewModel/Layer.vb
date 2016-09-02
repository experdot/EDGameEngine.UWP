Imports System.Numerics
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 图层
''' </summary>
Public Class Layer
    Implements ILayer
    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements IObjectStatus.Appearance
    Public Overridable Property Transform As Transform = Transform.Normal Implements IObjectStatus.Transform
    Public Overridable Property GameVisuals As New List(Of IGameVisualModel) Implements ILayer.GameVisuals
    Public Overridable Property Scene As Scene
    Public Sub OnDraw(drawingSession As CanvasDrawingSession) Implements ILayer.OnDraw
        Using cmdList = New CanvasCommandList(drawingSession)
            Using dl = cmdList.CreateDrawingSession
                For Each SubGameVisual In GameVisuals
                    SubGameVisual.Presenter.BeginDraw(dl)
                Next
            End Using
            drawingSession.DrawImage(cmdList)
        End Using
    End Sub
End Class
