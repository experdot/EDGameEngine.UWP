Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 高斯模糊效果
''' </summary>
Public Class GaussianBlurEffect
    Inherits CanvasEffectBase
    ''' <summary>
    ''' 模糊半径
    ''' </summary>
    Public Property BlurAmount As Integer = 3

    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Return New Effects.GaussianBlurEffect With {.Source = source, .BlurAmount = BlurAmount}
    End Function

    Public Shared Function EffectStatic(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator, Optional blurAmount As Integer = 3) As IGraphicsEffectSource
        Return New Effects.GaussianBlurEffect With {.Source = source, .BlurAmount = blurAmount}
    End Function
End Class
