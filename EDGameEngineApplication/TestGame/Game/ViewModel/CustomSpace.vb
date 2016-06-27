Imports System.Numerics
Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Windows.UI
''' <summary>
''' 初始化、更新可视化对象空间
''' </summary>
Public Class CustomSpace
    Inherits EDGameEngine.WorldManager
    Public Sub New(aw#, ah#)
        MyBase.New(aw, ah)
    End Sub
    Public Overrides Sub CreateObject()
        MyWorld.AddGameVisual(New ParticalManager)
        ' mySpace.AddGameVisual(New Plant(New Vector2(WorldSpace.SpaceWidth / 2, WorldSpace.SpaceHeight * 0.8)))
    End Sub
End Class
