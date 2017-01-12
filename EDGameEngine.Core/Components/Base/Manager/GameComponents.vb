''' <summary>
''' 提供对游戏附加组件的统一管理
''' </summary>
Public Class GameComponents
    Public Property Target As IGameVisual
    Public Property Sounds As TypedComponent(Of IAudio)
    Public Property Effects As TypedComponent(Of IEffect)
    Public Property Behaviors As TypedComponent(Of IBehavior)
    ''' <summary>
    ''' 由指定的可视化对象创建并初始化一个游戏组件管理对象的实例
    ''' </summary>
    ''' <param name="target">指定的可视化对象</param>
    Public Sub New(target As IGameVisual)
        Me.Target = target
        Sounds = New TypedComponent(Of IAudio) With {.Target = Me.Target}
        Effects = New TypedComponent(Of IEffect) With {.Target = Me.Target}
        Behaviors = New TypedComponent(Of IBehavior) With {.Target = Me.Target}
    End Sub

    Public Sub Start()
        Sounds.Start()
        Effects.Start()
        Behaviors.Start()
    End Sub
    Public Sub Update()
        Sounds.Update()
        Effects.Update()
        Behaviors.Update()
    End Sub
End Class
