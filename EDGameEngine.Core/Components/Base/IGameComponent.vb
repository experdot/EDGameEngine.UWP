''' <summary>
''' 表示一个可附加在游戏对象上的组件
''' </summary>
Public Interface IGameComponent
    Inherits IGameObject
    ''' <summary>
    ''' 目标模型
    ''' </summary>
    Property Target As IGameVisual
    ''' <summary>
    ''' 类型标记
    ''' </summary>
    Property ComponentType As ComponentType
End Interface
