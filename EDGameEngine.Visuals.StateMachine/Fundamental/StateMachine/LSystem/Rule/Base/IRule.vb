Imports EDGameEngine.Visuals
''' <summary>
''' 规则接口
''' </summary>
Public Interface IRule
    ''' <summary>
    ''' 优先级
    ''' </summary>
    Property Priority As Integer
    ''' <summary>
    ''' 目标
    ''' </summary>
    Property Target As Integer
    ''' <summary>
    ''' 生成
    ''' </summary>
    Function Generate(parent As State) As List(Of State)
End Interface
