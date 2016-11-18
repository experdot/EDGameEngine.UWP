Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 可视化的游戏物体基类
''' </summary>
Public MustInherit Class GameBody
    Implements IGameBody

    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements IGameBody.Appearance
    Public Overridable Property Transform As Transform = Transform.Normal Implements IGameBody.Transform
    Public Overridable Property GameComponents As GameComponents = New GameComponents(Me) Implements IGameBody.GameComponents
    Public Overridable Property Scene As IScene Implements IGameBody.Scene
    Public Overridable Property Presenter As IGameView Implements IGameBody.Presenter
    Public Overridable Property Rect As Rect Implements IGameVisual.Rect
    Public Shared Property Rnd As New Random

    Public Sub Start() Implements IGameBody.Start
        StartEx()
        GameComponents.Start()
    End Sub
    Public Sub Update() Implements IGameBody.Update
        UpdateEx()
        GameComponents.Update()
    End Sub

    Public MustOverride Sub StartEx()
    Public MustOverride Sub UpdateEx()
End Class
