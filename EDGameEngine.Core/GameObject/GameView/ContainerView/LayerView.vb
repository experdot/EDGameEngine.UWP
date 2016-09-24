Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 图层视图
''' </summary>
Public Class LayerView
    Inherits GameView
    Protected Property Target As ILayer
    Public Sub New(Target As ILayer)
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
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        For Each SubBody In Target.GameBodys
            SubBody.Presenter.BeginDraw(DrawingSession)
        Next
    End Sub
End Class
