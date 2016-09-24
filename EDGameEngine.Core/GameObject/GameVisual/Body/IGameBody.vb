Imports System.Numerics
''' <summary>
''' 表示一个可视化的游戏物体模型
''' </summary>
Public Interface IGameBody
    Inherits IGameVisual
    ''' <summary>
    ''' 物体的视图
    ''' </summary>
    Property Presenter As IGameView
End Interface
