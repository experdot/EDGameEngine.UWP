''' <summary>
''' 表示一个可附加在游戏对象上的组件
''' </summary>
Public Interface IGameComponent
    ''' <summary>
    ''' 目标模型
    ''' </summary>
    Property Target As IVisualObject
    ''' <summary>
    ''' 类型标记
    ''' </summary>
    Property CompnentType As ComponentType
    ''' <summary>
    ''' 开始
    ''' </summary>
    Sub Start()
    ''' <summary>
    ''' 更新
    ''' </summary>
    Sub Update()
End Interface
