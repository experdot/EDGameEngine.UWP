' The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.UI.Popups

Public NotInheritable Class HamburgerBox
    Inherits UserControl
    ''' <summary>
    ''' 汉堡菜单
    ''' </summary>
    Public ReadOnly Property Menu As HamburgerMenu
        Get
            Return HamburgerMenuControl
        End Get
    End Property
    ''' <summary>
    ''' 限定最小宽度
    ''' </summary>
    Public Property MinmumWidth As Integer = 640
    ''' <summary>
    ''' 限定最大宽度
    ''' </summary>
    Public Property MaxmumWidth As Integer = 1080
    ''' <summary>
    ''' 连续点击同一选项卡时，是否重新加载内容
    ''' </summary>
    Public Property IsReload As Boolean = False

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Menu.OpenPaneLength = 180
    End Sub

    Public Sub ClickMenuItem(item As HamburgerMenuItem, Optional isOption As Boolean = False)
        ContentGrid.DataContext = item
        If item IsNot Nothing Then
            If IsReload OrElse (item IsNot Menu.SelectedItem AndAlso item IsNot Menu.SelectedOptionsItem) Then
                ScenarioFrame.Navigate(item.TargetPageType)
            End If

            If isOption Then
                Menu.SelectedOptionsItem = item
            Else
                Menu.SelectedItem = item
            End If
        End If

        If Window.Current.Bounds.Width < MinmumWidth Then
            Menu.IsPaneOpen = False
        End If
    End Sub

    Private Sub HamburgerBox_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        If Window.Current.Bounds.Width < MinmumWidth Then
            Menu.IsPaneOpen = False
        ElseIf Window.Current.Bounds.Width > MaxmumWidth Then
            Menu.IsPaneOpen = True
        End If
    End Sub
    Private Sub HamburgerMenuControl_ItemClick(sender As Object, e As ItemClickEventArgs) Handles HamburgerMenuControl.ItemClick
        ClickMenuItem(TryCast(e.ClickedItem, HamburgerMenuItem))
    End Sub
    Private Sub HamburgerMenuControl_OptionsItemClick(sender As Object, e As ItemClickEventArgs) Handles HamburgerMenuControl.OptionsItemClick
        ClickMenuItem(TryCast(e.ClickedItem, HamburgerMenuItem))
    End Sub
End Class
