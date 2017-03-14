''' <summary>
''' 表示角色获得物品或学习技能后具有的某种能力
''' </summary>
Public MustInherit Class AbilityBase
    Implements IAbility
    ''' <summary>
    ''' 能力名称
    ''' </summary>
    Public Overridable Property Name As String Implements IAbility.Name
    ''' <summary>
    ''' 计数器
    ''' </summary>
    Public Property Counter As RemainingCounter Implements IAbility.Counter

    ''' <summary>
    ''' 执行能力
    ''' </summary>
    Protected MustOverride Sub Perform(character As ICharacter)

    Public Sub New()
        Counter = New RemainingCounter
    End Sub
    ''' <summary>
    ''' 释放能力
    ''' </summary>
    Public Overridable Sub Release(character As ICharacter) Implements IAbility.Release
        If Counter.Availiable Then
            PerformBefore(character)
            Perform(character)
            PerformOver(character)
        End If
    End Sub
    ''' <summary>
    ''' 执行之前
    ''' </summary>
    Protected Overridable Sub PerformBefore(Character As ICharacter)
        '默认不做任何事情
    End Sub
    ''' <summary>
    ''' 执行结束
    ''' </summary>
    Protected Overridable Sub PerformOver(Character As ICharacter)
        '计数器数量减一
        Counter.Change(CounterTypes.Count, -1)
    End Sub

End Class
