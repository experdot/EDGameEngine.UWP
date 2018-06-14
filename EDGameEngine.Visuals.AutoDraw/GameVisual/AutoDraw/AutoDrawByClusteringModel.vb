Imports EDGameEngine.Core
Imports EDGameEngine.Visuals.AutoDraw
Imports Microsoft.Graphics.Canvas

Public Class AutoDrawByClusteringModel
    Inherits GameBody
    Implements IAutoDrawModel
    Public Property Image As CanvasBitmap Implements IAutoDrawModel.Image
    Public Property ImageSize As Size Implements IAutoDrawModel.ImageSize
    Public Property CurrentPoints As New Concurrent.ConcurrentQueue(Of VertexWithLayer) Implements IAutoDrawModel.CurrentPoints
    Public Property PointsCountPerFrame As Integer = 1 Implements IAutoDrawModel.PointsCountPerFrame
    Public Property PointsCountMaxPerFrame As Integer = 5 Implements IAutoDrawModel.PointsCountMaxPerFrame
    Public Property LayerCount As Integer = 13 Implements IAutoDrawModel.LayerCount
    Public Property VertexWithLayerProvider As IVertexWithLayerProvider Implements IAutoDrawModel.VertexWithLayerProvider

    Public Overrides Sub StartEx()
        Me.Rect = New Rect(0, 0, Image.Bounds.Width, Image.Bounds.Height)
        Me.ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)
        Me.VertexWithLayerProvider = New ClusterAI(New PixelData(Image.GetPixelColors, CInt(Image.Bounds.Width), CInt(Image.Bounds.Height)), LayerCount - 1)
    End Sub
    Public Overrides Sub UpdateEx()
        UpdateDrawings()
    End Sub
    Private Sub UpdateDrawings()
        If CurrentPoints.Count = 0 AndAlso Not VertexWithLayerProvider.IsOver Then
            For i = 0 To PointsCountPerFrame - 1
                CurrentPoints.Enqueue(VertexWithLayerProvider.NextPoint())
            Next
            If PointsCountPerFrame < PointsCountMaxPerFrame Then PointsCountPerFrame += 1
        End If
    End Sub
End Class
