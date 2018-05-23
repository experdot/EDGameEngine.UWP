''' <summary>
''' 表示一个游戏数据模型对应的视图
''' </summary>
Public Interface IGameView
    Inherits IGameObject
    ''' <summary>
    ''' 是否允许位图缓存
    ''' </summary>
    Property CacheAllowed As Boolean
    ''' <summary>
    ''' 附加至的可视化对象
    ''' </summary>
    Property GameVisual As IGameVisual
    ''' <summary>
    ''' 附加至指定的<see cref=" IGameVisual"/>对象
    ''' </summary>
    Sub AttachToGameVisual(target As IGameVisual)
End Interface
