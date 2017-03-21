Imports System.Numerics
Imports ActionGameLib.UWP
''' <summary>
''' 横向右移
''' </summary>
Public Class MoveRight
    Inherits AbilityBase
    Public Overrides Property Name As String = "MoveRight"
    Protected Overrides Sub Perform(target As ICharacter)
        target.Collide.Body.LinearVelocity += New Vector2(1.0F, 0)
    End Sub
End Class
