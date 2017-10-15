Public Class RuleTree
    Inherits RuleBase
    Public Sub New(target As Integer, priority As Integer)
        Me.Target = target
        Me.Priority = priority
    End Sub
    Public Overrides Function Generate(parent As State) As List(Of State)
        Dim results As New List(Of State)
        Dim states As String = "FF+[+F-F-F]-[-F+F+F]"
        For Each SubChar In states
            results.Add(New State(AscW(SubChar), parent, parent.Generation + 1))
        Next
        Return results
    End Function
End Class

