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
            AttachToGameVisual(value)
        End Set
    End Property
    ''' <summary>
    ''' 渲染目标
    ''' </summary>
    Protected Target As T

    Public Overrides Sub AttachToGameVisual(target As IGameVisual)
        '解除原来的对象
        If Me.Target IsNot Nothing Then
            Me.Target.Presenter = Nothing
        End If
        '附加至新的对象
        If target Is Nothing Then
            Me.Target = Nothing
        Else
            Me.Target = CType(target, T)
            target.Presenter = Me
        End If
    End Sub
    Public Overrides Sub BeginDraw(session As CanvasDrawingSession)
        If CacheAllowed AndAlso Cache IsNot Nothing Then
            session.DrawImage(CType(TransformEffect.EffectStatic(Cache, session, Target.Transform), ICanvasImage))
        Else
            Using cmdList = New CanvasCommandList(session)
                Using ds = cmdList.CreateDrawingSession
                    OnDraw(ds)
                End Using
                Me.CommandList = cmdList
                Dim effect As IGraphicsEffectSource = cmdList
                For Each SubEffect In Target.GameComponents.Effects.Items
                    effect = CType(SubEffect, ICanvasEffect).EffectWithCanvasResourceCreator(effect, session)
                Next
                session.DrawImage(CType(TransformEffect.EffectStatic(effect, session, Target.Transform), ICanvasImage))
                If CacheAllowed AndAlso Cache Is Nothing Then
                    Cache = BitmapCacheHelper.CacheImage(session, CType(effect, ICanvasImage))
                End If
            End Using
        End If
    End Sub
End Class
