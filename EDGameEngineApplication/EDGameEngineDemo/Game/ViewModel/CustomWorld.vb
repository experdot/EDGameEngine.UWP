Imports System.Numerics
Imports EDGameEngine
''' <summary>
''' 初始化、更新可视化对象空间
''' </summary>
Public Class CustomWorld
    Inherits World
    Public Sub New(aw#, ah#)
        MyBase.New(aw, ah)
    End Sub
    Dim gho
    Public Overrides Sub CreateObject()
        'Dim size As Single = 20
        'Dim border As Single = size * 1.382
        'For i = -3 To 3
        '    For j = -3 To 3
        '        Dim rect As New Rect(-10 - i * border, -10 + j * border, size, size)
        '        Dim rectModel As New VisualRectangle(rect)
        '        MyScene.AddGameVisual(rectModel, New RectangleView(rectModel))
        '    Next
        'Next

        'Dim tempModel As New ParticalFollow()
        'MyScene.AddGameVisual(tempModel, New ParticalView(tempModel))

        'Dim tempModel2 As New Plant(New Vector2(MyScene.Width / 2, MyScene.Height * 0.8))
        'MyScene.AddGameVisual(tempModel2, New PlantView(tempModel2))

        Dim tempModel3 As New AutoDrawModel() With {.Image = MyScene.ImageManager.GetResource(ImageResourceID.Scenery1)}
        MyScene.AddGameVisual(tempModel3, New AutoDrawView(tempModel3))
    End Sub
End Class
