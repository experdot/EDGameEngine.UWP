Imports EDGameEngine.Visuals
''' <summary>
''' 包含同一种目标状态的规则集对象
''' </summary>
Public Class RuleSet
    ''' <summary>
    ''' 规则集
    ''' </summary>
    Public Property Rules As List(Of IRule)
    ''' <summary>
    ''' 第一优先的规则
    ''' </summary>
    Public ReadOnly Property First As IRule
        Get
            Return Rules.First
        End Get
    End Property
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        Rules = New List(Of IRule)
    End Sub
    ''' <summary>
    ''' 添加规则
    ''' </summary>
    Public Sub Add(rule As IRule)
        Rules.Add(rule)
        Rules.Sort(New RulePriorityCompare)
    End Sub

End Class
