Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 残影效果器
''' </summary>
Public Class GhostEffect
    Inherits EffectBase
    ''' <summary>
    ''' 获取或设置当前坐标
    ''' </summary>
    Public Property Position As Vector2
    ''' <summary>
    ''' 获取或设置源矩形
    ''' </summary>
    Public Property SourceRect As Rect
    ''' <summary>
    ''' 获取或设置残影不透明度
    ''' </summary>
    Public Property Opacity As Single = 1.0F
    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Static Cache As IGraphicsEffectSource
        Static last As Vector2 = Position
        Static offset As Vector2
        offset = Position - last
        last = Position
        If Cache Is Nothing Then Cache = source
        Using cac = New CanvasRenderTarget(CType(resourceCreator, ICanvasResourceCreatorWithDpi), CInt(SourceRect.Width), CInt(SourceRect.Height))
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.Clear(Windows.UI.Colors.Transparent)
                ds.DrawImage(CType(Cache, ICanvasImage), -offset, SourceRect, Opacity)
                ds.DrawImage(CType(source, ICanvasImage))
            End Using
            Cache = CanvasBitmap.CreateFromColors(resourceCreator, cac.GetPixelColors, CInt(sizepx.Width), CInt(sizepx.Height))
            Return Cache
        End Using
    End Function
End Class
