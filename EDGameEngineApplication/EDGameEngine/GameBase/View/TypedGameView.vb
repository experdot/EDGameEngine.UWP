Imports Microsoft.Graphics.Canvas
''' <summary>
''' 表示某种类型模型的视图
''' </summary>
''' <typeparam name="T"></typeparam>
Public MustInherit Class TypedGameView(Of T As IGameVisualModel)
    Inherits GameView
    Protected Property Target As T
    Sub New(Target As T)
        Me.Target = Target
    End Sub
    Public Overrides Sub BeginDraw(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                OnDraw(Dl)
            End Using
            Using es = Effector.Transform2D(cmdList, Target.Transform)
                DrawingSession.DrawImage(es)
            End Using
        End Using
    End Sub
End Class
