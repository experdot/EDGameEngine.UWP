Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports EDGameEngine.Core.Utilities
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 植物的视图
''' </summary>
Public Class PlantView
    Inherits TypedCanvasView(Of IPlant)
    ''' <summary>
    ''' 树干资源Id
    ''' </summary>
    Public Property BranchResourceId As Integer
    ''' <summary>
    ''' 树叶资源Id
    ''' </summary>
    Public Property LeafResourceId As Integer
    ''' <summary>
    ''' 花朵资源Id
    ''' </summary>
    Public Property FlowerResourceId As Integer

    Public Sub New(Target As IPlant)
        MyBase.New(Target)
    End Sub

    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        DrawTree(drawingSession, Target.Root)
    End Sub

    Private Sub DrawTree(drawingSession As CanvasDrawingSession, parent As TreeNode)
        For Each SubNode In parent.Children
            SubNode.RealLocation = parent.RealLocation + SubNode.Location * SubNode.Percent
            'DrawLineBranch(drawingSession, SubNode)
            DrawImageBranch(drawingSession, SubNode)
            DrawImageFlower(drawingSession, SubNode)
            DrawTree(drawingSession, SubNode)
        Next
    End Sub
    Private Sub DrawLineBranch(drawingSession As CanvasDrawingSession, node As TreeNode)
        Dim strokeStyle As New Geometry.CanvasStrokeStyle With {.StartCap = Geometry.CanvasCapStyle.Triangle, .EndCap = Geometry.CanvasCapStyle.Triangle}
        Dim strokeWidth As Single = CSng(Target.Root.Rank * 5 * Math.Pow(0.618, 8 - node.Rank)) * node.Percent
        Dim middleLocation = node.Parent.RealLocation + node.Location.RotateNew(node.MidRotateAngle / 3) * 0.618 * node.Percent
        drawingSession.DrawLine(node.Parent.RealLocation, middleLocation, Colors.Black, strokeWidth, strokeStyle)
        drawingSession.DrawLine(node.RealLocation, middleLocation, Colors.Black, strokeWidth, strokeStyle)
    End Sub
    Private Sub DrawImageBranch(drawingSession As CanvasDrawingSession, node As TreeNode)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image As CanvasBitmap = DirectCast(ImageResource.GetResource(BranchResourceId), CanvasBitmap)
        Static SrcRect As Rect = Image.Bounds
        Dim branchWidth = (Target.Root.Rank + 2) * 15 * Math.Pow(0.618, Target.Root.Rank - node.Rank) * node.Percent
        Dim branchHeight As Single = node.Location.Length * node.Percent
        Dim alpha As Single = CSng(1.0F)
        drawingSession.Transform = Matrix3x2.CreateRotation(CSng(Math.Atan2(node.Location.Y, node.Location.X) - Math.PI / 2）, node.Parent.RealLocation)
        drawingSession.DrawImage(Image, New Rect(node.Parent.RealLocation.X, node.Parent.RealLocation.Y, branchWidth, branchHeight), SrcRect, alpha)
        drawingSession.Transform = Matrix3x2.Identity
    End Sub
    Private Sub DrawImageFlower(drawingSession As CanvasDrawingSession, node As TreeNode)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image As CanvasBitmap = DirectCast(ImageResource.GetResource(LeafResourceId), CanvasBitmap)
        Static SrcRect As Rect = Image.Bounds
        If node.Flowers.Count > 0 AndAlso node.Percent > 0.5 Then
            For i = 0 To node.Flowers.Count - 1
                Dim flower As Flower = node.Flowers(i)
                Dim border As Single = 14 * flower.Size * node.Percent
                Dim location As Vector2 = flower.RealLocation
                drawingSession.Transform *= Matrix3x2.CreateRotation(flower.Rotation, location)
                drawingSession.DrawImage(Image, New Rect(location.X - border, location.Y - border, border * 2, border * 2), SrcRect, flower.Opacity)
                drawingSession.Transform = Matrix3x2.Identity
            Next
        End If
    End Sub
End Class
