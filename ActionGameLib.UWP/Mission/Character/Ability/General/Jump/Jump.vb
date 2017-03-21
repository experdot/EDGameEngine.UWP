Imports System.Numerics
Imports ActionGameLib.UWP
''' <summary>
''' 跳跃
''' </summary>
Public Class Jump
    Inherits AbilityBase
    Public Overrides Property Name As String = "Jump"

    Public JumpState As JumpStateMachine(Of Character)
    Protected Overrides Sub Perform(target As ICharacter)
        If Math.Abs(target.Collide.Body.LinearVelocity.Y) < 0.001F Then
            JumpState.JumpCombo = 0
        End If
        If JumpState.JumpCombo < JumpStateMachine(Of Character).MaxJumpCombo Then
            target.Collide.Body.LinearVelocity = New Vector2(target.Collide.Body.LinearVelocity.X, -5.0F)
            JumpState.JumpCombo += 1
        Else

        End If
        'AddHandler character.Collide.Body.
    End Sub

    Public Overrides Sub Update(target As ICharacter)
        MyBase.Update(target)
        If Math.Abs(target.Collide.Body.LinearVelocity.Y) < 0.001F Then
            JumpState.JumpCombo = 0
        End If
    End Sub
End Class
