Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Effects
Imports Windows.Graphics.Effects
''' <summary>
''' 线性变换效果器
''' </summary>
Public Class TransformEffect
    Inherits EffectBase
    ''' <summary>
    ''' 获取或设置当前线性变换
    ''' </summary>
    Public Property Transform As Transform
    Public Sub New(ByRef trans As Transform)
        Transform = trans
    End Sub
    Public Overrides Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource
        Dim trans = New Transform2DEffect With {.Source = source}
        trans.TransformMatrix = Matrix3x2.CreateScale(Transform.Scale, Transform.Center) *
                                Matrix3x2.CreateRotation(Transform.Rotation, Transform.Center) *
                                Matrix3x2.CreateTranslation(Transform.Translation)
        Return trans
    End Function

    Public Shared Function EffectStatic(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession, trans As Transform) As IGraphicsEffectSource
        Dim eff = New Transform2DEffect With {.Source = source}
        eff.TransformMatrix = Matrix3x2.CreateScale(trans.Scale, trans.Center) *
                                Matrix3x2.CreateRotation(trans.Rotation, trans.Center) *
                                Matrix3x2.CreateTranslation(trans.Translation)
        Return eff
    End Function
End Class
