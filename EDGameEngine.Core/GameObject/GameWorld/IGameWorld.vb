Imports EDGameEngine.Core
''' <summary>
''' 游戏世界接口
''' </summary>
Public Interface IGameWorld
    Inherits IGameObject
    ''' <summary>
    ''' 宽度
    ''' </summary>
    Property Width As Integer
    ''' <summary>
    ''' 高度
    ''' </summary>
    Property Height As Integer
    ''' <summary>
    ''' 渲染模式
    ''' </summary>
    Property RenderMode As RenderMode
    ''' <summary>
    ''' 当前活动的场景
    ''' </summary>
    Property ActiveScene As IScene
    ''' <summary>
    ''' 场景集合
    ''' </summary>
    Property Scenes As Dictionary(Of String, IScene)
    ''' <summary>
    ''' 切换场景
    ''' </summary>
    Sub SwitchScene(sceneName As String, Optional restart As Boolean = False)
End Interface
