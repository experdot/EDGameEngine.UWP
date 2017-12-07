Imports EDGameEngine.Components.Behavior
Imports EDGameEngine.Core.Graphics
Imports EDGameEngine.Core.UI
''' <summary>
''' 游戏场景
''' </summary>
Public Class Scene1
    Inherits SceneWithUI
    Public Sub New(world As WorldWithUI, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Protected Overrides Sub CreateObject()

        Dim temp As New ActionGameModel
        AddGameVisual(temp, New ActionGameView(temp))

        '画面居中
        temp.GameComponents.Behaviors.Add(New TransformScript)
        '画面残影
        'temp.GameComponents.Effects.Add(New GhostEffect With {.Opacity = 0.96})
        '键盘控制摄像机
        'Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})

    End Sub

    Protected Overrides Sub CreateUI()
        Return
    End Sub

    Protected Overrides Async Function CreateResourcesAsync(imageResource As ImageResource) As Task
        Await imageResource.Add(BlockImageID.Blank, "Images/Brick_Blank.png")
        Await imageResource.Add(BlockImageID.Question, "Images/Brick_Question.png")
        Await imageResource.Add(CharacterImageID.Default, "Images/Hero.png")
    End Function

End Class
