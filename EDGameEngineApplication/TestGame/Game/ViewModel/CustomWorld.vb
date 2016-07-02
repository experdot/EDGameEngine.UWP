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
        'Dim rect As New Rect(MyScene.Width / 2 - 100, MyScene.Height / 2 - 100, 200, 200)
        MyScene.AddGameVisual(New ParticalManager(MyScene))
        'MyScene.AddGameVisual(New VisualRectangle(rect))
        'MyScene.AddGameVisual(New VisualLine(New Vector2() {New Vector2(100, 100),
        '                                                    New Vector2(400, 300)}))
        MyScene.AddGameVisual(New Plant(New Vector2(MyScene.Width / 2, MyScene.Height * 0.8)))
    End Sub
End Class
