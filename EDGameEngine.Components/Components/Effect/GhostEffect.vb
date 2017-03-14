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
        Static LastSource As IGraphicsEffectSource
        If LastSource Is Nothing Then
            Dim render = New CanvasRenderTarget(CType(resourceCreator, ICanvasResourceCreatorWithDpi), CInt(SourceRect.Width), CInt(SourceRect.Height))
            Using ds = render.CreateDrawingSession
                ds.Clear(Windows.UI.Colors.Transparent)
            End Using
            LastSource = render
        End If
        Dim temp = New CanvasRenderTarget(CType(resourceCreator, ICanvasResourceCreatorWithDpi), CInt(SourceRect.Width), CInt(SourceRect.Height))

        Using ds = temp.CreateDrawingSession
            ds.Clear(Windows.UI.Colors.Transparent)
            ds.DrawImage(CType(LastSource, ICanvasImage), Vector2.Zero, SourceRect, Opacity)
            ds.DrawImage(CType(source, ICanvasImage))
        End Using
        LastSource = temp

        Return CanvasBitmap.CreateFromColors(resourceCreator, temp.GetPixelColors, CInt(temp.Bounds.Width), CInt(temp.Bounds.Height))
    End Function
End Class
