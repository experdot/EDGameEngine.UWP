Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 植物的视图
''' </summary>
Public Class PlantView
    Inherits TypedCanvasView(Of IPlant)
    Public Sub New(Target As IPlant)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        DrawTree(drawingSession, Target.Root, New Vector2, 0)
    End Sub
    Dim Tw() As Single = {0.618, 0.628, 0.638, 0.648, 0.658, 0.668, 0.678, 0.688, 0.698, 0.708, 0.718}
    Dim LineCss As New Geometry.CanvasStrokeStyle With {.StartCap = Geometry.CanvasCapStyle.Triangle,
                                                        .EndCap = Geometry.CanvasCapStyle.Triangle}
    Private Sub DrawTree(ds As CanvasDrawingSession, ParentNode As TreeNode, OffSet As Vector2, angle As Single, Optional Ratio As Single = 16)
        For Each SubNode In ParentNode.Children
            Dim StrokeWidth As Single = CSng(Target.Root.Rank * 5 * Math.Pow(Tw(SubNode.Rank), 8 - SubNode.Rank) * SubNode.Percent)
            Dim midLoc = ParentNode.RealLoc + SubNode.Location.RotateNew(SubNode.MidRotateAngle / 5) * SubNode.Percent * 0.618

            SubNode.RealLoc = ParentNode.RealLoc + SubNode.Location * SubNode.Percent

            ds.DrawLine(ParentNode.RealLoc, midLoc, Colors.Black, StrokeWidth, LineCss)
            ds.DrawLine(SubNode.RealLoc, midLoc, Colors.Black, StrokeWidth, LineCss)
            Dim TempV = ParentNode.RealLoc + SubNode.RealLoc.RotateNew(CSng(SubNode.DiePercent * Math.PI * 2)) * CSng((Math.Sin(SubNode.DiePercent) / 2 * 0))
            'DrawImageBranch(ds, ParentNode, SubNode, SubNode.Location, TempV)
            DrawFlower(ds, Ratio, SubNode, TempV)
            DrawTree(ds, SubNode, OffSet, angle)
        Next
    End Sub
    Private Sub DrawImageBranch(ds As CanvasDrawingSession, ParentNode As TreeNode, SubNode As TreeNode, loc As Vector2, tempV As Vector2)
        Dim branchWidth = Target.Root.Rank * 15 * Math.Pow(Tw(SubNode.Rank), 9 - SubNode.Rank) * SubNode.Percent
        Dim BranchHeight As Single = loc.Length * SubNode.Percent
        ds.Transform = Matrix3x2.CreateRotation(CSng(Math.Atan2(loc.Y, loc.X) - Math.PI / 2 + Math.PI * 2 * SubNode.DiePercent * 0）, tempV)
        'ds.DrawImage(Target.Scene.ImageManager.GetResource(ImageResourceId.TreeBranch1),
        '             New Rect(tempV.X, tempV.Y, branchWidth, BranchHeight),
        '             New Rect(0, 0, 100, 300),
        '            CSng(0.9 + (0.1 / Target.Root.Rank) * SubNode.Rank))
        ds.Transform = Matrix3x2.CreateRotation(0)
    End Sub
    Private Sub DrawFlower(ds As CanvasDrawingSession, Ratio As Single, SubNode As TreeNode, tempV As Vector2)
        If SubNode.HasFlower And SubNode.Percent > 0.5 Then
            Dim Border As Single = Ratio * SubNode.FlowerSize * SubNode.Percent
            'ds.DrawImage(DirectCast(Target.Scene.ImageManager.GetResource(ImageResourceId.YellowFlower1), CanvasBitmap),
            '             New Rect(tempV.X - Border, tempV.Y - Border, Border * 2, Border * 2))
        End If
    End Sub
End Class
