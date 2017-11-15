''' <summary>
''' 游戏组件基类
''' </summary>
Public MustInherit Class GameComponentBase
    Implements IGameComponent
    ''' <summary>
    ''' 目标对象
    ''' </summary>
    Public Property Target As IGameVisual Implements IGameComponent.Target
    ''' <summary>
    ''' 类型标记
    ''' </summary>
    Public MustOverride Property ComponentType As ComponentType Implements IGameComponent.ComponentType
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
