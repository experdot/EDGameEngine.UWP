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
        Dim cac = New CanvasRenderTarget(CType(resourceCreator, ICanvasResourceCreatorWithDpi), New Size(rect.Width, rect.Height))
        Using ds = cac.CreateDrawingSession
            Using shadow = New Effects.ShadowEffect With {.Source = Target.Presenter.CommandList}
                ds.Clear(Windows.UI.Colors.Transparent)
                ds.DrawImage(CType(source, ICanvasImage))
                ds.DrawImage(shadow)
                ds.DrawRectangle(shadow.GetBounds(resourceCreator), Windows.UI.Colors.Black)
            End Using
        End Using
        Return cac
    End Function
End Class
