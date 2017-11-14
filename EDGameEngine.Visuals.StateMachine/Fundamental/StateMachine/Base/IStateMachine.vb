Imports EDGameEngine.Core
''' <summary>
''' 状态机模型接口
''' </summary>
Public Interface IStateMachine
    Inherits IGameBody
    ''' <summary>
    ''' 状态集
    ''' </summary>
    Property States As List(Of State)
End Interface
