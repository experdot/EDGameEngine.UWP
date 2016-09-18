Imports System.Numerics
''' <summary>
''' 可视化的游戏物体基类
''' </summary>
Public MustInherit Class GameVisual
    Implements IGameVisual
    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements IGameVisual.Appearance
    Public Overridable Property Transform As Transform = Transform.Normal Implements IGameVisual.Transform
    Public Overridable Property GameComponents As GameComponents = New GameComponents(Me) Implements IGameVisual.GameComponents
    Public Overridable Property Scene As IScene Implements IGameVisual.Scene
    Public Overridable Property Presenter As IGameView Implements IGameVisual.Presenter
    Public Shared Property Rnd As New Random
    Public Sub Start() Implements IGameVisual.Start
        StartEx()
        GameComponents.Start()
    End Sub
    Public Sub Update() Implements IGameVisual.Update
        UpdateEx()
        GameComponents.Update()
    End Sub

    Public MustOverride Sub StartEx()
    Public MustOverride Sub UpdateEx()
End Class
