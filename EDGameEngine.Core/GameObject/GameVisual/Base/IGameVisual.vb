Imports EDGameEngine.Core
''' <summary>
''' 表示场景里的可视化对象
''' </summary>
Public Interface IGameVisual
    Inherits IGameObject
    ''' <summary>
    ''' 转换
    ''' </summary>
    Property Transform As Transform
    ''' <summary>
    ''' 外观
    ''' </summary>
    Property Appearance As Appearance
    ''' <summary>
    ''' 可视化对象的附加组件管理器
    ''' </summary>
    Property GameComponents As GameComponents
    ''' <summary>
    ''' 视图
    ''' </summary>
    Property GameView As IGameView
    ''' <summary>
    ''' 所属场景
    ''' </summary>
    Property Scene As IScene
End Interface
