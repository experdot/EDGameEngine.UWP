Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Effects
Imports Windows.Graphics.Effects
''' <summary>
''' 表示某种类型模型的视图
''' </summary>
Public MustInherit Class TypedGameView(Of T As IGameVisual)
    Inherits GameView
    Protected Property Target As T
    Sub New(Target As T)
        Me.Target = Target
    End Sub
    Public Overrides Sub BeginDraw(DrawingSession As CanvasDrawingSession)
        If CacheAllowed AndAlso Cache IsNot Nothing Then
            DrawingSession.DrawImage(CType(TransformEffect.EffectStatic(Cache, DrawingSession, Target.Transform), ICanvasImage))
        Else
            Using cmdList = New CanvasCommandList(DrawingSession)
                Using Dl = cmdList.CreateDrawingSession
                    OnDraw(Dl)
                End Using
                Dim effect As IGraphicsEffectSource = cmdList
                For Each SubEffect In Target.GameComponents.Effects.Items
                    effect = SubEffect.Effect(effect, DrawingSession)
                Next
                DrawingSession.DrawImage(CType(TransformEffect.EffectStatic(effect, DrawingSession, Target.Transform), ICanvasImage))
                If CacheAllowed AndAlso Cache Is Nothing Then
                    Cache = BitmapCacheHelper.CacheImage(DrawingSession, CType(effect, ICanvasImage))
                End If
            End Using
        End If
    End Sub
End Class
