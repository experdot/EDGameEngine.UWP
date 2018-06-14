Public Class RuleTree
    Inherits RuleBase
    Public Sub New(target As Integer, priority As Integer)
        Me.Target = target
        Me.Priority = priority
    End Sub
    Public Overrides Function Generate(parent As State) As List(Of State)
        Dim results As New List(Of State)
        Dim states As String = "FF+[+F-FLL-F]-[-FL+F+FR]"
        'Dim states As String = "F+F--F+F"
        'Dim states As String = "F+FL-FRF+F+FL-F"
        For Each state In states
            results.Add(New State(AscW(state), parent, parent.Generation + 1))
        Next
        Return results
    End Function
End Class

