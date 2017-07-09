''' <summary>
''' 游戏启动器
''' </summary>
Public Class GameStarter
    Public Shared Async Sub Start(Optional title As String = "")
        Await MultiWindow.CreateNewWindowAsync(GetType(Scenario5_Game), title)
    End Sub
End Class
