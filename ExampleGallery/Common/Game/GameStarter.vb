Imports ExampleGallery.Windows10.UI.MultipleViews
Imports Windows.UI.Core
''' <summary>
''' 游戏启动器
''' </summary>
Public Class GameStarter
    Shared SecondaryViewHelper As SecondaryViewHelper

    Public Shared Async Sub Start(Optional title As String = "")
        Await CreateNewWindowAsync(GetType(Scenario5_Game), title)
    End Sub

    Private Shared Async Function CreateNewWindowAsync(sourcePageType As Type, Optional title As String = "") As Task
        ' * CoreApplication.CreateNewView() - 创建一个新的 SecondaryView（只是新建一个 SecondaryView 实例，并不会显示出来）        
        Dim createwindow = Sub()
                               SecondaryViewHelper = SecondaryViewHelper.CreateForCurrentView()
                               SecondaryViewHelper.Title = title
                               SecondaryViewHelper.StartViewInUse()
                               Dim frame = New Frame()
                               frame.Navigate(sourcePageType, SecondaryViewHelper)
                               Window.Current.Content = frame
                               Window.Current.Activate()
                               ' 这里通过 ApplicationView.GetForCurrentView() 获取到的是新开窗口的 ApplicationView 对象
                               Dim secondaryView As ApplicationView = ApplicationView.GetForCurrentView()
                               secondaryView.Title = title
                           End Sub
        Await Core.CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, createwindow)
        Try
            SecondaryViewHelper.StartViewInUse()
            ApplicationViewSwitcher.DisableShowingMainViewOnActivation()
            Dim viewShown = Await ApplicationViewSwitcher.TryShowAsStandaloneAsync(SecondaryViewHelper.Id, ViewSizePreference.[Default], ApplicationView.GetForCurrentView().Id, ViewSizePreference.[Default])
            If Not viewShown Then
                Debug.WriteLine("显示 SecondaryView 失败")
            End If
            SecondaryViewHelper.StopViewInUse()
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
        End Try
    End Function
End Class
