Imports EDGameEngine.Core
''' <summary>
''' 用户游戏世界
''' </summary>
Public Class CustomWorld
    Inherits World
    Public Sub New(aw#, ah#)
        MyBase.New(aw, ah)
    End Sub
    Public Overrides Sub Start()
        Scenes.Add("Main", New Scene1(Me, New Size(Width, Height)))
        Scenes.Add("Test", New Scene2(Me, New Size(Width, Height)))
        Scenes.Add("Physic", New Scene3(Me, New Size(Width, Height)))
        SwitchScene("Physic")
    End Sub
End Class
