Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 指针跟随脚本
''' </summary>
Public Class PointerFollowScript
    Inherits BehaviorBase
    Public Overrides Sub Start()
        RemoveHandler Scene.Inputs.Mouse.MouseChanged, AddressOf MouseChanged
        AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf MouseChanged
    End Sub

    Public Overrides Sub Update()

    End Sub

    Private Sub MouseChanged(loc As Vector2)
        Me.Target.Transform.Translation = loc - Scene.Camera.Position
    End Sub
End Class
