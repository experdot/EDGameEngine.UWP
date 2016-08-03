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

        'For i = -5 To 5
        '    For j = -5 To 5
        '        Dim rect As New Rect(-10 - i * 24, -10 + j * 24, 20, 20)
        '        MyScene.AddGameVisual(New VisualRectangle(rect))
        '    Next
        'Next
        MyScene.AddGameVisual(New ParticalManager(MyScene))
        MyScene.AddGameVisual(New VisualLine())
        ' MyScene.AddGameVisual(New Plant(New Vector2(MyScene.Width / 2, MyScene.Height * 0.8)))
    End Sub
End Class
