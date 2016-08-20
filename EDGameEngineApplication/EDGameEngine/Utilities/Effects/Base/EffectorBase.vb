Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 效果器基类
''' </summary>
Public MustInherit Class EffectorBase
    Implements IEffector
    ''' <summary>
    ''' 返回或设置效果器的有效性
    ''' </summary>
    Public Property Enabled As Boolean = True
    Public MustOverride Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource Implements IEffector.Effect

End Class
