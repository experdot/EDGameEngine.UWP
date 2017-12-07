Imports EDGameEngine.Core
Imports EDGameEngine.Core.UI
''' <summary>
''' 自定义游戏世界
''' </summary>
Public Class CustomWorld
    Inherits WorldWithUI
    Public Sub New(aw#, ah#)
        MyBase.New(aw, ah)
    End Sub
    Public Overrides Sub Start()
        Scenes.Add("Main", New Scene1(Me, New Size(Width, Height)))
        SwitchScene("Main")
    End Sub

    Public Overrides Sub Update()

    End Sub
End Class
