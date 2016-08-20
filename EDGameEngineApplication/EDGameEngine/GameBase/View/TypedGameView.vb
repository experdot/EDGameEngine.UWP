Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
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
            Dim eff As IGraphicsEffectSource = cmdList
            For Each SubEffector In Target.Effectors
                eff = SubEffector.Effect(eff, DrawingSession)
            Next
            DrawingSession.DrawImage(TransformEffector.EffectStatic(eff, DrawingSession, Target.Transform))
        End Using
    End Sub
End Class
