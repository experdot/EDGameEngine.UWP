Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas

Public Class AutoDrawByFastAIModel
    Inherits GameBody
    Implements IAutoDrawModel
    ''' <summary>
    ''' 线条画集合
    ''' </summary>
    Public Property DrawingManager As DrawingManager
    ''' <summary>
    ''' 原图
    ''' </summary>
    Public Property Image As CanvasBitmap Implements IAutoDrawModel.Image
    ''' <summary>
    ''' 原图大小
    ''' </summary>
    Public Property ImageSize As Size Implements IAutoDrawModel.ImageSize
    ''' <summary>
    ''' 当前绘制序列
    ''' </summary>
    Public Property CurrentPoints As New Concurrent.ConcurrentQueue(Of PointWithLayer) Implements IAutoDrawModel.CurrentPoints
    ''' <summary>
    ''' 当前绘制长度
    ''' </summary>
    Public Property PointsCount As Integer = 300 Implements IAutoDrawModel.PointsCount
    ''' <summary>
    ''' 最大绘制长度
    ''' </summary>
    Public Property PointsCountMax As Integer = 300 Implements IAutoDrawModel.PointsCountMax
    ''' <summary>
    ''' 图层数量
    ''' </summary>
    Public Property LayerCount As Integer = 8 Implements IAutoDrawModel.LayerCount
    ''' <summary>
    ''' 绘圆的图层索引
    ''' </summary>
    Public Property CircleLayers As Integer() = {7} Implements IAutoDrawModel.CircleLayers

    Public Overrides Sub StartEx()
        Me.Rect = New Rect(0, 0, Image.Bounds.Width, Image.Bounds.Height)
        Me.ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)

        DrawingManager = New DrawingManager
        DrawingManager.InitFromImage(Image, LayerCount)
    End Sub
    Public Overrides Sub UpdateEx()
        UpdateDrawings()
    End Sub
    Private Sub UpdateDrawings()
        If CurrentPoints.Count = 0 AndAlso Not DrawingManager.IsOver Then
            For i = 0 To PointsCount - 1
                CurrentPoints.Enqueue(DrawingManager.NextPointFast())
            Next
            If PointsCount < PointsCountMax Then PointsCount += 1
        End If
    End Sub
End Class
