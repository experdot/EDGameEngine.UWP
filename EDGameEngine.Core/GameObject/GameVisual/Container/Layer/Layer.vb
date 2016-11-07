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
    Public Overridable Property GameView As IGameView = New LayerView(Me) Implements IGameVisual.GameView

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
