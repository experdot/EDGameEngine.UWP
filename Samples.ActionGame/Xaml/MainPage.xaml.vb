'“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        ' Populate the scenario list from the SampleConfiguration.vb file
        ScenarioControl.Menu.ItemsSource = Scenarios
        ScenarioControl.Menu.OptionsItemsSource = Options
        ScenarioControl.ClickMenuItem(Scenarios(0))
    End Sub

End Class
