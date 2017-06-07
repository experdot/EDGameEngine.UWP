Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 使用贴图的粒子系统视图
''' </summary>
Public Class ParticlesImageView
    Inherits TypedGameView(Of IParticles)
    ''' <summary>
    ''' 贴图资源Id
    ''' </summary>
    Public Property ImageResourceId As Integer
    ''' <summary>
    ''' 贴图缩放比例
    ''' </summary>
    Public Property ImageScale As Single = 1.0F

    Public Property Colors As Color()
    Public Property Bounds As Rect

    Public Sub New(Target As IParticles)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Dim image As CanvasBitmap = DirectCast(Target.Scene.ImageManager.GetResource(ImageResourceId), CanvasBitmap)
        Dim srcRect As Rect = image.Bounds
        For Each SubParticle In Target.Particles
            If SubParticle.Age = 0 Then Continue For
            Dim x As Integer = CInt（SubParticle.Location.X） + 200
            Dim y As Integer = CInt(SubParticle.Location.Y)
            If x < 0 Then x = 0
            If y < 0 Then y = 0
            If x > Bounds.Width - 1 Then x = CInt(Bounds.Width - 1)
            If y > Bounds.Height - 1 Then y = CInt(Bounds.Height - 1)

            Dim color As Color = Colors(CInt(y * Bounds.Width + x))
            color.A = CByte(SubParticle.Color.R / 4)
            'If SubParticle.Color.R = 250 AndAlso SubParticle.Size < 3.0F Then
            '    Dim tempV As Vector2 = SubParticle.Location
            '    Dim Border As Single = SubParticle.Size * ImageScale * RandomHelper.NextNorm(0, 200) / 100
            '    Dim alpha As Single = CSng(RandomHelper.NextNorm(0, 100) / 100) 'CSng(SubParticle.Color.A / 255)
            '    drawingSession.DrawImage(image, New Rect(tempV.X - Border, tempV.Y - Border, Border * 2, Border * 2), srcRect, alpha)
            'End If
            drawingSession.FillCircle(SubParticle.Location, SubParticle.Size, color)
        Next
    End Sub
End Class
