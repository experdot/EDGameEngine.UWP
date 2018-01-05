' https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class Scenario5_Game
    Inherits Page
    Public Sub Start(id As Integer)
        GameBox.World = New CustomWorld(GameBox.ActualWidth, GameBox.ActualHeight, id)
    End Sub
End Class
