''' <summary>
''' 表示一个可视化的游戏数据模型
''' </summary>
Public Interface IGameVisualModel
    ReadOnly Property Presenter As GameView
    Sub Update()
End Interface
