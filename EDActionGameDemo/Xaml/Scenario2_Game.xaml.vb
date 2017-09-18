﻿' “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class Scenario2_Game
    Inherits Page
    Private Sub GameBox_Loaded(sender As Object, e As RoutedEventArgs) Handles GameBox.Loaded
        GameBox.World = New CustomWorld(GameBox.ActualWidth, GameBox.ActualHeight)
        GameBox.World.Start()
    End Sub
End Class
