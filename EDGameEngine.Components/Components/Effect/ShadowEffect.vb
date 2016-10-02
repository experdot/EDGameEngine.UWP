Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
''' <summary>
''' 阴影效果
''' </summary>
Public Class ShadowEffect
    Inherits EffectBase
    Public Overrides Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource
        Dim rect = CType(source, ICanvasImage).GetBounds(DrawingSession)
        Using cac = New CanvasRenderTarget(DrawingSession, New Size(rect.Width, rect.Height))
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.DrawImage(CType(source, ICanvasImage))
                Using shadow = New Effects.ShadowEffect With {.Source = Target.Presenter.CommandList}
                    ds.DrawImage(shadow)
                    ds.DrawRectangle(shadow.GetBounds(DrawingSession), Windows.UI.Colors.Black)
                End Using
            End Using
            Return CanvasBitmap.CreateFromColors(DrawingSession, cac.GetPixelColors, CInt(sizepx.Width), CInt(sizepx.Height))
        End Using
    End Function
End Class
