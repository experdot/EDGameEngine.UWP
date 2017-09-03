Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Windows.System
''' <summary>
''' 游戏世界
''' </summary>
Public MustInherit Class World
    Implements IDisposable
    ''' <summary>
    ''' 图形资源创建器
    ''' </summary>
    Public Property ResourceCreator As ICanvasResourceCreator
    ''' <summary>
    ''' UI容器
    ''' </summary>
    Public Property UIContainer As Grid
    ''' <summary>
    ''' 宽度
    ''' </summary>
    Public Property Width As Integer
    ''' <summary>
    ''' 高度
    ''' </summary>
    Public Property Height As Integer
    ''' <summary>
    ''' 场景集合
    ''' </summary>
    Public Property Scenes As New Dictionary(Of String, Scene)
    ''' <summary>
    ''' 活动的场景
    ''' </summary>
    Public Property ActiveScene As IScene
    ''' <summary>
    ''' 渲染模式
    ''' </summary>
    Public Property RenderMode As RenderMode
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(ActualWidth#, ActualHeight#)
        OnSizeChanged(CInt(ActualWidth), CInt(ActualHeight))
    End Sub
    ''' <summary>
    ''' 开始
    ''' </summary>
    Public MustOverride Sub Start()
    ''' <summary>
    ''' 更新
    ''' </summary>
    Public Sub Update(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)
        If RenderMode = RenderMode.Async Then
            ActiveScene?.Update()
        End If
    End Sub
    ''' <summary>
    ''' 渲染
    ''' </summary>
    Public Sub Draw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        If RenderMode = RenderMode.Sync Then
            ActiveScene?.Update()
        End If
        ActiveScene?.OnDraw(args.DrawingSession)
    End Sub
    ''' <summary>
    ''' 切换至指定的场景
    ''' </summary>
    Public Sub SwitchScene(key As String, Optional reStart As Boolean = False)
        ActiveScene = Scenes.Item(key)
        If ActiveScene.State = SceneState.Wait OrElse reStart = True Then
            ActiveScene.Start()
        End If
    End Sub
    ''' <summary>
    ''' 世界大小改变时
    ''' </summary>
    Public Sub OnSizeChanged(sX As Integer, sY As Integer)
        Width = sX
        Height = sY
        If ActiveScene IsNot Nothing Then
            ActiveScene.Rect = New Rect(0, 0, sX, sY)
        End If
    End Sub
    ''' <summary>
    ''' 键按下时
    ''' </summary>
    Public Sub OnKeyDown(keycode As VirtualKey)
        ActiveScene?.Inputs.Keyboard.RaiseKeyDown(keycode)
    End Sub
    ''' <summary>
    ''' 键释放时
    ''' </summary>
    Public Sub OnKeyUp(keycode As VirtualKey)
        ActiveScene?.Inputs.Keyboard.RaiseKeyUp(keycode)
    End Sub
    ''' <summary>
    ''' 指针按下时
    ''' </summary>
    Public Sub OnPointerPressed(loc As Vector2)
        ActiveScene?.Inputs.Mouse.OnPointerPressed(loc)
    End Sub
    ''' <summary>
    ''' 指针释放时
    ''' </summary>
    Public Sub OnPointerReleased(loc As Vector2)
        ActiveScene?.Inputs.Mouse.OnPointerReleased(loc)
    End Sub
    ''' <summary>
    ''' 指针移动时
    ''' </summary>
    Public Sub OnPointerMove(loc As Vector2)
        ActiveScene?.Inputs.Mouse.OnPointerMoved(loc)
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                ActiveScene?.Dispose()
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
