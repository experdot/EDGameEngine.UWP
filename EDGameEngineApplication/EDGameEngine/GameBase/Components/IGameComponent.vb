''' <summary>
''' 表示一个可附加在游戏对象上的组件
''' </summary>
Public Interface IGameComponent
    ''' <summary>
    ''' 开始
    ''' </summary>
    Sub Start()
    ''' <summary>
    ''' 更新
    ''' </summary>
    Sub Update()
End Interface
