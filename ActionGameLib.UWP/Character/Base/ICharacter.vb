Imports ActionGameLib.UWP
''' <summary>
''' 游戏角色接口
''' </summary>
Public Interface ICharacter
    ''' <summary>
    ''' 能力管理器
    ''' </summary>
    Property AbilityManager As AblilityManager
    ''' <summary>
    ''' 生命值
    ''' </summary>
    Property HP As RemainingCounter
    ''' <summary>
    ''' 人物贴图
    ''' </summary>
    Property Image As ResourceId
    ''' <summary>
    ''' 位置
    ''' </summary>
    ''' <returns></returns>
    Property Location As Location
    ''' <summary>
    ''' 普通攻击
    ''' </summary>
    Sub Attack()
    ''' <summary>
    ''' 普通跳跃
    ''' </summary>
    Sub Jump()
End Interface
