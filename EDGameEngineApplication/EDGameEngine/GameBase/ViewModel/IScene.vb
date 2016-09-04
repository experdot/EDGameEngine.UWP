Imports Microsoft.Graphics.Canvas
Public Interface IScene
    Inherits IDisposable
    ''' <summary>
    ''' 场景宽度
    ''' </summary>
    ''' <returns></returns>
    Property Width As Single
    ''' <summary>
    ''' 场景高度
    ''' </summary>
    ''' <returns></returns>
    Property Height As Single
    ''' <summary>
    ''' 所属世界
    ''' </summary>
    ''' <returns></returns>
    Property World As World
    ''' <summary>
    ''' 图像资源
    ''' </summary>
    ''' <returns></returns>
    Property ImageManager As ImageResourceManager
    ''' <summary>
    ''' 游戏图层
    ''' </summary>
    ''' <returns></returns>
    Property GameLayers As List(Of ILayer)
    ''' <summary>
    ''' 游戏模型
    ''' </summary>
    ''' <returns></returns>
    Property GameVisuals As List(Of IGameVisualModel)
    ''' <summary>
    ''' 加载场景资源
    ''' </summary>
    ''' <param name="resourceCreator"></param>
    ''' <returns></returns>
    Function LoadAsync(resourceCreator As ICanvasResourceCreator) As Task
    ''' <summary>
    ''' 创建场景物体
    ''' </summary>
    Sub CreateObject()
    ''' <summary>
    ''' 场景绘制
    ''' </summary>
    ''' <param name="drawingSession"></param>
    Sub OnDraw(drawingSession As CanvasDrawingSession)
    ''' <summary>
    ''' 场景更新
    ''' </summary>
    Sub Update()
End Interface
