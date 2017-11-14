''' <summary>
''' <see cref="IRule"/>优先级比较器
''' </summary>
Public Class RulePriorityCompare
    Implements IComparer(Of IRule)
    Public Function Compare(x As IRule, y As IRule) As Integer Implements IComparer(Of IRule).Compare
        If x.Priority >= y.Priority Then
            Return 1
        Else
            Return -1
        End If
    End Function
End Class
