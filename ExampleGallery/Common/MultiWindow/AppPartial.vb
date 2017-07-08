Imports Windows.ApplicationModel.Activation
Imports Windows.UI.Core
Imports Windows.UI.ViewManagement
Imports Windows.UI.Xaml
Imports Windows.UI.Xaml.Controls

Namespace Windows10
    Partial Public Class App
        ' PrimaryView 的 CoreDispatcher
        Private _mainDispatcher As CoreDispatcher
        ' PrimaryView 的窗口标识
        Private _mainViewId As Integer

        '' partial method，实现了 App.xaml.cs 中的声明
        'Partial Private Sub OnLaunched_MultipleViews(args As LaunchActivatedEventArgs)
        '    _mainDispatcher = Window.Current.Dispatcher
        '    _mainViewId = ApplicationView.GetForCurrentView().Id
        'End Sub

        Public ReadOnly Property MainDispatcher() As CoreDispatcher
            Get
                Return _mainDispatcher
            End Get
        End Property

        Public ReadOnly Property MainViewId() As Integer
            Get
                Return _mainViewId
            End Get
        End Property
    End Class
End Namespace

