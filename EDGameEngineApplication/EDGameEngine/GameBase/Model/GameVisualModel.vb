Imports System.Numerics
Imports EDGameEngine
''' <summary>
''' 可视化的游戏物体基类
''' </summary>
Public MustInherit Class GameVisualModel

    Implements IGameVisualModel
    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements IObjectStatus.Appearance
    Public Overridable Property Transform As Transform = Transform.Normal Implements IObjectStatus.Transform
    Public Overridable Property Effectors As New List(Of IEffector) Implements IGameVisualModel.Effectors
    Public Overridable Property Scene As IScene Implements IGameVisualModel.Scene
        Set(value As IScene)
            m_Scene = value
            Start()
        End Set
        Get
            Return m_Scene
        End Get
    End Property
    Public Overridable Property Presenter As GameView Implements IGameVisualModel.Presenter
    Public Shared Property Rnd As New Random
    Private m_Scene As Scene

    Public MustOverride Sub Start() Implements IGameVisualModel.Start
    Public MustOverride Sub Update() Implements IGameVisualModel.Update
End Class
