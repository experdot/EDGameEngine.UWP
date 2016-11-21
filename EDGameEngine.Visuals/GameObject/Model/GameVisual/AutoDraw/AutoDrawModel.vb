Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Public Class AutoDrawModel
    Inherits GameBody
    ''' <summary>
    ''' 线条画集合
    ''' </summary>
    Public Property DrawingMgr As DrawingManager
    ''' <summary>
    ''' 原图
    ''' </summary>
    Public Property Image As CanvasBitmap
    ''' <summary>
    ''' 原图大小
    ''' </summary>
    Public Property ImageSize As Size
    ''' <summary>
    ''' 绘制序列
    ''' </summary>
    Public Property CurrentPoints As New List(Of Point)
    ''' <summary>
    ''' 每帧绘制长度
    ''' </summary>
    Public Property PointsCountMax As Integer = 800

    Public Overrides Sub StartEx()
        ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)
        Me.Rect = New Rect(0, 0, ImageSize.Width, ImageSize.Height)
        DrawingMgr = New DrawingManager
        Dim sizes As Single() = {16, 8, 4, 2}
        Dim alphas As Byte() = {120, 160, 200, 240}
        Dim noises As Integer() = {60, 40, 20, 10}
        Dim tempPixels As New PixelData
        For i = 0 To 3
            Dim tempImage As CanvasBitmap = CType(GaussianBlurEffect.EffectStatic(Image, Scene.World.ResourceCreator, 4 - i), CanvasBitmap)
            tempPixels = New PixelData(tempImage.GetPixelColors, CInt(Image.Bounds.Width), CInt(Image.Bounds.Height))
            DrawingMgr.Drawings.Add(New Drawing(tempPixels, i + 3) With {.PenAlpha = alphas(i), .PenSize = sizes(i)})
            DrawingMgr.Drawings(i).Denoising(noises(i))
            DrawingMgr.Drawings(i).MatchAverageColor()
            DrawingMgr.Drawings(i).MatchLineSize()
        Next
        tempPixels = New PixelData(Image.GetPixelColors, CInt(Image.Bounds.Width), CInt(Image.Bounds.Height))
        DrawingMgr.Drawings.Add(New Drawing(tempPixels, 7) With {.PenAlpha = 255, .PenSize = 1})

        GameComponents.Effects.Add(New GhostEffect() With {.SourceRect = Image.Bounds})
        GameComponents.Effects.Add(New ShadowEffect)
    End Sub
    Public Overrides Sub UpdateEx()
        '更新绘制序列
        UpdateDrawings()
    End Sub
    Private Sub UpdateDrawings()
        If CurrentPoints.Count = 0 Then
            For i = 0 To PointsCountMax - 1
                CurrentPoints.Add(DrawingMgr.NextPoint())
            Next
            If PointsCountMax < 3000 Then PointsCountMax += 1
        End If
    End Sub
End Class
