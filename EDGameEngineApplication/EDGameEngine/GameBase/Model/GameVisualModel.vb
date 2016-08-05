Imports System.Numerics
Imports EDGameEngine
''' <summary>
''' 可视化的游戏物体基类
''' </summary>
Public MustInherit Class GameVisualModel
    Implements IGameVisualModel
    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements IObjectStatus.Appearance
    Public Overridable Property Transform As Transform = Transform.Normal Implements IObjectStatus.Transform
    Public Overridable Property Scene As IScene Implements IGameVisualModel.Scene
    Public Overridable Property Presenter As GameView Implements IGameVisualModel.Presenter
    Public Shared Property Rnd As New Random
    Public MustOverride Sub Update() Implements IGameVisualModel.Update
End Class
