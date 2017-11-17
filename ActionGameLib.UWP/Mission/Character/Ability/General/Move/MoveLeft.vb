Imports System.Numerics
Imports ActionGameLib.UWP
''' <summary>
''' 横向左移
''' </summary>
Public Class MoveLeft
    Inherits AbilityBase
    Public Overrides Property Name As String = "MoveLeft"
    Protected Overrides Sub Perform(target As ICharacter)
        'target.Collide.Body.LinearVelocity += New Vector2(-1.0F, 0)
        target.Collide.Body.Position += New Vector2(-0.001F, 0)
        target.Collide.Body.ApplyLinearImpulse(New Vector2(-1.0F, 0))
    End Sub
End Class
