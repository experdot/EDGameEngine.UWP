Imports System.Numerics
Imports EDGameEngine
''' <summary>
''' 可视化的游戏物体基类
''' </summary>
Public MustInherit Class GameVisualModel
    Implements IGameVisualModel
    Public Overridable Property Location As Vector2 = Vector2.Zero Implements IGameVisualModel.Location
    Public Overridable Property Scale As Vector2 = Vector2.One Implements IGameVisualModel.Scale
    Public Overridable Property Rotation As Single = 0.0F Implements IGameVisualModel.Rotation
    Public Overridable Property Visible As Boolean = True Implements IGameVisualModel.Visible
    Public MustOverride Property Presenter As GameView Implements IGameVisualModel.Presenter
    Public MustOverride Sub Update() Implements IGameVisualModel.Update
End Class
