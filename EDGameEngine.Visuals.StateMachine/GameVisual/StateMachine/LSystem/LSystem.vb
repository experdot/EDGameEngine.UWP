''' <summary>
''' L系统
''' </summary>
Public Class LSystem
    ''' <summary>
    ''' 状态集
    ''' </summary>
    Public Property States As List(Of State)
    ''' <summary>
    ''' 规则集
    ''' </summary>
    Public Property RuleSets As Dictionary(Of Integer, RuleSet)
    ''' <summary>
    ''' 初代
    ''' </summary>
    Public Property Root As State

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        RuleSets = New Dictionary(Of Integer, RuleSet)
        States = New List(Of State)
    End Sub
    ''' <summary>
    ''' 初始化初代
    ''' </summary>
    Public Sub InitRoot(root As State)
        Me.Root = root
        States.Clear()
        States.Add(root)
    End Sub
    ''' <summary>
    ''' 添加规则
    ''' </summary>
    Public Sub AddRule(rule As IRule)
        If Not RuleSets.ContainsKey(rule.Target) Then
            RuleSets.Add(rule.Target, New RuleSet)
        End If
        RuleSets.Item(rule.Target).Add(rule)
    End Sub
    ''' <summary>
    ''' 生成
    ''' </summary>
    Public Sub Generate(Optional count As Integer = 1)
        For i = 0 To count - 1
            Dim tempStates As New List(Of State)
            For Each SubState In States
                If RuleSets.ContainsKey(SubState.Id) Then
                    SubState.Children = RuleSets.Item(SubState.Id).First.Generate(SubState)
                    tempStates.AddRange(SubState.Children)
                Else
                    SubState.Children = GetDefault(SubState)
                    tempStates.AddRange(SubState.Children)
                End If
            Next
            States.Clear()
            States.AddRange(tempStates)
        Next
    End Sub

    Private Function GetDefault(state As State) As List(Of State)
        Dim result As New List(Of State)
        result.Add(New State(state.Id, state, state.Generation + 1))
        Return result
    End Function
End Class
