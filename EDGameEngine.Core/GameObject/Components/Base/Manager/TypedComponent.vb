''' <summary>
''' 提供对某种游戏组件的统一管理
''' </summary>
Public Class TypedComponent(Of T As IGameComponent)
    Implements IGameComponent
    Public Property ComponentType As ComponentType Implements IGameComponent.ComponentType
    Public Property Target As IGameVisual Implements IGameComponent.Target
    ''' <summary>
    ''' 组件集合
    ''' </summary>
    Public Property Items As New List(Of T)
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        Me.Items = New List(Of T)
    End Sub
    Public Sub Start() Implements IGameComponent.Start
        For Each item In Items
            item.Start()
        Next
    End Sub
    Public Sub Update() Implements IGameComponent.Update
        For Each item In Items
            item.Update()
        Next
    End Sub
    Public Sub Add(item As T)
        item.Target = Target
        If Target.Scene?.State = SceneState.Loop Then
            item.Start()
        End If
        Items.Add(item)
    End Sub
End Class
