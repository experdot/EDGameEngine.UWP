Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 场景视图
''' </summary>
Public Class SceneView
    Inherits TypedCanvasView(Of IScene)
    Public Sub New(Target As IScene)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        If Target.State = SceneState.Loop Then
            LoadedDraw(drawingSession)
        Else
            LoadingDraw(drawingSession)
        End If
    End Sub
    Public Overridable Sub LoadingDraw(drawingSession As CanvasDrawingSession)
        Static Dots As String() = {" ", ".", "..", "..."}
        Static Index As Integer
        Index = (Index + 1) Mod 80
        drawingSession.DrawText("场景加载中，请稍后" & Dots(CInt(Math.Truncate(Index / 20))), New Vector2(Target.Width, Target.Height) / 2, Colors.Black, TextFormat.Center)
        drawingSession.DrawText(Target.Progress.Description, New Vector2(Target.Width, Target.Height + 50) / 2, Colors.Black, TextFormat.CenterL)
    End Sub
    Public Overridable Sub LoadedDraw(drawingSession As CanvasDrawingSession)
        For Each SubLayer In Target.GameLayers
            Using cmdList = New CanvasCommandList(drawingSession)
                Using dl = cmdList.CreateDrawingSession
                    dl.Clear(SubLayer.Background)
                    CType(SubLayer.Presenter, ICanvasView).BeginDraw(dl)
                End Using
                drawingSession.DrawImage(cmdList)
            End Using
        Next
    End Sub
End Class

