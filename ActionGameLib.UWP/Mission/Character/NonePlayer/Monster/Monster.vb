Imports ActionGameLib.UWP
''' <summary>
''' 怪物
''' </summary>
Public Class Monster
    Inherits NPC
    Public Overrides Property Camp As Camp = Camp.Enemy

    Public Sub New()
        '添加基础移动
        AbilityManager.Add(New Jump)
        AbilityManager.Add(New MoveLeft)
        AbilityManager.Add(New MoveRight)
        '添加基础攻击
        AbilityManager.Add(New Attack)
    End Sub

End Class
