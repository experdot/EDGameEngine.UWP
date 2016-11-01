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
        DrawingMgr = New DrawingManager
        Dim tempPixels As New PixelData(Image.GetPixelColors, CInt(Image.Bounds.Width), CInt(Image.Bounds.Height))
        Dim sizes As Single() = {32, 16, 8, 4, 1}
        Dim alphas As Byte() = {16, 32, 64, 128, 255}
        For i = 0 To 4
            DrawingMgr.Drawings.Add(New Drawing(tempPixels, i + 3) With {.PenAlpha = alphas(i), .PenSize = sizes(i)})
        Next
        For i = 0 To 3
            DrawingMgr.Drawings(i).Denoising(50 - i * 10)
            DrawingMgr.Drawings(i).MatchAverageColor()
        Next
        ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)
        GameComponents.Effects.Add(New GhostEffect() With {.SourceRect = Image.Bounds})
        GameComponents.Effects.Add(New ShadowEffect)
    End Sub
    Public Overrides Sub UpdateEx()
        Static ImageVec As Vector2 = New Vector2(CSng(ImageSize.Width), CSng(ImageSize.Height)) / 2
        '图像位置居中
        Transform.Translation = New Vector2(Scene.Width, Scene.Height) / 2 - ImageVec '- Loc / 4
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
