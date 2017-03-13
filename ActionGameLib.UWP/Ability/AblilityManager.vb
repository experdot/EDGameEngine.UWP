''' <summary>
''' 能力管理器
''' </summary>
Public Class AblilityManager
    ''' <summary>
    ''' 能力集合
    ''' </summary>
    Public Property Abilities As Dictionary(Of String, IAbility)
    ''' <summary>
    ''' 添加能力
    ''' </summary>
    Public Sub Add(ability As IAbility)
        Abilities.Add(ability.Name, ability)
    End Sub
    ''' <summary>
    ''' 移除指定名称的能力
    ''' </summary>
    Public Sub Remove(name As String)
        Abilities.Remove(name)
    End Sub
    ''' <summary>
    ''' 返回指定名称的能力
    ''' </summary>
    Public Function FindAbilityByName(name As String) As IAbility
        Return Abilities.Item(name)
    End Function
End Class
