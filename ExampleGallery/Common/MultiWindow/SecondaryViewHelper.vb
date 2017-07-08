'
' * SecondaryViewHelper - 自定义的一个帮助类，用于简化 SecondaryView 的管理
' 


Imports System.ComponentModel
Imports Windows.UI.Core
Imports Windows.UI.ViewManagement

Namespace Windows10.UI.MultipleViews
    Public Class SecondaryViewHelper
        Implements INotifyPropertyChanged
        ' for INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler

        ' 当前 SecondaryView 的 CoreDispatcher
        Private _dispatcher As CoreDispatcher
        ' 当前 SecondaryView 的 ApplicationView
        Private _applicationView As ApplicationView

        ' 当前 SecondaryView 的标题
        Private _title As String
        ' 当前 SecondaryView 的窗口标识
        Private _viewId As Integer

        ' 当前 SecondaryView 被引用的次数
        Private _refCount As Integer = 0
        ' 当前 SecondaryView 是否已经被释放
        Private _released As Boolean = False

        ' 禁止通过 new 实例化
        Private Sub New(newWindow As CoreWindow)
            _dispatcher = newWindow.Dispatcher
            _viewId = ApplicationView.GetApplicationViewIdForWindow(newWindow)

            _applicationView = ApplicationView.GetForCurrentView()

            RegisterForEvents()
        End Sub

        ' 实例化 SecondaryViewHelper
        Public Shared Function CreateForCurrentView() As SecondaryViewHelper
            '
            '             * CoreWindow.GetForCurrentThread() - 获取当前窗口的 CoreWindow
            '             

            Return New SecondaryViewHelper(CoreWindow.GetForCurrentThread())
        End Function
        Private Sub RegisterForEvents()
            '
            '             * ApplicationView.GetForCurrentView() - 获取当前窗口的 ApplicationView
            '             * ApplicationView.Consolidated - 当前 app 存活着两个或两个以上的窗口时，此窗口关闭后触发的事件
            '             

            AddHandler ApplicationView.GetForCurrentView().Consolidated, AddressOf SecondaryViewHelper_Consolidated
        End Sub

        Private Sub UnregisterForEvents()
            RemoveHandler ApplicationView.GetForCurrentView().Consolidated, AddressOf SecondaryViewHelper_Consolidated
        End Sub

        Private Sub SecondaryViewHelper_Consolidated(sender As ApplicationView, args As ApplicationViewConsolidatedEventArgs)
            StopViewInUse()
        End Sub

        ' 当前 SecondaryView 开始使用了（与 StopViewInUse() 成对）
        ' 因为每一个窗口都可以被同 app 的别的窗口调用，而每一个窗口又都是一个独立的线程，所以要做好线程处理
        Public Function StartViewInUse() As Integer
            Dim releasedCopy As Boolean = False
            Dim refCountCopy As Integer = 0

            SyncLock Me
                releasedCopy = _released
                If Not _released Then
                    refCountCopy = System.Threading.Interlocked.Increment(_refCount)
                End If
            End SyncLock

            If releasedCopy Then
                Throw New InvalidOperationException("this view is being disposed")
            End If

            Return refCountCopy
        End Function

        ' 当前 SecondaryView 结束使用了（与 StartViewInUse() 成对）
        ' 因为每一个窗口都可以被同 app 的别的窗口调用，而每一个窗口又都是一个独立的线程，所以要做好线程处理
        Public Function StopViewInUse() As Integer
            Dim refCountCopy As Integer = 0
            Dim releasedCopy As Boolean = False

            SyncLock Me
                releasedCopy = _released
                If Not _released Then
                    refCountCopy = System.Threading.Interlocked.Decrement(_refCount)
                    If refCountCopy = 0 Then
                        ' 当前 SecondaryView 不再被任何人需要了，清理之
                        Dim task = _dispatcher.RunAsync(CoreDispatcherPriority.Low, AddressOf FinalizeRelease)
                    End If
                End If
            End SyncLock

            If releasedCopy Then
                Throw New InvalidOperationException("this view is being disposed")
            End If

            Return refCountCopy
        End Function

        ' 清理当前 SecondaryView
        Private Sub FinalizeRelease()
            Dim justReleased As Boolean = False
            SyncLock Me
                If _refCount = 0 Then
                    justReleased = True
                    _released = True
                End If
            End SyncLock

            If justReleased Then
                UnregisterForEvents()

                ' 触发 Released 事件
                OnReleased(EventArgs.Empty)
            End If
        End Sub

        ' 定义 Released 事件
        Public Event Released As EventHandler(Of EventArgs)
        Private Event INotifyPropertyChanged_PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Overridable Sub OnReleased(e As EventArgs)
            RaiseEvent Released(Me, e)
        End Sub

        Public ReadOnly Property Id() As Integer
            Get
                Return _viewId
            End Get
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set
                If _title <> Value Then
                    _title = Value

                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Title)))
                End If
            End Set
        End Property

        Public ReadOnly Property IsReleased() As Boolean
            Get
                Return _released
            End Get
        End Property

        Public ReadOnly Property ApplicationView() As ApplicationView
            Get
                Return _applicationView
            End Get
        End Property
    End Class
End Namespace
