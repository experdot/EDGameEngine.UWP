Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 阴影效果
''' </summary>
Public Class ShadowEffect
    Inherits EffectBase
    Public Overrides Function Effect(source As IGraphicsEffectSource, resourceCreator As ICanvasResourceCreator) As IGraphicsEffectSource
        Dim rect = CType(source, ICanvasImage).GetBounds(resourceCreator)
        Using cac = New CanvasRenderTarget(CType(resourceCreator, ICanvasResourceCreatorWithDpi), New Size(rect.Width, rect.Height))
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                Using shadow = New Effects.ShadowEffect With {.Source = Target.GameView.CommandList}
                    ds.Clear(Windows.UI.Colors.Transparent)
                    ds.DrawImage(CType(source, ICanvasImage))
                    ds.DrawImage(shadow)
                    ds.DrawRectangle(shadow.GetBounds(resourceCreator), Windows.UI.Colors.Black)
                End Using
            End Using
            Return CanvasBitmap.CreateFromColors(resourceCreator, cac.GetPixelColors, CInt(sizepx.Width), CInt(sizepx.Height))
        End Using
    End Function
End Class
