''' <summary>
''' 可视化数据模型基类
''' </summary>
Public MustInherit Class GameVisualModel
    Implements IGameVisualModel
    Public MustOverride ReadOnly Property Presenter As GameView Implements IGameVisualModel.Presenter
    Public MustOverride Sub Update() Implements IGameVisualModel.Update
End Class
