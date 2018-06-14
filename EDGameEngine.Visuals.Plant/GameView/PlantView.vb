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

    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        DrawTree(session, Target.Root)
    End Sub

    Private Sub DrawTree(session As CanvasDrawingSession, parent As TreeNode)
        For Each node In parent.Children
            node.RealLocation = parent.RealLocation + node.Location * node.Percent
            'DrawLineBranch(drawingSession, SubNode)
            DrawImageBranch(session, node)
            DrawImageFlower(session, node)
            DrawTree(session, node)
        Next
    End Sub
    Private Sub DrawLineBranch(session As CanvasDrawingSession, node As TreeNode)
        Dim strokeStyle As New Geometry.CanvasStrokeStyle With {.StartCap = Geometry.CanvasCapStyle.Triangle, .EndCap = Geometry.CanvasCapStyle.Triangle}
        Dim strokeWidth As Single = CSng(Target.Root.Rank * 5 * Math.Pow(0.618, Target.Root.Rank - node.Rank)) * node.Percent
        Dim middleLocation = node.Parent.RealLocation + node.Location.RotateNew(node.MidRotateAngle / 3) * 0.618 * node.Percent
        session.DrawLine(node.Parent.RealLocation, middleLocation, Colors.Black, strokeWidth, strokeStyle)
        session.DrawLine(node.RealLocation, middleLocation, Colors.Black, strokeWidth, strokeStyle)
    End Sub
    Private Sub DrawImageBranch(session As CanvasDrawingSession, node As TreeNode)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image As CanvasBitmap = DirectCast(ImageResource.GetResource(BranchResourceId), CanvasBitmap)
        Static SrcRect As Rect = Image.Bounds
        Dim branchWidth = (Target.Root.Rank + 2) * 16 * Math.Pow(0.618, Target.Root.Rank - node.Rank) * node.Percent
        Dim branchHeight As Single = node.Location.Length * node.Percent
        Dim alpha As Single = CSng(1.0F)
        session.Transform = Matrix3x2.CreateRotation(CSng(Math.Atan2(node.Location.Y, node.Location.X) - Math.PI / 2）, node.Parent.RealLocation)
        session.DrawImage(Image, New Rect(node.Parent.RealLocation.X, node.Parent.RealLocation.Y, branchWidth, branchHeight), SrcRect, alpha)
        session.Transform = Matrix3x2.Identity
    End Sub
    Private Sub DrawImageFlower(session As CanvasDrawingSession, node As TreeNode)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image1 As CanvasBitmap = DirectCast(ImageResource.GetResource(LeafResourceId), CanvasBitmap)
        Static Image2 As CanvasBitmap = DirectCast(ImageResource.GetResource(FlowerResourceId), CanvasBitmap)
        Static SrcRect1 As Rect = Image1.Bounds
        Static SrcRect2 As Rect = Image2.Bounds
        If node.Flowers.Count > 0 AndAlso node.Percent > 0.5 Then
            For i = 0 To node.Flowers.Count - 1
                Dim flower As Flower = node.Flowers(i)
                Dim border As Single = If(flower.Kind = 0, 14, 10) * flower.Size * node.Percent
                Dim image As CanvasBitmap = If(flower.Kind = 0, Image1, Image2)
                Dim center As Vector2 = flower.RealLocation

                DrawTrajectory(session, flower)

                session.Transform *= Matrix3x2.CreateRotation(flower.Rotation, center) * Matrix3x2.CreateScale(flower.Scale, center)
                session.DrawImage(image, New Rect(center.X - border, center.Y - border, border * 2, border * 2), image.Bounds, flower.Opacity)
                session.Transform = Matrix3x2.Identity
            Next
        End If
    End Sub
    Private Sub DrawTrajectory(session As CanvasDrawingSession, flower As Flower)
        If flower.Trajectory.Count > 1 Then
            For j = 0 To flower.Trajectory.Count - 2
                Dim alpha As Integer = CInt((Math.Sin(Math.PI * j / flower.Trajectory.Count) * 40 + 5) * flower.Opacity)
                session.DrawLine(flower.Trajectory(j), flower.Trajectory(j + 1), Color.FromArgb(CByte(alpha), 0, 0, 0))
            Next
        End If
    End Sub
End Class
