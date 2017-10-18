''' <summary>
''' 表示游戏场景
''' </summary>
Public Interface IScene
    Inherits IGameVisual
    ''' <summary>
    ''' 场景宽度
    ''' </summary>
    ReadOnly Property Width As Single
    ''' <summary>
    ''' 场景高度
    ''' </summary>
    ReadOnly Property Height As Single
    ''' <summary>
    ''' 所属世界
    ''' </summary>
    Property World As IWorld
    ''' <summary>
    ''' 摄像机
    ''' </summary>
    Property Camera As ICamera
    ''' <summary>
    ''' 用户输入
    ''' </summary>
    Property Inputs As Inputs
    ''' <summary>
    ''' 游戏图层
    ''' </summary>
    Property GameLayers As List(Of ILayer)
    ''' <summary>
    ''' 场景状态
    ''' </summary>
    ''' <returns></returns>
    Property State As SceneState
    ''' <summary>
    ''' 加载进度
    ''' </summary>
    Property Progress As Progress
    ''' <summary>
    ''' 添加场景物体
    ''' </summary>
    Sub AddGameVisual(model As IGameBody, view As IGameView, Optional layerIndex As Integer = 0)
End Interface
