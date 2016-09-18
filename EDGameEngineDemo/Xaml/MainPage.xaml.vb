'“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Public Shared Current As MainPage
    Public Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Current = Me
        SampleTitle.Text = FEATURE_NAME
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        ' Populate the scenario list from the SampleConfiguration.cs file
        ScenarioControl.ItemsSource = Scenarios
        If Window.Current.Bounds.Width < 640 Then
            ScenarioControl.SelectedIndex = -1
        Else
            ScenarioControl.SelectedIndex = 0
        End If
    End Sub
    Private Sub ScenarioControl_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        ' Clear the status block when navigating scenarios.
        NotifyUser([String].Empty, NotifyType.StatusMessage)

        Dim scenarioListBox As ListBox = TryCast(sender, ListBox)
        Dim s As Scenario = TryCast(scenarioListBox.SelectedItem, Scenario)
        If s IsNot Nothing Then
            ScenarioFrame.Navigate(s.ClassType)
            If Window.Current.Bounds.Width < 640 Then
                Splitter.IsPaneOpen = False
            End If
        End If
    End Sub

    Private Sub ToggleButton_Click(sender As Object, e As RoutedEventArgs)
        Splitter.IsPaneOpen = Not Splitter.IsPaneOpen
    End Sub
    Private Sub PrivacyLink_Click(sender As Object, e As RoutedEventArgs)

    End Sub
    Private Sub HyperlinkButton_Click(sender As Object, e As RoutedEventArgs)

    End Sub
    Public Sub NotifyUser(strMessage As String, type As NotifyType)
        Select Case type
            Case NotifyType.StatusMessage
                StatusBorder.Background = New SolidColorBrush(Windows.UI.Colors.Green)
                Exit Select
            Case NotifyType.ErrorMessage
                StatusBorder.Background = New SolidColorBrush(Windows.UI.Colors.Red)
                Exit Select
        End Select
        StatusBlock.Text = strMessage
        ' Collapse the StatusBlock if it has no text to conserve real estate.
        StatusBorder.Visibility = If((StatusBlock.Text <> [String].Empty), Visibility.Visible, Visibility.Collapsed)
        If StatusBlock.Text <> [String].Empty Then
            StatusBorder.Visibility = Visibility.Visible
            StatusPanel.Visibility = Visibility.Visible
        Else
            StatusBorder.Visibility = Visibility.Collapsed
            StatusPanel.Visibility = Visibility.Collapsed
        End If
    End Sub

    Private Sub MainPage_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        SampleTitle.Text = "Sample " & Grid1.RenderSize.ToString & Me.FocusState
    End Sub

    Public Enum NotifyType
        StatusMessage
        ErrorMessage
    End Enum
End Class
