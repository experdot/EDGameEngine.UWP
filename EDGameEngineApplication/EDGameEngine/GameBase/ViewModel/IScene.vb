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
