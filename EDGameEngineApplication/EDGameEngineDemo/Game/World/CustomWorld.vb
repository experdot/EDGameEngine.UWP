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
    Public Overrides Sub CreateScene()
        CurrentScene = New Scene1(New Size(Width, Height))
    End Sub
End Class
