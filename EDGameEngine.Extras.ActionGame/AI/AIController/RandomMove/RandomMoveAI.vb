﻿''' <summary>
''' 随机移动AI控制器
''' </summary>
Public Class RandomMoveAI
    Implements IAIController
    ''' <summary>
    ''' 静态的<see cref="Random"/>实例
    ''' </summary>
    Protected Shared Rnd As New Random

    Public Sub Start(mission As IMission) Implements IAIController.Start

    End Sub
    Public Sub Update(mission As IMission) Implements IAIController.Update
        For Each character In mission.Characters
            If character.GetType Is GetType(Monster) Then
                MonsterMove(CType(character, Monster))
            End If
        Next
    End Sub

    Private Shared Sub MonsterMove(monster As Monster)
        If Rnd.NextDouble > 0.99 Then
            monster.AbilityManager.FindAbilityByName("Jump").Release(monster)
        End If
        If Math.Abs(monster.Collide.Body.LinearVelocity.X) < 0.5F Then
            If Rnd.NextDouble > 0.618 Then
                If Rnd.NextDouble > 0.5 Then
                    monster.AbilityManager.FindAbilityByName("MoveLeft").Release(monster)
                Else
                    monster.AbilityManager.FindAbilityByName("MoveRight").Release(monster)
                End If
            End If
        End If
    End Sub

End Class
