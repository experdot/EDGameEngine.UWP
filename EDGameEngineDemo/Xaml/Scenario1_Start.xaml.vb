' “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class Scenario1_Start
    Inherits Page

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TryEnterFullScreenMode()
        MainPage.Current.SetHeaderPanelVisible(Visibility.Collapsed)
        MainPage.Current.SelectScenario(1)
    End Sub
End Class
