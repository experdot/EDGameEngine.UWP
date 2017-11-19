Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports System.Text
Imports System.Numerics
Imports Windows.UI
Imports EDGameEngine.Core.Utilities
''' <summary>
''' L系统树模型的视图
''' </summary>
Public Class LSystemTreeView
    Inherits TypedCanvasView(Of IStateMachine)
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
    ''' <summary>
    ''' 贴图缩放比例
    ''' </summary>
    Public Property ImageScale As Single = 1.0F

    Private Rnd As New Random

    Public Sub New(Target As IStateMachine)
        MyBase.New(Target)
    End Sub

    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Static CurrentI As Integer = 0

        If CurrentI >= Target.States.Count Then Return

        Static LengthOfLine As Single = -20.0F
        Static Center As Vector2 = New Vector2(Target.Scene.Width * 0.5F, Target.Scene.Height * 0.99F)
        Static Offset As New Vector2(0, LengthOfLine)
        Static CenterStack As New Stack(Of Vector2)
        Static OffsetStack As New Stack(Of Vector2)

        Dim stepIndex As Integer = 0
        Dim stepBound As Integer = 1000

        Dim subState As State
        For i = CurrentI To Target.States.Count - 1
            subState = Target.States(i)
            Select Case subState.Id
                Case AscW("F")
                    Dim realOffset = Offset * CSng(Math.Log(Math.E + (5 - CenterStack.Count)) * 0.5) * CSng(Rnd.NextDouble)
                    'DrawLineBranch(drawingSession, Center, realOffset, CenterStack.Count)
                    DrawImageBranch(drawingSession, Center, realOffset, CenterStack.Count)
                    Center += realOffset
                Case AscW("L") 'Leaf
                    If CenterStack.Count > 4 OrElse Rnd.NextDouble > CenterStack.Count * 0.1 + 0.1 Then Exit Select
                    DrawLeaf(drawingSession, Center, CenterStack.Count)
                Case AscW("R") 'Flower
                    If CenterStack.Count > 4 OrElse Rnd.NextDouble > CenterStack.Count * 0.1 + 0.1 Then Exit Select
                    DrawFlower(drawingSession, Center, CenterStack.Count)
                Case AscW("+")
                    Offset.Rotate(CSng(Math.PI / 6))
                Case AscW("-")
                    Offset.Rotate(-CSng(Math.PI / 6))
                Case AscW("[")
                    CenterStack.Push(Center)
                    OffsetStack.Push(Offset)
                Case AscW("]")
                    Center = CenterStack.Pop
                    Offset = OffsetStack.Pop
            End Select

            CurrentI = i + 1
            stepIndex += 1
            If stepIndex >= stepBound Then
                Exit For
            End If
        Next
    End Sub

    Private Sub DrawLineBranch(drawingSession As CanvasDrawingSession, center As Vector2, offset As Vector2, depth As Integer)
        Dim stroke As Single = (6 - depth) * 1.6F
        drawingSession.DrawLine(center, center + offset, Color.FromArgb(CByte(255 - depth * 10), 0, 0, 0), stroke)
    End Sub

    Private Sub DrawImageBranch(drawingSession As CanvasDrawingSession, center As Vector2, offset As Vector2, depth As Integer)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image As CanvasBitmap = DirectCast(ImageResource.GetResource(BranchResourceId), CanvasBitmap)
        Static SrcRect As Rect = Image.Bounds
        Static Ratio As Single = 1

        Dim branchWidth As Single = CSng(offset.Length * 0.8 * Ratio)
        Dim branchHeight As Single = offset.Length * Ratio
        Dim alpha As Single = CSng(RandomHelper.NextNorm(60, 90) / 100)

        drawingSession.Transform = Matrix3x2.CreateRotation(CSng(Math.Atan2(offset.Y, offset.X) - Math.PI / 2）, center)
        drawingSession.DrawImage(Image, New Rect(center.X, center.Y, branchWidth, branchHeight), SrcRect, alpha)
        drawingSession.Transform = Matrix3x2.CreateRotation(0)
    End Sub

    Private Sub DrawLeaf(drawingSession As CanvasDrawingSession, center As Vector2, depth As Integer)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image As CanvasBitmap = DirectCast(ImageResource.GetResource(LeafResourceId), CanvasBitmap)
        Static SrcRect As Rect = Image.Bounds
        Dim position As Vector2 = center
        Dim border As Single = (depth + 0) * ImageScale * RandomHelper.NextNorm(10, 400) / 100
        Dim alpha As Single = CSng(RandomHelper.NextNorm(20, 80) / 100)
        drawingSession.Transform *= Matrix3x2.CreateRotation(CSng(Rnd.NextDouble * Math.PI / 2 - Math.PI / 4), center)
        drawingSession.DrawImage(Image, New Rect(position.X - border, position.Y - border, border * 2, border * 2), SrcRect, alpha)
        drawingSession.Transform = Matrix3x2.Identity
    End Sub
    Private Sub DrawFlower(drawingSession As CanvasDrawingSession, center As Vector2, depth As Integer)
        Static ImageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource
        Static Image As CanvasBitmap = DirectCast(ImageResource.GetResource(FlowerResourceId), CanvasBitmap)
        Static SrcRect As Rect = Image.Bounds
        Dim position As Vector2 = center
        Dim border As Single = (depth + 0) * ImageScale * RandomHelper.NextNorm(100, 300) / 100
        Dim alpha As Single = CSng(RandomHelper.NextNorm(20, 80) / 100)
        drawingSession.Transform *= Matrix3x2.CreateRotation(CSng(Rnd.NextDouble * Math.PI / 2 - Math.PI / 4), center)
        drawingSession.DrawImage(Image, New Rect(position.X - border, position.Y - border, border * 2, border * 2), SrcRect, alpha)
        drawingSession.Transform = Matrix3x2.Identity
    End Sub
End Class