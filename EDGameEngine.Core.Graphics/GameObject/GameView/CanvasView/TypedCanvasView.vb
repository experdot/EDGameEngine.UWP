Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Effects
Imports Windows.Graphics.Effects
''' <summary>
''' 表示某种类型的由Win2D渲染的视图
''' </summary>
Public MustInherit Class TypedCanvasView(Of T As IGameVisual)
    Inherits CanvasView
    Public Overrides Property GameVisual As IGameVisual
        Get
            Return Target
        End Get
        Set(value As IGameVisual)
            Target = CType(value, T)
        End Set
    End Property
    ''' <summary>
    ''' 渲染目标
    ''' </summary>
    Protected Property Target As T

    Public Overrides Sub AttachToGameVisual(target As IGameVisual)
        Me.Target = CType(target, T)
        target.Presenter = Me
    End Sub
    Public Overrides Sub BeginDraw(drawingSession As CanvasDrawingSession)
        If CacheAllowed AndAlso Cache IsNot Nothing Then
            drawingSession.DrawImage(CType(TransformEffect.EffectStatic(Cache, drawingSession, Target.Transform), ICanvasImage))
        Else
            Using cmdList = New CanvasCommandList(drawingSession)
                Using ds = cmdList.CreateDrawingSession
                    OnDraw(ds)
                End Using
                Me.CommandList = cmdList
                Dim effect As IGraphicsEffectSource = cmdList
                For Each SubEffect In Target.GameComponents.Effects.Items
                    effect = CType(SubEffect, ICanvasEffect).EffectWithCanvasResourceCreator(effect, drawingSession)
                Next
                drawingSession.DrawImage(CType(TransformEffect.EffectStatic(effect, drawingSession, Target.Transform), ICanvasImage))
                If CacheAllowed AndAlso Cache Is Nothing Then
                    Cache = BitmapCacheHelper.CacheImage(drawingSession, CType(effect, ICanvasImage))
                End If
            End Using
        End If
    End Sub
End Class
