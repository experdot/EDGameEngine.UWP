Imports Microsoft.Graphics.Canvas
''' <summary>
''' 包含Win2D图形资源创建器的对象接口
''' </summary>
Public Interface IObjectWithResourceCreator
    ''' <summary>
    ''' Win2D图形资源创建器
    ''' </summary>
    Property ResourceCreator As ICanvasResourceCreator
End Interface
