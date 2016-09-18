Imports Microsoft.Graphics.Canvas
''' <summary>
''' 表示游戏场景
''' </summary>
Public Interface IScene
    Inherits IDisposable
    ''' <summary>
    ''' 场景宽度
    ''' </summary>
    Property Width As Single
    ''' <summary>
    ''' 场景高度
    ''' </summary>
    Property Height As Single
    ''' <summary>
    ''' 所属世界
    ''' </summary>
    Property World As World
    ''' <summary>
    ''' 摄像机
    ''' </summary>
    Property Camera As ICamera
    ''' <summary>
    ''' 图像资源
    ''' </summary>
    Property ImageManager As ImageResourceManager
    ''' <summary>
    ''' 用户输入
    ''' </summary>
    Property Inputs As Inputs
    ''' <summary>
    ''' 游戏图层
    ''' </summary>
    Property GameLayers As List(Of ILayer)
    ''' <summary>
    ''' 游戏模型
    ''' </summary>
    Property GameVisuals As List(Of IGameVisual)
    ''' <summary>
    ''' 加载场景资源
    ''' </summary>
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
    Event MouseMove()
End Interface
