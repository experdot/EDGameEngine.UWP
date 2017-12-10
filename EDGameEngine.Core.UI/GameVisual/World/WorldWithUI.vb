Imports System.Numerics
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Windows.System
''' <summary>
''' 游戏世界
''' </summary>
Public MustInherit Class WorldWithUI
    Implements IGameWorld, IObjectWithResourceCreator
    ''' <summary>
    ''' 宽度
    ''' </summary>
    Public Property Width As Integer Implements IGameWorld.Width
    ''' <summary>
    ''' 高度
    ''' </summary>
    Public Property Height As Integer Implements IGameWorld.Height
    ''' <summary>
    ''' 场景集合
    ''' </summary>
    Public Property Scenes As New Dictionary(Of String, IScene) Implements IGameWorld.Scenes
    ''' <summary>
    ''' 活动的场景
    ''' </summary>
    Public Property ActiveScene As IScene Implements IGameWorld.ActiveScene
    ''' <summary>
    ''' 渲染模式
    ''' </summary>
    Public Property RenderMode As RenderMode Implements IGameWorld.RenderMode
    ''' <summary>
    ''' 图形资源创建器
    ''' </summary>
    Public Property ResourceCreator As ICanvasResourceCreator Implements IObjectWithResourceCreator.ResourceCreator
    ''' <summary>
    ''' UI容器
    ''' </summary>
    Public Property UIContainer As Grid

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(ActualWidth#, ActualHeight#)
        OnSizeChanged(CInt(ActualWidth), CInt(ActualHeight))
    End Sub
    ''' <summary>
    ''' 开始
    ''' </summary>
    Public MustOverride Sub Start() Implements IGameWorld.Start
    ''' <summary>
    ''' 开始
    ''' </summary>
    Public MustOverride Sub Update() Implements IGameWorld.Update
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
        CType(ActiveScene.Presenter, ICanvasView)?.BeginDraw(args.DrawingSession)
    End Sub
    ''' <summary>
    ''' 切换至指定的场景
    ''' </summary>
    Public Sub SwitchScene(key As String, Optional reStart As Boolean = False) Implements IGameWorld.SwitchScene
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
End Class
