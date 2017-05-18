Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Text
Imports Windows.UI
''' <summary>
''' 游戏场景基类
''' </summary>
Public MustInherit Class Scene
    Implements IScene

    Public Property ImageManager As ImageResourceManager Implements IScene.ImageManager
    Public Property GameLayers As New List(Of ILayer) Implements IScene.GameLayers
    Public ReadOnly Property Width As Single Implements IScene.Width
        Get
            Return CSng(Rect.Width)
        End Get
    End Property
    Public ReadOnly Property Height As Single Implements IScene.Height
        Get
            Return CSng(Rect.Height)
        End Get
    End Property
    Public Property World As World Implements IScene.World
    Public Property Camera As ICamera Implements IScene.Camera
    Public Property Inputs As Inputs Implements IScene.Inputs
    Public Property State As SceneState Implements IScene.State
    Public Property Progress As Progress Implements IScene.Progress
    Public Property Scene As IScene = Me Implements IGameVisual.Scene
    Public Property Transform As Transform Implements IGameVisual.Transform
        Get
            Return Camera.Transform
        End Get
        Set(value As Transform)
            Camera.Transform = value
        End Set
    End Property
    Public Property Appearance As Appearance Implements IGameVisual.Appearance
        Get
            Return Camera.Appearance
        End Get
        Set(value As Appearance)
            Camera.Appearance = value
        End Set
    End Property
    Public Property GameComponents As GameComponents Implements IGameVisual.GameComponents
    Public Property Presenter As IGameView = New SceneView(Me) Implements IGameVisual.Presenter
    Public Property Rect As Rect Implements IGameVisual.Rect

    Public MustOverride Function CreateResoucesAsync(imgRes As ImageResourceManager) As Task
    Public MustOverride Sub CreateObject()
    Public MustOverride Sub CreateUI()

    Dim ModifyActions As New List(Of Action)

    Public Sub New(world As World, WindowSize As Size)
        Me.World = world
        Me.Inputs = New Inputs
        Me.Camera = New Camera With {.Scene = Me}
        Me.GameComponents = New GameComponents(Me)
        Me.Progress = New Progress(0, "")
        Me.State = SceneState.Wait
        Rect = New Rect(0, 0, WindowSize.Width, WindowSize.Height)
    End Sub

    Public Async Sub Start() Implements IGameObject.Start
        While World.ResourceCreator Is Nothing
            Await Task.Delay(10)
        End While
        Progress = New Progress(0.1, "加载外部资源")
        Await LoadAsync(World.ResourceCreator)
        Await Task.Run(Async Function()
                           Progress.Description = "创建实体"
                           CreateObject()
                           Await World.UIContainer.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                                                                                                    Sub()
                                                                                                        CreateUI()
                                                                                                    End Sub)
                           Progress.Description = "初始化场景"
                           For Each SubLayer In GameLayers
                               SubLayer.Start()
                           Next
                           Camera.Start()
                           Progress.Description = "初始化组件"
                           GameComponents.Start()
                           State = SceneState.Loop
                       End Function)
    End Sub

    Public Sub Update() Implements IScene.Update
        If State = SceneState.Loop Then
            For Each SubLayer In GameLayers
                SubLayer.Update()
            Next
            Camera.Update()
            GameComponents.Update()
            For Each SubAction In ModifyActions
                SubAction.Invoke
            Next
            ModifyActions.Clear()
        End If
    End Sub
    Public Sub AddUIElement(element As UIElement, Optional LayerIndex As Integer = 0)
        Dim modifyAct = Sub()
                            While (GameLayers.Count <= LayerIndex)
                                GameLayers.Add(New ControlLayer With {.Scene = Me})
                            End While
                            CType(GameLayers(LayerIndex), ControlLayer).AddControl(element)
                        End Sub
        If State = SceneState.Loop Then
            ModifyActions.Add(modifyAct)
        Else
            modifyAct.Invoke()
        End If
    End Sub
    Public Sub AddGameVisual(model As IGameBody, view As IGameView, Optional LayerIndex As Integer = 0) Implements IScene.AddGameVisual
        model.Scene = Me
        model.Presenter = view
        Dim modifyAct = Sub()
                            While (GameLayers.Count <= LayerIndex)
                                GameLayers.Add(New Layer With {.Scene = Me})
                            End While
                            GameLayers(LayerIndex).GameBodys.Add(model)
                            If Not State = SceneState.Wait Then
                                model.Start()
                            End If
                        End Sub
        If State = SceneState.Loop Then
            ModifyActions.Add(modifyAct)
        Else
            modifyAct.Invoke()
        End If
    End Sub
    Public Async Function LoadAsync(resourceCreator As ICanvasResourceCreator) As Task Implements IScene.LoadAsync
        ImageManager = New ImageResourceManager(resourceCreator)
        Await CreateResoucesAsync(ImageManager)
    End Function
    Public Sub OnDraw(drawingSession As CanvasDrawingSession) Implements IScene.OnDraw
        If State = SceneState.Loop Then
            LoadedDraw(drawingSession)
        Else
            LoadingDraw(drawingSession)
        End If
    End Sub
    Public Overridable Sub LoadingDraw(drawingSession As CanvasDrawingSession)
        Static Dots As String() = {" ", ".", "..", "..."}
        Static Index As Integer
        Index = (Index + 1) Mod 80
        drawingSession.DrawText("场景加载中，请稍后" & Dots(CInt(Math.Truncate(Index / 20))), New Vector2(Width, Height) / 2, Colors.Black, TextFormat.Center)
        drawingSession.DrawText(Progress.Description, New Vector2(Width, Height + 50) / 2, Colors.Black, TextFormat.CenterL)
    End Sub
    Public Overridable Sub LoadedDraw(drawingSession As CanvasDrawingSession)
        Presenter?.BeginDraw(drawingSession)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用
    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                ImageManager?.Dispose()
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
    Public Sub Dispose() Implements IScene.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
