Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
Imports System.Text
Imports System.Numerics
Imports Windows.UI
''' <summary>
''' L系统树模型的视图
''' </summary>
Public Class LSystemTreeView
    Inherits TypedGameView(Of IStateMachine)
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

    Public Sub New(Target As IStateMachine)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)

        Static Rnd As New Random
        Static CurrentI As Integer = 0

        If CurrentI >= Target.States.Count Then Return

        Static Len As Single = -20.0F
        Static Center As Vector2 = New Vector2(Target.Scene.Width / 2, Target.Scene.Height * 0.99F)
        Static Offset As New Vector2(0, Len)
        Static CenterStack As New Stack(Of Vector2)
        Static OffsetStack As New Stack(Of Vector2)

        Dim imageLeaf As CanvasBitmap = DirectCast(Target.Scene.ImageManager.GetResource(LeafResourceId), CanvasBitmap)
        Dim imageFlower As CanvasBitmap = DirectCast(Target.Scene.ImageManager.GetResource(FlowerResourceId), CanvasBitmap)
        Dim srcRectLeaf As Rect = imageLeaf.Bounds
        Dim srcRectFlower As Rect = imageFlower.Bounds

        Dim stepIndex As Integer = 0
        Dim stepBound As Integer = 1000

        Dim subState As State
        For i = CurrentI To Target.States.Count - 1
            subState = Target.States(i)
            Select Case subState.Id
                Case AscW("F")
                    Dim realOffset = Offset * CSng(Math.Log(Math.E + (5 - CenterStack.Count)) * 0.5) * CSng(Rnd.NextDouble)
                    Dim stroke As Single = (6 - CenterStack.Count) * 1.6F
                    drawingSession.DrawLine(Center, Center + realOffset, Color.FromArgb(CByte(255 - CenterStack.Count * 10), 0, 0, 0), stroke)
                    Center += realOffset
                Case AscW("L") 'Leaf
                    If CenterStack.Count > 4 OrElse Rnd.NextDouble > CenterStack.Count * 0.1 + 0.6 Then Exit Select
                    Dim tempV As Vector2 = Center
                    Dim Border As Single = (CenterStack.Count + 0) * ImageScale * RandomHelper.NextNorm(100, 300) / 100
                    Dim alpha As Single = CSng(RandomHelper.NextNorm(20, 120) / 100) + 50
                    drawingSession.Transform *= Matrix3x2.CreateRotation(CSng(Rnd.NextDouble * Math.PI / 3 - Math.PI / 6), Center)
                    drawingSession.DrawImage(imageLeaf, New Rect(tempV.X - Border, tempV.Y - Border, Border * 2, Border * 2), srcRectLeaf, alpha)
                    drawingSession.Transform = Matrix3x2.Identity
                Case AscW("R") 'Flower
                    If CenterStack.Count > 4 OrElse Rnd.NextDouble > CenterStack.Count * 0.2 + 0.2 Then Exit Select
                    Dim tempV As Vector2 = Center
                    Dim Border As Single = (CenterStack.Count + 0) * ImageScale * RandomHelper.NextNorm(10, 400) / 100
                    Dim alpha As Single = CSng(RandomHelper.NextNorm(20, 120) / 100) + 20
                    drawingSession.Transform *= Matrix3x2.CreateRotation(CSng(Rnd.NextDouble * Math.PI / 3 - Math.PI / 6), Center)
                    drawingSession.DrawImage(imageFlower, New Rect(tempV.X - Border, tempV.Y - Border, Border * 2, Border * 2), srcRectFlower, alpha)
                    drawingSession.Transform = Matrix3x2.Identity
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
End Class