Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

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
    Public Property CurrentPoints As New Concurrent.ConcurrentQueue(Of PointWithLayer)
    ''' <summary>
    ''' 每帧绘制长度
    ''' </summary>
    Public Property PointsCountMax As Integer = 300
    ''' <summary>
    ''' 图层数量
    ''' </summary>
    Public Property LayerCount As Integer

    Public Overrides Sub StartEx()
        Me.LayerCount = 8
        Me.Rect = New Rect(0, 0, Image.Bounds.Width, Image.Bounds.Height)
        Me.ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)

        DrawingMgr = New DrawingManager
        DrawingMgr.InitFromImage(Image, LayerCount)
        'GameComponents.Effects.Add(New GhostEffect() With {.SourceRect = Image.Bounds})
        'GameComponents.Effects.Add(New ShadowEffect)
    End Sub
    Public Overrides Sub UpdateEx()
        '更新绘制序列
        UpdateDrawings()
    End Sub
    Private Sub UpdateDrawings()
        If CurrentPoints.Count = 0 Then
            For i = 0 To PointsCountMax - 1
                CurrentPoints.Enqueue(DrawingMgr.NextPointQuality())
            Next
            'If PointsCountMax < 100 Then PointsCountMax += 1
        End If
    End Sub
End Class
