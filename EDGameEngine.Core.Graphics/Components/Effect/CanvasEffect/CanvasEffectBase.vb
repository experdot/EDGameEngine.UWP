Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 效果器基类
''' </summary>
Public MustInherit Class CanvasEffectBase
    Inherits GameComponentBase
    Implements ICanvasEffect
    Public Overrides Property ComponentType As ComponentType = ComponentType.Effect
    Public MustOverride Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
    Public Function Effect(source As IGraphicsEffectSource) As IGraphicsEffectSource Implements IEffect.Effect
        Return EffectWithCanvasResourceCreator(source, CanvasDevice.GetSharedDevice)
    End Function
    Public Function EffectWithCanvasResourceCreator(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource Implements ICanvasEffect.EffectWithCanvasResourceCreator
        Return Effect(source, resourceCreator)
    End Function
    Public Overrides Sub Start()

    End Sub
    Public Overrides Sub Update()

    End Sub
End Class
