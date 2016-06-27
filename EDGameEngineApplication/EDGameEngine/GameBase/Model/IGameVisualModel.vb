Imports System.Numerics
''' <summary>
''' 表示一个可视化的游戏数据模型
''' </summary>
Public Interface IGameVisualModel
    Inherits IObjectStatus
    ''' <summary>
    ''' 物体的视图对象
    ''' </summary>
    ''' <returns></returns>
    Property Presenter As GameView
    ''' <summary>
    ''' 更新物体状态
    ''' </summary>
    Sub Update()
End Interface
