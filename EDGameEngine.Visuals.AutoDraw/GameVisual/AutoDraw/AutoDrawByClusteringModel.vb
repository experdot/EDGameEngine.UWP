Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas

Public Class AutoDrawByClusteringModel
    Inherits GameBody
    Implements IAutoDrawModel
    ''' <summary>
    ''' 聚类AI
    ''' </summary>
    Public Property ClusterAI As ClusterAI

    Public Property Image As CanvasBitmap Implements IAutoDrawModel.Image
    Public Property ImageSize As Size Implements IAutoDrawModel.ImageSize
    Public Property CurrentPoints As New Concurrent.ConcurrentQueue(Of VertexWithLayer) Implements IAutoDrawModel.CurrentPoints
    Public Property PointsCount As Integer = 300 Implements IAutoDrawModel.PointsCount
    Public Property PointsCountMax As Integer = 5000 Implements IAutoDrawModel.PointsCountMax
    Public Property LayerCount As Integer = 13 Implements IAutoDrawModel.LayerCount
    Public Property CircleLayers As Integer() = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12} Implements IAutoDrawModel.CircleLayers
    'Public Property CircleLayers As Integer() = {12} Implements IAutoDrawModel.CircleLayers

    Public Overrides Sub StartEx()
        Me.Rect = New Rect(0, 0, Image.Bounds.Width, Image.Bounds.Height)
        Me.ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)

        ClusterAI = New ClusterAI(New PixelData(Image.GetPixelColors, CInt(Image.Bounds.Width), CInt(Image.Bounds.Height)), LayerCount - 1)

    End Sub
    Public Overrides Sub UpdateEx()
        UpdateDrawings()
    End Sub
    Private Sub UpdateDrawings()
        If CurrentPoints.Count = 0 AndAlso Not ClusterAI.IsOver Then
            For i = 0 To PointsCount - 1
                CurrentPoints.Enqueue(ClusterAI.NextPoint)
            Next
            If PointsCount < PointsCountMax Then PointsCount += 1
        End If
    End Sub
End Class
