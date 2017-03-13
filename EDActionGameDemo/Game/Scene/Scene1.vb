Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
Public Class Scene1
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        Scene.GameComponents.Behaviors.Add(New PhysicsScript)
        '创建物体脚本
        'Me.GameComponents.Behaviors.Add(New CreateBodyScript())
        '键盘控制摄像机
        'Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
        'Me.GameComponents.Effects.Add(New StreamEffect)
        '场景全局残影
        Me.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 0.9})
    End Sub

    Public Overrides Sub CreateUI()

    End Sub

    Public Overrides Function CreateResouces(imgRes As ImageResourceManager) As Task
        'Await imgRes.Add(ImageResourceID.TreeBranch1, "Image/Tree_Black.png")
    End Function

End Class
