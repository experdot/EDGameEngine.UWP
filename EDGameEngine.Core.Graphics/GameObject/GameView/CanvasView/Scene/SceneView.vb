Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 场景视图
''' </summary>
Public Class SceneView
    Inherits TypedCanvasView(Of IScene)
    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        If Target.State = SceneState.Loop Then
            LoadedDraw(session)
        Else
            LoadingDraw(session)
        End If
    End Sub
    Public Overridable Sub LoadingDraw(session As CanvasDrawingSession)
        Static Dots As String() = {" ", ".", "..", "..."}
        Static Index As Integer
        Index = (Index + 1) Mod 80
        session.DrawText("场景加载中，请稍后" & Dots(CInt(Math.Truncate(Index / 20))), New Vector2(Target.Width, Target.Height) / 2, Colors.Black, TextFormatHelper.Center)
        session.DrawText(Target.Progress.Description, New Vector2(Target.Width, Target.Height + 50) / 2, Colors.Black, TextFormatHelper.CenterAndSize12)
    End Sub
    Public Overridable Sub LoadedDraw(session As CanvasDrawingSession)
        For Each SubLayer In Target.GameLayers
            Using cmdList = New CanvasCommandList(session)
                Using ds = cmdList.CreateDrawingSession
                    ds.Clear(SubLayer.Background)
                    CType(SubLayer.Presenter, ICanvasView)?.BeginDraw(ds)
                End Using
                session.DrawImage(cmdList)
            End Using
        Next
    End Sub
End Class

