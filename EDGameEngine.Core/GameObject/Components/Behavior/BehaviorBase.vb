''' <summary>
''' 游戏行为基类
''' </summary>
Public MustInherit Class BehaviorBase
    Inherits GameComponentBase
    Implements IBehavior
    Public Overrides Property ComponentType As ComponentType = ComponentType.Behavior
End Class
