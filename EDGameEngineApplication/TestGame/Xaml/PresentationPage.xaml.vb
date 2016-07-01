' “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Windows.UI
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class PresentationPage
    Inherits Page
    Private Sub BtnBack_Click(sender As Object, e As RoutedEventArgs)
        If Frame.CanGoBack Then
            GC.Collect()
            Frame.GoBack()
        Else
            App.Current.Exit()
        End If
    End Sub
    Private Sub GameBox_Loaded(sender As Object, e As RoutedEventArgs) Handles GameBox.Loaded
        GameBox.World = New CustomWorld(GameBox.ActualWidth, GameBox.ActualHeight)
    End Sub
End Class
