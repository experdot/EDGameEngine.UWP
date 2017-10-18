Imports EDGameEngine.Core
Imports EDGameEngine.Core.UI
''' <summary>
''' 用户游戏世界
''' </summary>
Public Class CustomWorld
    Inherits WorldWithUI
    Public ReadOnly Property Id As Integer
    Public Sub New(aw#, ah#, id As Integer)
        MyBase.New(aw, ah)
        Me.Id = id
        Start()
    End Sub
    Public Overrides Sub Start()
        If Id >= 10000 Then
            Scenes.Add("Main", New Scene_Visuals(Me, New Size(Width, Height), Id))
        Else
            Scenes.Add("Main", New Scene_Compnents(Me, New Size(Width, Height), Id))
        End If
        SwitchScene("Main")
    End Sub

    Public Overrides Sub Update()

    End Sub
End Class
