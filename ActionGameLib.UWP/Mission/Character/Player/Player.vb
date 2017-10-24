''' <summary>
''' 玩家角色
''' </summary>
Public Class Player
    Inherits CharacterBase
    Public Sub New()
        '添加基础移动
        AbilityManager.Add(New Jump)
        AbilityManager.Add(New MoveLeft)
        AbilityManager.Add(New MoveRight)
        '添加基础攻击
        AbilityManager.Add(New Attack)
    End Sub
End Class
