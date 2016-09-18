Imports System.Numerics
''' <summary>
''' 表示一个可视化的游戏数据模型
''' </summary>
Public Interface IGameVisual
    Inherits IVisualObject
    ''' <summary>
    ''' 物体的视图对象
    ''' </summary>
    Property Presenter As IGameView
    ''' <summary>
    ''' 初始化
    ''' </summary>
    Sub Start()
    ''' <summary>
    ''' 更新模型
    ''' </summary>
    Sub Update()
End Interface
