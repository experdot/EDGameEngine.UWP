﻿Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Windows.UI
Public Class Scene3
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()

        Scene.GameComponents.Behaviors.Add(New PhysicsScript)
        '键盘控制摄像机
        Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})

        '场景全局残影
        Me.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 0.6})

    End Sub

    Public Overrides Sub CreateUI()

    End Sub

    Public Overrides Function CreateResoucesAsync(imgRes As ImageResourceManager) As Task
        Throw New NotImplementedException()
    End Function
End Class
