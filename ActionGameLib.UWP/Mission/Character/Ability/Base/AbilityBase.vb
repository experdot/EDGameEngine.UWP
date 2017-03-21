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
    Protected MustOverride Sub Perform(target As ICharacter)

    Public Sub New()
        Counter = New RemainingCounter
    End Sub
    ''' <summary>
    ''' 初始化状态
    ''' </summary>
    Public Overridable Sub Start(target As ICharacter) Implements IAbility.Start
        'null
    End Sub
    ''' <summary>
    ''' 更新状态
    ''' </summary>
    Public Overridable Sub Update(target As ICharacter) Implements IAbility.Update
        'null
    End Sub
    ''' <summary>
    ''' 释放能力
    ''' </summary>
    Public Overridable Sub Release(target As ICharacter) Implements IAbility.Release
        If Counter.Availiable Then
            PerformBefore(target)
            Perform(target)
            PerformOver(target)
        End If
    End Sub
    ''' <summary>
    ''' 执行之前
    ''' </summary>
    Protected Overridable Sub PerformBefore(target As ICharacter)
        'null
    End Sub
    ''' <summary>
    ''' 执行结束
    ''' </summary>
    Protected Overridable Sub PerformOver(target As ICharacter)
        '计数器数量减一
        Counter.Change(CounterTypes.Count, -1)
    End Sub
End Class
