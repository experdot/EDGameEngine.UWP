Imports EDGameEngine.Core
''' <summary>
''' 游戏场景基类
''' </summary>
Public MustInherit Class SceneBase
    Implements IScene
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
    Public Property World As IWorld Implements IScene.World
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
    Public Property Presenter As IGameView Implements IGameVisual.Presenter
    Public Property Rect As Rect Implements IGameVisual.Rect

    Protected MustOverride Async Function CreateGameObjectsAsync() As Task
    Protected MustOverride Sub CreateSceneView()
    Protected MustOverride Function GetDefaultLayer() As ILayer

    Private ModifyActions As New List(Of Action)

    Public Sub New(world As IWorld, size As Size)
        Me.World = world
        Me.Inputs = New Inputs
        Me.Camera = New Camera With {.Scene = Me}
        Me.GameComponents = New GameComponents(Me)
        Me.Progress = New Progress(0, "")
        Me.State = SceneState.Wait
        Rect = New Rect(0, 0, size.Width, size.Height)
        CreateSceneView()
    End Sub

    Public Async Sub Start() Implements IGameObject.Start
        Await Task.Run(Async Function()
                           Progress = New Progress(0, "创建实体")
                           Await CreateGameObjectsAsync()
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
    Public Sub AddGameVisual(model As IGameBody, view As IGameView, Optional LayerIndex As Integer = 0) Implements IScene.AddGameVisual
        model.Scene = Me
        model.AttachGameView(view)
        Dim modifyAct = Sub()
                            While (GameLayers.Count <= LayerIndex)
                                GameLayers.Add(GetDefaultLayer())
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
    Public Sub AttachGameView(view As IGameView) Implements IGameVisual.AttachGameView
        Me.Presenter = view
        view.GameVisual = Me
    End Sub
End Class
