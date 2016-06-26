Imports System.Numerics
''' <summary>
''' 表示一个可视化的游戏数据模型
''' </summary>
Public Interface IGameVisualModel
    ''' <summary>
    ''' 物体位置
    ''' </summary>
    ''' <returns></returns>
    Property Location As Vector2
    ''' <summary>
    ''' 物体缩放
    ''' </summary>
    ''' <returns></returns>
    Property Scale As Vector2
    ''' <summary>
    ''' 物体旋转
    ''' </summary>
    ''' <returns></returns>
    Property Rotation As Single
    ''' <summary>
    ''' 物体可见性
    ''' </summary>
    ''' <returns></returns>
    Property Visible As Boolean
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
