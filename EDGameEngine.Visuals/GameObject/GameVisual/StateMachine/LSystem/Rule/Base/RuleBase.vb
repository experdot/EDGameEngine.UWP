''' <summary>
''' 规则基类
''' </summary>
Public MustInherit Class RuleBase
    Implements IRule

    Public Property Target As Integer Implements IRule.Target
    Public Property Priority As Integer Implements IRule.Priority


    Public MustOverride Function Generate(parent As State) As List(Of State) Implements IRule.Generate
End Class
