Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas

Public Class AutoDrawByFastAIModel
    Inherits GameBody
    Implements IAutoDrawModel

    Public Property Image As CanvasBitmap Implements IAutoDrawModel.Image
    Public Property ImageSize As Size Implements IAutoDrawModel.ImageSize
    Public Property CurrentPoints As New Concurrent.ConcurrentQueue(Of VertexWithLayer) Implements IAutoDrawModel.CurrentPoints
    Public Property PointsCountPerFrame As Integer = 300 Implements IAutoDrawModel.PointsCountPerFrame
    Public Property PointsCountMaxPerFrame As Integer = 300 Implements IAutoDrawModel.PointsCountMaxPerFrame
    Public Property LayerCount As Integer = 8 Implements IAutoDrawModel.LayerCount
    Public Property CircleLayers As Integer() = {7} Implements IAutoDrawModel.CircleLayers

    Public Property DrawingManager As DrawingManager

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
            For i = 0 To PointsCountPerFrame - 1
                CurrentPoints.Enqueue(DrawingManager.NextPointQuality())
            Next
            If PointsCountPerFrame < PointsCountMaxPerFrame Then PointsCountPerFrame += 1
        End If
    End Sub
End Class
