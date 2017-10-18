Imports Windows.UI
''' <summary>
''' 表示游戏图层
''' </summary>
Public Interface ILayer
    Inherits IGameVisual
    ''' <summary>
    ''' 图层包含的可视化对象
    ''' </summary>
    Property GameBodys As List(Of IGameBody)
    ''' <summary>
    ''' 背景色
    ''' </summary>
    Property Background As Color
End Interface
