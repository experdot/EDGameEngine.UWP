Imports EDGameEngine.Core
''' <summary>
''' 提供对游戏附加组件的统一管理
''' </summary>
Public Class GameComponents
    Implements IGameObject
    Public Property Target As IGameVisual
    Public Property Sounds As TypedComponent(Of IAudio)
    Public Property Effects As TypedComponent(Of IEffect)
    Public Property Behaviors As TypedComponent(Of IBehavior)
    ''' <summary>
    ''' 由指定的可视化对象创建并初始化一个游戏组件管理对象的实例
    ''' </summary>
    Public Sub New(target As IGameVisual)
        Me.Target = target
        Sounds = New TypedComponent(Of IAudio) With {.Target = Me.Target}
        Effects = New TypedComponent(Of IEffect) With {.Target = Me.Target}
        Behaviors = New TypedComponent(Of IBehavior) With {.Target = Me.Target}
    End Sub

    ''' <summary>
    ''' 开始
    ''' </summary>
    Public Sub Start() Implements IGameObject.Start
        Sounds.Start()
        Effects.Start()
        Behaviors.Start()
    End Sub
    ''' <summary>
    ''' 更新
    ''' </summary>
    Public Sub Update() Implements IGameObject.Update
        Sounds.Update()
        Effects.Update()
        Behaviors.Update()
    End Sub
End Class
