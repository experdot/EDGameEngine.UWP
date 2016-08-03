Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Effects
Imports Windows.Graphics.Effects
''' <summary>
''' 效果器
''' </summary>
Public Class Effector
    ''' <summary>
    ''' 2D效果变换
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="transform"></param>
    ''' <returns></returns>
    Public Shared Function Transform2D(source As IGraphicsEffectSource, transform As Transform) As ICanvasImage
        Dim trans = New Transform2DEffect With {.Source = source}
        trans.TransformMatrix = Matrix3x2.CreateScale(transform.Scale, transform.Center) *
                                    Matrix3x2.CreateRotation(transform.Rotation, transform.Center) *
                                    Matrix3x2.CreateTranslation(transform.Position)
        Return trans
    End Function
End Class
