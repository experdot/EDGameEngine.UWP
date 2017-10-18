Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.Graphics.Effects
Imports Windows.UI
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
    Public Overridable Property Presenter As IGameView Implements IGameVisual.Presenter
    Public Overridable Property Rect As Rect Implements IGameVisual.Rect
    Public Overridable Property Background As Color = Colors.Transparent Implements ILayer.Background

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
