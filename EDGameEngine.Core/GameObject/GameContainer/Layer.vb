Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 图层
''' </summary>
Public Class Layer
    Implements ILayer
    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements ILayer.Appearance
    Public Overridable Property Transform As Transform = Transform.Normal Implements ILayer.Transform
    Public Overridable Property Scene As IScene Implements ILayer.Scene
    Public Overridable Property GameVisuals As New List(Of IGameBody) Implements ILayer.GameVisuals
    Public Overridable Property GameComponents As GameComponents = New GameComponents(Me) Implements ILayer.GameComponents

    Public Sub OnDraw(drawingSession As CanvasDrawingSession) Implements ILayer.OnDraw
        Using cmdList = New CanvasCommandList(drawingSession)
            Using dl = cmdList.CreateDrawingSession
                For Each SubGameVisual In GameVisuals
                    SubGameVisual.Presenter.BeginDraw(dl)
                Next
            End Using

            Dim effect As IGraphicsEffectSource = cmdList
            For Each SubEffect In GameComponents.Effects.Items
                effect = SubEffect.Effect(effect, drawingSession)
            Next
            drawingSession.DrawImage(CType(TransformEffect.EffectStatic(effect, drawingSession, Transform), ICanvasImage))
        End Using
    End Sub
End Class
