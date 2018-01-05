'“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

Imports Microsoft.Toolkit.Uwp.UI.Controls
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        ' Populate the scenario list from the SampleConfiguration.vb file
        ScenarioControl.Menu.ItemsSource = Scenarios
        ScenarioControl.Menu.OptionsItemsSource = Options
        'ScenarioControl.Menu.SelectedItem = Scenarios(0)
        ScenarioControl.ClickMenuItem(CType(Scenarios(0), HamburgerMenuItem))
    End Sub

End Class
