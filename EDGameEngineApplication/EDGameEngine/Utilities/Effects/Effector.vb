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
    Public Shared Function Transform2D(source As IGraphicsEffectSource, transform As Transform) As ICanvasImage
        Dim trans = New Transform2DEffect With {.Source = source}
        trans.TransformMatrix = Matrix3x2.CreateScale(transform.Scale, transform.Center) *
                                    Matrix3x2.CreateRotation(transform.Rotation, transform.Center) *
                                    Matrix3x2.CreateTranslation(transform.Position)
        Return trans
    End Function
    ''' <summary>
    ''' 水流效果
    ''' </summary>
    Public Function Stream(source As IGraphicsEffectSource) As ICanvasImage
        Static ts As Single
        ts = (ts + 0.01) Mod (Math.PI * 2)
        Dim dispX As Single = 75.0F * Math.Sin(ts)
        Dim dispY As Single = 75.0F * Math.Cos(ts)
        Dim dispMap = New Effects.DisplacementMapEffect() With {
                     .Source = source,
                     .XChannelSelect = Effects.EffectChannelSelect.Red,
                     .YChannelSelect = Effects.EffectChannelSelect.Green,
                     .Amount = 50.0F,
                     .Displacement = New Effects.Transform2DEffect() With {
                        .TransformMatrix = Matrix3x2.CreateTranslation(dispX, dispY),
                         .Source = New Effects.BorderEffect() With {
                         .ExtendX = CanvasEdgeBehavior.Mirror,
                         .ExtendY = CanvasEdgeBehavior.Mirror,
                         .Source = New Effects.TurbulenceEffect() With {.Octaves = 10, .Frequency = New Vector2(0.01, 0.01)}}}}
        Return dispMap
    End Function
    ''' <summary>
    ''' 残影效果
    ''' </summary>
    ''' <param name="source"></param>
    ''' <returns></returns>
    Public Function Ghost(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession, pos As Vector2, SrcRect As Rect) As ICanvasImage
        Static Cache As IGraphicsEffectSource
        Static last As Vector2 = pos
        Static offset As Vector2
        offset = pos - last
        last = pos
        If Cache Is Nothing Then Cache = source
        Using cac = New CanvasRenderTarget(DrawingSession, SrcRect.Width, SrcRect.Height)
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                'ds.Clear(Windows.UI.Colors.Transparent)
                ds.DrawImage(Cache, -offset, SrcRect, 0.9)
                ds.DrawImage(source)
            End Using
            Cache = CanvasBitmap.CreateFromColors(DrawingSession, cac.GetPixelColors, sizepx.Width, sizepx.Height)
            Return Cache
        End Using
    End Function


End Class
