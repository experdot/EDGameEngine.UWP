Imports System.Numerics
''' <summary>
''' 表示一个可视化的游戏数据模型
''' </summary>
Public Interface IGameVisualModel
    Inherits IObjectStatus
    ''' <summary>
    ''' 物体所在场景
    ''' </summary>
    ''' <returns></returns>
    Property Scene As IScene
    ''' <summary>
    ''' 物体的视图对象
    ''' </summary>
    ''' <returns></returns>
    Property Presenter As GameView
    ''' <summary>
    ''' 组件管理对象
    ''' </summary>
    Property GameComponents As GameComponents
    ''' <summary>
    ''' 初始化模型
    ''' </summary>
    Sub Start()
    ''' <summary>
    ''' 更新模型
    ''' </summary>
    Sub Update()
End Interface
