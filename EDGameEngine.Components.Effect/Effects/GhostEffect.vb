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

    Public Overrides Function Effect(source As IGraphicsEffectSource, creator As ICanvasResourceCreator) As IGraphicsEffectSource
        Static LastSource As CanvasBitmap
        Dim rect As Rect = Target.Scene.Rect
        Dim cmdlist = New CanvasCommandList(creator)
        Using ds = cmdlist.CreateDrawingSession
            ds.Clear(Windows.UI.Colors.Transparent)
            If LastSource IsNot Nothing Then
                ds.DrawImage(LastSource, Offset, rect, Opacity)
            End If
            ds.DrawImage(CType(source, ICanvasImage))
        End Using
        LastSource = BitmapCacheHelper.CacheImageClip(creator, cmdlist, rect)
        Return cmdlist
    End Function
End Class
