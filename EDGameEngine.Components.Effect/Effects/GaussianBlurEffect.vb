Imports System.Numerics
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
    ''' <summary>
    ''' 是否绘制原始图像
    ''' </summary>
    Public Property IsDrawRaw As Boolean = False
    ''' <summary>
    ''' 偏移
    ''' </summary>
    Public Property Offset As Vector2 = Vector2.Zero
    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim cmdList = New CanvasCommandList(resourceCreator)
        Using ds = cmdList.CreateDrawingSession
            Using blur = New Effects.GaussianBlurEffect With {.Source = source, .BlurAmount = BlurAmount}
                ds.Clear(Windows.UI.Colors.Transparent)
                ds.DrawImage(blur, Offset)
                If IsDrawRaw Then
                    ds.DrawImage(CType(source, ICanvasImage))
                End If
            End Using
        End Using
        Return cmdList
    End Function
End Class
