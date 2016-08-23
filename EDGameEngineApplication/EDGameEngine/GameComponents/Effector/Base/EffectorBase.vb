Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 效果器基类
''' </summary>
Public MustInherit Class EffectorBase
    Inherits GameComponentBase
    Implements IEffector
    Public MustOverride Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource Implements IEffector.Effect
    Public Overrides Sub Start()

    End Sub
    Public Overrides Sub Update()

    End Sub
End Class
