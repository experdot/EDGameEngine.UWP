Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Effects
Imports Windows.Graphics.Effects
''' <summary>
''' 线性变换效果器
''' </summary>
Public Class TransformEffect
    Inherits CanvasEffectBase
    ''' <summary>
    ''' 获取或设置当前线性变换
    ''' </summary>
    Public Property Transform As Transform

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(ByRef trans As Transform)
        Transform = trans
    End Sub
    Public Overrides Function Effect(source As IGraphicsEffectSource, creator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim trans As New Transform2DEffect With {.Source = source}
        trans.TransformMatrix = Matrix3x2.CreateScale(Transform.Scale, Transform.Center) *
                                Matrix3x2.CreateRotation(Transform.Rotation, Transform.Center) *
                                Matrix3x2.CreateTranslation(Transform.Translation)
        Return trans
    End Function

    Public Shared Function EffectStatic(source As IGraphicsEffectSource, session As CanvasDrawingSession, transform As Transform) As IGraphicsEffectSource
        Dim effect As New Transform2DEffect With {.Source = source}
        effect.TransformMatrix = Matrix3x2.CreateScale(transform.Scale, transform.Center) *
                                Matrix3x2.CreateRotation(transform.Rotation, transform.Center) *
                                Matrix3x2.CreateTranslation(transform.Translation)
        Return effect
    End Function
End Class
