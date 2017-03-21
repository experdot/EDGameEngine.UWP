''' <summary>
''' 能力接口
''' </summary>
Public Interface IAbility
    Inherits ITypedUpdateable(Of ICharacter)
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
    Sub Release(target As ICharacter)

End Interface
