Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas

Public Class AutoDrawModel
    Inherits GameBody
    ''' <summary>
    ''' 线条画集合
    ''' </summary>
    Public Property DrawingManager As DrawingManager
    ''' <summary>
    ''' 原图
    ''' </summary>
    Public Property Image As CanvasBitmap
    ''' <summary>
    ''' 原图大小
    ''' </summary>
    Public Property ImageSize As Size
    ''' <summary>
    ''' 当前绘制序列
    ''' </summary>
    Public Property CurrentPoints As New Concurrent.ConcurrentQueue(Of PointWithLayer)
    ''' <summary>
    ''' 当前绘制长度
    ''' </summary>
    Public Property PointsCount As Integer = 300
    ''' <summary>
    ''' 最大绘制长度
    ''' </summary>
    Public Property PointsCountMax As Integer = 300
    ''' <summary>
    ''' 图层数量
    ''' </summary>
    Public Property LayerCount As Integer = 8

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
