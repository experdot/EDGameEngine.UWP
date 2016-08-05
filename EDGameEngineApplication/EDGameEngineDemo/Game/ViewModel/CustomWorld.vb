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
    Public Overrides Sub CreateObject()
        'For i = -3 To 3
        '    For j = -3 To 3
        '        Dim rect As New Rect(-10 - i * 24, -10 + j * 24, 20, 20)
        '        Dim rectModel As New VisualRectangle(rect)
        '        MyScene.AddGameVisual(rectModel, New RectangleView(rectModel))
        '    Next
        'Next

        'Dim tempModel As New ParticalFollow()
        'MyScene.AddGameVisual(tempModel, New ParticalView(tempModel))

        Dim tempModel2 As New Plant(New Vector2(MyScene.Width / 2, MyScene.Height * 0.8))
        MyScene.AddGameVisual(tempModel2, New PlantView(tempModel2))
    End Sub
End Class
