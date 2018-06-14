Imports System.Numerics
Imports EDGameEngine.Core.Graphics
Imports EDGameEngine.Core.Utilities
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 使用贴图的粒子系统视图
''' </summary>
Public Class ParticlesImageView
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

    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image As CanvasBitmap = DirectCast(ImageResource.GetResource(ImageResourceId), CanvasBitmap)
        Static SourceRect As Rect = Image.Bounds

        For Each particle In Target.Particles
            Dim tempV As Vector2 = particle.Location
            Dim border As Single = particle.Size * ImageScale * RandomHelper.NextNorm(0, 200) / 100
            Dim opacity As Single = CSng(particle.Color.A / 255)
            session.DrawImage(Image, New Rect(tempV.X - border, tempV.Y - border, border * 2, border * 2), SourceRect, opacity)
        Next
    End Sub
End Class
