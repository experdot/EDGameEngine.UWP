Imports Windows.UI.Core
''' <summary>
''' 用于创建多窗体的对象
''' </summary>
Public Class MultiWindow
    ''' <summary>
    ''' 创建一个新窗体
    ''' </summary>
    Public Shared Async Function CreateNewWindowAsync(sourcePageType As Type, Optional title As String = "") As Task
        Dim viewId As Integer
        Dim createwindow = Sub()
                               Dim frame = New Frame()
                               frame.Navigate(sourcePageType)
                               Window.Current.Content = frame
                               Window.Current.Activate()
                               Dim view = ApplicationView.GetForCurrentView()
                               view.Title = title
                               viewId = view.Id
                           End Sub
        Await Core.CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, createwindow)
        Try
            Await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
        End Try
    End Function
End Class
