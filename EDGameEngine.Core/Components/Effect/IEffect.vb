Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 效果器
''' </summary>
Public Interface IEffect
    Inherits IGameComponent
    ''' <summary>
    ''' 返回当前效果变换
    ''' </summary>
    ''' <param name="source">源</param>
    ''' <param name="DrawingSession">绘图命令器</param>
    ''' <returns></returns>
    Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource
End Interface
