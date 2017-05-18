Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 场景视图
''' </summary>
Public Class SceneView
    Inherits TypedGameView(Of IScene)
    Public Sub New(Target As IScene)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        For Each SubLayer In Target.GameLayers
            Using cmdList = New CanvasCommandList(drawingSession)
                Using dl = cmdList.CreateDrawingSession
                    dl.Clear(SubLayer.Background)
                    SubLayer.Presenter.BeginDraw(dl)
                End Using
                drawingSession.DrawImage(cmdList)
            End Using
        Next
    End Sub
End Class

