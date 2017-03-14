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

        Dim temp As New ActionGameModel
        temp.GameComponents.Behaviors.Add(New TransformScript)
        AddGameVisual(temp, New ActionGameView(temp))

        '键盘控制摄像机
        Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
        '场景全局残影
        'Me.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 0.96})
    End Sub

    Public Overrides Sub CreateUI()

    End Sub

    Public Overrides Async Function CreateResouces(imgRes As ImageResourceManager) As Task
        Await imgRes.Add(BlockImageID.Blank, "Images/Brick_Blank.png")
        Await imgRes.Add(BlockImageID.Question, "Images/Brick_Question.png")
        Await imgRes.Add(CharacterImageID.Default, "Images/Hero.png")
    End Function

End Class
