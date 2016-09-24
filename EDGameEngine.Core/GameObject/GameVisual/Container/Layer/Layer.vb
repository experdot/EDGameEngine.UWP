Imports System.Numerics
Imports EDGameEngine.Core
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
    Public Overridable Property GameBodys As New List(Of IGameBody) Implements ILayer.GameBodys
    Public Overridable Property GameComponents As GameComponents = New GameComponents(Me) Implements ILayer.GameComponents
    Public Property Presenter As LayerView = New LayerView(Me) Implements ILayer.Presenter

    Public Sub OnDraw(drawingSession As CanvasDrawingSession) Implements ILayer.OnDraw
        Using cmdList = New CanvasCommandList(drawingSession)
            Using dl = cmdList.CreateDrawingSession
                For Each SubGameVisual In GameBodys
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

    Public Overridable Sub Start() Implements IGameObject.Start
        For Each SubBody In GameBodys
            SubBody.Start()
        Next
        GameComponents.Start()
    End Sub
    Public Overridable Sub Update() Implements IGameObject.Update
        For Each SubBody In GameBodys
            SubBody.Update()
        Next
        GameComponents.Update()
    End Sub
End Class
