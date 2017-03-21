''' <summary>
''' AI控制器
''' </summary>
Public Class AIContorller

    Public Shared Rnd As New Random
    ''' <summary>
    ''' 控制角色
    ''' </summary>
    Public Shared Sub Control(mission As Mission)
        For Each SubCharacter In mission.Characters
            If SubCharacter.GetType Is GetType(Monster) Then
                MonsterMove(SubCharacter)
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
