Imports EDGameEngine.Core
''' <summary>
''' 游戏场景基类
''' </summary>
Public MustInherit Class SceneBase
    Inherits GameVisualBase
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
    Public Property World As IGameWorld Implements IScene.World
    Public Property Camera As ICamera Implements IScene.Camera
    Public Property Inputs As Inputs Implements IScene.Inputs
    Public Property State As SceneState Implements IScene.State
    Public Property Progress As Progress Implements IScene.Progress
    Public Overrides Property Transform As Transform
        Get
            Return Camera.Transform
        End Get
        Set(value As Transform)
            If Camera IsNot Nothing Then
                Camera.Transform = value
            End If
        End Set
    End Property
    Public Overrides Property Appearance As Appearance
        Get
            Return Camera.Appearance
        End Get
        Set(value As Appearance)
            If Camera IsNot Nothing Then
                Camera.Appearance = value
            End If
        End Set
    End Property

    Protected MustOverride Async Function CreateGameObjectsAsync() As Task
    Protected MustOverride Sub CreateSceneView()
    Protected MustOverride Function GetDefaultLayer() As ILayer

    Private ModifiedActions As New List(Of Action)

    Public Sub New(world As IGameWorld, size As Size)
        Me.World = world
        Me.Inputs = New Inputs
        Me.Camera = New Camera With {.Scene = Me}
        Me.Progress = New Progress(0, "")
        Me.State = SceneState.Wait
        Rect = New Rect(0, 0, size.Width, size.Height)
        CreateSceneView()
    End Sub

    Public Overrides Async Sub Start()
        Await Task.Run(Async Function()
                           Progress = New Progress(0, "创建实体")
                           Await CreateGameObjectsAsync()
                           Progress.Description = "初始化场景"
                           State = SceneState.Initialize
                           For Each SubLayer In GameLayers
                               SubLayer.Start()
                           Next
                           Camera.Start()
                           Progress.Description = "初始化组件"
                           GameComponents.Start()
                       End Function)
        State = SceneState.Loop
    End Sub
    Public Overrides Sub Update()
        If State = SceneState.Loop Then
            '执行旧的改动
            For Each SubAction In ModifiedActions
                SubAction.Invoke()
            Next
            ModifiedActions.Clear()
            '更新图层
            For Each SubLayer In GameLayers
                SubLayer.Update()
            Next
            Camera.Update()
            GameComponents.Update()
        End If
    End Sub
    Public Sub AddGameVisual(model As IGameBody, view As IGameView, Optional LayerIndex As Integer = 0) Implements IScene.AddGameVisual
        model.Scene = Me
        model.AttachGameView(view)
        Dim modifiedAction = Sub()
                                 While (GameLayers.Count <= LayerIndex)
                                     GameLayers.Add(GetDefaultLayer())
                                 End While
                                 GameLayers(LayerIndex).GameBodys.Add(model)
                                 If State = SceneState.Initialize OrElse State = SceneState.Loop Then
                                     model.Start()
                                 End If
                             End Sub
        If State = SceneState.Initialize OrElse State = SceneState.Loop Then
            ModifiedActions.Add(modifiedAction)
        Else
            modifiedAction.Invoke()
        End If
    End Sub
End Class
