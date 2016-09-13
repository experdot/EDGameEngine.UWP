Imports System.Numerics
Imports EDGameEngine
''' <summary>
''' 可视化的游戏物体基类
''' </summary>
Public MustInherit Class GameVisual
    Implements IGameVisual
    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements IGameVisual.Appearance
    Public Overridable Property Transform As Transform = Transform.Normal Implements IGameVisual.Transform
    Public Overridable Property GameComponents As GameComponents = New GameComponents(Me) Implements IGameVisual.GameComponents
    Public Overridable Property Scene As IScene Implements IGameVisual.Scene
        Set(value As IScene)
            m_Scene = value
            Start()
        End Set
        Get
            Return m_Scene
        End Get
    End Property
    Public Overridable Property Presenter As GameView Implements IGameVisual.Presenter
    Public Shared Property Rnd As New Random
    Private m_Scene As Scene

    Public Sub Start() Implements IGameVisual.Start
        StartSelf()
        GameComponents.Start()
    End Sub
    Public Sub Update() Implements IGameVisual.Update
        UpdateSelf()
        GameComponents.Update()
    End Sub

    Public MustOverride Sub StartSelf()
    Public MustOverride Sub UpdateSelf()
End Class
