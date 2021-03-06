Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Utilities
Imports EDGameEngine.Visuals
''' <summary>
''' 植物模型
''' </summary>
Public Class Plant
    Inherits GameBody
    Implements IPlant
    Public Property Root As TreeNode Implements IPlant.Root
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(location As Vector2, Optional rank As Integer = 8)
        Root = New TreeNode(New Vector2(0, -1), 200, rank) With
        {
            .RealLocation = location
        }
        Root.Generate(True)
    End Sub
    Public Overrides Sub StartEx()
        Flower.Rnd = New Random()
    End Sub
    Public Overrides Sub UpdateEx()
        Static TempSingle As Single
        TempSingle = CSng((TempSingle + 0.05) Mod (Math.PI * 2)）
        Root.RealLocation = New Vector2(Scene.Width / 2, CSng(Scene.Height * 0.8)）
        Root.GrowUp(0.01)
        Root.Wave(CSng(Math.Sin(TempSingle) / 1000))
    End Sub
End Class
