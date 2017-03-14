''' <summary>
''' 普通攻击
''' </summary>
Public Class Attack
    Inherits AbilityBase
    Public Overrides Property Name As String = "Attack"
    Protected Overrides Sub Perform(character As ICharacter)
        Throw New NotImplementedException()
    End Sub
End Class
