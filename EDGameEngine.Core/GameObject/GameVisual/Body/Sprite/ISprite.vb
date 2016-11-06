Imports Microsoft.Graphics.Canvas
''' <summary>
''' 贴图物体
''' </summary>
Public Interface ISprite
    Inherits IGameBody
    ''' <summary>
    ''' 贴图
    ''' </summary>
    Property Image As ICanvasImage
End Interface
