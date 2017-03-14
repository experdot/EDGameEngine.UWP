Imports System.Numerics
''' <summary>
''' 跳跃
''' </summary>
Public Class Jump
    Inherits AbilityBase
    Public Overrides Property Name As String = "Jump"

    Protected Overrides Sub Perform(character As ICharacter)
        character.Collide.Body.LinearVelocity += New Vector2(0, -1.0F)
    End Sub
End Class
