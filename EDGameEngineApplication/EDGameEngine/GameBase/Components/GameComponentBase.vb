Imports EDGameEngine

Public MustInherit Class GameComponentBase
    Implements IGameComponent
    Public MustOverride Sub Start() Implements IGameComponent.Start
    Public MustOverride Sub Update() Implements IGameComponent.Update
End Class
