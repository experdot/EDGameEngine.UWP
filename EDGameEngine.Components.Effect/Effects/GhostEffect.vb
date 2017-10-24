Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' [Experimental]残影效果器
''' </summary>
Public Class GhostEffect
    Inherits CanvasEffectBase
    ''' <summary>
    ''' 获取或设置残影不透明度
    ''' </summary>
    Public Property Opacity As Single = 1.0F
    ''' <summary>
    ''' 获取或设置残影偏移向量
    ''' </summary>
    Public Property Offset As Vector2 = Vector2.Zero

    'Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
    '    Static LastSource As IGraphicsEffectSource

    '    Dim rect = Target.Scene.Rect
    '    If LastSource Is Nothing Then
    '        Dim render = New CanvasRenderTarget(resourceCreator, CInt(rect.Width), CInt(rect.Height))
    '        Using ds = render.CreateDrawingSession
    '            ds.Clear(Windows.UI.Colors.Transparent)
    '        End Using
    '        LastSource = render
    '    End If
    '    Dim drawingSession As CanvasDrawingSession = resourceCreator


    '    Dim temp = New CanvasRenderTarget(resourceCreator, CInt(rect.Width), CInt(rect.Height))
    '    Using ds = temp.CreateDrawingSession
    '        ds.Clear(Windows.UI.Colors.Transparent)
    '        ds.DrawImage(CType(LastSource, ICanvasImage), Vector2.Zero, rect, Opacity)
    '        ds.DrawImage(CType(source, ICanvasImage))
    '    End Using
    '    LastSource = temp

    '    'Return temp
    '    Return CanvasBitmap.CreateFromColors(resourceCreator, temp.GetPixelColors, CInt(temp.Bounds.Width), CInt(temp.Bounds.Height))
    'End Function


    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Static LastSource As CanvasBitmap

        Dim rect As Rect = Target.Scene.Rect
        Dim cmdlist = New CanvasCommandList(resourceCreator)

        Using ds = cmdlist.CreateDrawingSession
            ds.Clear(Windows.UI.Colors.Transparent)
            If LastSource IsNot Nothing Then
                ds.DrawImage(LastSource, Offset, rect, Opacity)
            End If
            ds.DrawImage(source)
        End Using

        LastSource = BitmapCacheHelper.CacheImageClip(resourceCreator, cmdlist, rect)
        Return cmdlist
    End Function
End Class
