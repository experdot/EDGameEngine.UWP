''' <summary>
''' 能力接口
''' </summary>
Public Interface IAbility
    ''' <summary>
    ''' 名称
    ''' </summary>
    Property Name As String
    ''' <summary>
    ''' 计数器
    ''' </summary>
    Property Counter As RemainingCounter
    ''' <summary>
    ''' 释放技能
    ''' </summary>
    Sub Release(character As ICharacter)
End Interface
