Imports System.Numerics
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class ParticlesBackgroundImageView
    Inherits TypedCanvasView(Of IParticles)
    ''' <summary>
    ''' 贴图资源Id
    ''' </summary>
    Public Property ImageResourceId As Integer
    ''' <summary>
    ''' 贴图缩放比例
    ''' </summary>
    Public Property ImageScale As Single = 1.0F
    ''' <summary>
    ''' 偏移
    ''' </summary>
    Public Property Offset As Vector2 = Vector2.Zero
    ''' <summary>
    ''' 是否居中
    ''' </summary>
    Public Property IsCenter As Boolean = True
    ''' <summary>
    ''' 不透明度
    ''' </summary>
    Public Property Opacity As Single = 1.0F

    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image As CanvasBitmap = DirectCast(ImageResource.GetResource(ImageResourceId), CanvasBitmap)
        Static Colors As Color() = Image.GetPixelColors()
        Static SourceRect As Rect = Image.Bounds

        Dim realOffset As Vector2
        If IsCenter Then
            realOffset = New Vector2(Target.Scene.Width, Target.Scene.Height) - New Vector2(CSng(SourceRect.Width), CSng(SourceRect.Height))
            realOffset = -(realOffset / 2)
        Else
            realOffset = Offset
        End If

        For Each SubParticle In Target.Particles
            Dim x As Integer = CInt(SubParticle.Location.X + realOffset.X)
            Dim y As Integer = CInt(SubParticle.Location.Y + realOffset.Y)
            If x < 0 Then x = 0
            If y < 0 Then y = 0
            If x > SourceRect.Width - 1 Then x = CInt(SourceRect.Width - 1)
            If y > SourceRect.Height - 1 Then y = CInt(SourceRect.Height - 1)

            Dim color As Color = Colors(CInt(y * SourceRect.Width + x))
            color.A = CByte(SubParticle.Color.A * Opacity)
            session.FillCircle(SubParticle.Location, SubParticle.Size, color)
        Next
    End Sub
End Class
