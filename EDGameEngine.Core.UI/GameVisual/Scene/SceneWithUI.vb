Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI.Core
''' <summary>
''' 游戏场景基类
''' </summary>
Public MustInherit Class SceneWithUI
    Inherits SceneBase
    Implements IObjectWithImageResource
    Public Property ImageResource As ImageResource Implements IObjectWithImageResource.ImageResource


    Protected MustOverride Sub CreateUI()
    Protected MustOverride Sub CreateObject()
    Protected MustOverride Function CreateResourcesAsync(imageResource As ImageResource) As Task

    Public Sub New(world As WorldWithUI, size As Size)
        MyBase.New(world, size)
    End Sub

    Protected Overrides Sub CreateSceneView()
        Me.AttachGameView(New SceneView)
    End Sub
    Protected Overrides Function GetDefaultLayer() As ILayer
        Dim layer As New Layer With {.Scene = Me}
        layer.AttachGameView(New LayerView())
        Return layer
    End Function
    Protected Overrides Async Function CreateGameObjectsAsync() As Task
        While CType(World, WorldWithUI).ResourceCreator Is Nothing
            Await Task.Delay(10)
        End While
        Await LoadAsync(CType(World, WorldWithUI).ResourceCreator)
        CreateObject()
        Dim container As UIElement = CType(World, WorldWithUI).UIContainer
        Dim priority As CoreDispatcherPriority = CoreDispatcherPriority.Normal
        Await container.Dispatcher.RunAsync(priority, Sub()
                                                          CreateUI()
                                                      End Sub)
    End Function
    Private Async Function LoadAsync(resourceCreator As ICanvasResourceCreator) As Task
        ImageResource = New ImageResource(resourceCreator)
        Await CreateResourcesAsync(ImageResource)
    End Function
End Class
