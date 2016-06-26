'“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private Sub BtnNav_Click(sender As Object, e As RoutedEventArgs)
        Frame.Navigate(GetType(PresentationPage))
    End Sub

    Private Sub MainPage_KeyDown(sender As Object, e As KeyRoutedEventArgs) Handles Me.KeyDown
        If e.Key = Windows.System.VirtualKey.F Then
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode()
        Else
            ApplicationView.GetForCurrentView().ExitFullScreenMode()
        End If
    End Sub
End Class
