Imports EDGameEngine.Core
''' <summary>
''' 可视化游戏对象的基类
''' </summary>
Public MustInherit Class GameVisualBase
    Implements IGameVisual

    Public Overridable Property Transform As Transform = Transform.Normal Implements IGameVisual.Transform
    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements IGameVisual.Appearance
    Public Overridable Property GameComponents As GameComponents = New GameComponents(Me) Implements IGameVisual.GameComponents
    Public Overridable Property Presenter As IGameView Implements IGameVisual.Presenter
    Public Overridable Property Scene As IScene Implements IGameVisual.Scene
    Public Overridable Property Rect As Rect Implements IGameVisual.Rect

    Public Sub AttachGameView(view As IGameView) Implements IGameVisual.AttachGameView
        '解除原来的视图
        If Me.Presenter IsNot Nothing Then
            Me.Presenter.GameVisual = Nothing
        End If
        '附加新的视图
        Me.Presenter = view
        If view IsNot Nothing Then
            view.GameVisual = Me
        End If
    End Sub

    Public MustOverride Sub Start() Implements IGameObject.Start
    Public MustOverride Sub Update() Implements IGameObject.Update

End Class
