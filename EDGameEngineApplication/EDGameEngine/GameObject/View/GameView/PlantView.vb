Imports System.Numerics
Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class PlantView
    Inherits TypedGameView(Of Plant)
    Public Sub New(Target As Plant)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                DrawTree(Dl, Target.Tree, New Vector2, 0)
            End Using
            DrawingSession.DrawImage(cmdList)
        End Using
    End Sub
    Dim Tw() As Single = {0.618, 0.628, 0.638, 0.648, 0.658, 0.668, 0.678, 0.688, 0.698, 0.708, 0.718}
    Dim LineCss As New Geometry.CanvasStrokeStyle With {.StartCap = Geometry.CanvasCapStyle.Triangle,
                                                            .EndCap = Geometry.CanvasCapStyle.Triangle}
    Private Sub DrawTree(DS As CanvasDrawingSession, ParentNode As TreeNode, OffSet As Vector2, angle As Single, Optional Ratio As Single = 16)
        For Each SubNode In ParentNode.ChildNode
            Dim StrokeWidth = Target.Tree.Rank * 15 * Math.Pow(Tw(SubNode.Rank), 8 - SubNode.Rank) * SubNode.Percent
            Dim midLoc = ParentNode.RealLoc + SubNode.Location.RotateNew(SubNode.MidRotateAngle / 5) * SubNode.Percent * 0.618

            SubNode.RealLoc = ParentNode.RealLoc + SubNode.Location * SubNode.Percent

            DS.DrawLine(ParentNode.RealLoc, midLoc, Colors.Black, StrokeWidth, LineCss)
            DS.DrawLine(SubNode.RealLoc, midLoc, Colors.Black, StrokeWidth, LineCss)
            Dim TempV = ParentNode.RealLoc + SubNode.RealLoc.RotateNew(SubNode.DiePercent * Math.PI * 2) * (Math.Sin(SubNode.DiePercent) / 2 * 0)
            DrawImageBranch(DS, ParentNode, SubNode, SubNode.Location, TempV)
            DrawFlower(DS, Ratio, SubNode, TempV)
            DrawTree(DS, SubNode, OffSet, angle)
        Next
    End Sub
    Private Sub DrawImageBranch(DS As CanvasDrawingSession, ParentNode As TreeNode, SubNode As TreeNode, loc As Vector2, tempV As Vector2)
        Dim branchWidth = Target.Tree.Rank * 15 * Math.Pow(Tw(SubNode.Rank), 9 - SubNode.Rank) * SubNode.Percent
        Dim BranchHeight As Single = loc.Length * SubNode.Percent
        DS.Transform = Matrix3x2.CreateRotation(Math.Atan2(loc.Y, loc.X) - Math.PI / 2 + Math.PI * 2 * SubNode.DiePercent * 0, tempV)
        DS.DrawImage(Target.Scene.ImageManager.GetResource(ImageResourceID.TreeBranch1), New Rect(tempV.X, tempV.Y, branchWidth, BranchHeight), New Rect(0, 0, 100, 300), 0.9 + (0.1 / Target.Tree.Rank) * SubNode.Rank)
        DS.Transform = Matrix3x2.CreateRotation(0)
    End Sub
    Private Sub DrawFlower(DS As CanvasDrawingSession, Ratio As Single, SubNode As TreeNode, tempV As Vector2)
        If SubNode.HasFlower And SubNode.Percent > 0.5 Then
            Dim Border As Single = Ratio * SubNode.FlowerSize * SubNode.Percent
            'DS.DrawImage(DirectCast(Target.Scene, Scene).ImageManager.GetResource(ImageResourceID.YellowFlower1), New Rect(tempV.X - Border, tempV.Y - Border, Border * 2, Border * 2))
        End If
    End Sub
End Class
