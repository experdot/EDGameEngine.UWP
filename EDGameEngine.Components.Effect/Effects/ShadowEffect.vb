Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 阴影效果
''' </summary>
Public Class ShadowEffect
    Inherits CanvasEffectBase
    ''' <summary>
    ''' 是否绘制原始图像
    ''' </summary>
    Public Property IsDrawRaw As Boolean = False
    ''' <summary>
    ''' 偏移
    ''' </summary>
    Public Property Offset As Vector2 = Vector2.Zero
    Public Overrides Function Effect(source As IGraphicsEffectSource, creator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim cmdList = New CanvasCommandList(creator)
        Using ds = cmdList.CreateDrawingSession
            Using shadow = New Effects.ShadowEffect With {.Source = CType(Target.Presenter, CanvasView).CommandList}
                ds.Clear(Windows.UI.Colors.Transparent)
                ds.DrawImage(shadow, Offset)
                If IsDrawRaw Then
                    ds.DrawImage(CType(source, ICanvasImage))
                End If
            End Using
        End Using
        Return cmdList
    End Function
End Class
