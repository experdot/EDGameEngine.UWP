Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' [Experimental]残影效果器
''' </summary>
Public Class GhostEffect
    Inherits EffectBase
    ''' <summary>
    ''' 获取或设置源矩形
    ''' </summary>
    Public Property SourceRect As Rect
    ''' <summary>
    ''' [Unused]获取或设置残影不透明度
    ''' </summary>
    Public Property Opacity As Single = 1.0F
    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Static RenderTarget As CanvasRenderTarget
        If RenderTarget Is Nothing Then
            RenderTarget = New CanvasRenderTarget(CType(resourceCreator, ICanvasResourceCreatorWithDpi), CInt(SourceRect.Width), CInt(SourceRect.Height))
            Using ds = RenderTarget.CreateDrawingSession
                ds.Clear(Windows.UI.Colors.Transparent)
            End Using
        End If
        Using ds = RenderTarget.CreateDrawingSession
            ds.DrawImage(CType(source, ICanvasImage))
        End Using
        Return RenderTarget
    End Function
End Class
