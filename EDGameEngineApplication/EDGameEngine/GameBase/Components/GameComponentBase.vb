Imports EDGameEngine

Public MustInherit Class GameComponentBase
    Implements IGameComponent
    Public Property Target As IGameVisualModel Implements IGameComponent.Target
    Public MustOverride Property CompnentType As ComponentType Implements IGameComponent.CompnentType
    Public Shared Property Rnd As New Random
    Public MustOverride Sub Start() Implements IGameComponent.Start
    Public MustOverride Sub Update() Implements IGameComponent.Update
End Class
