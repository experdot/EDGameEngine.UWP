Imports EDGameEngine

Public MustInherit Class GameComponentBase
    Implements IGameComponent
    ''' <summary>
    ''' 目标对象
    ''' </summary>
    Public Property Target As IVisualObject Implements IGameComponent.Target
    Public MustOverride Property CompnentType As ComponentType Implements IGameComponent.CompnentType
    ''' <summary>
    ''' 当前组件目标对象所在的场景
    ''' </summary>
    Public Property Scene As IScene
        Get
            Return Target.Scene
        End Get
        Set(value As IScene)
            Target.Scene = value
        End Set
    End Property
    ''' <summary>
    ''' 当前组件目标对象所在的场景的摄像机
    ''' </summary>
    Public Property Camera As ICamera
        Get
            Return Target.Scene.Camera
        End Get
        Set(value As ICamera)
            Target.Scene.Camera = value
        End Set
    End Property
    Public Shared Property Rnd As New Random
    Public MustOverride Sub Start() Implements IGameComponent.Start
    Public MustOverride Sub Update() Implements IGameComponent.Update
End Class
