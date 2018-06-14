Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 由Win2D渲染的效果器
''' </summary>
Public Interface ICanvasEffect
    Inherits IEffect
    Function EffectWithCanvasResourceCreator(source As IGraphicsEffectSource, creator As ICanvasResourceCreator) As IGraphicsEffectSource
End Interface
