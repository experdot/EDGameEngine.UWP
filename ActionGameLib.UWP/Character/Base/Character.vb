Imports ActionGameLib.UWP
''' <summary>
''' 表示游戏角色的基类
''' </summary>
Public Class Character
    Implements ICharacter
    ''' <summary>
    ''' 角色死亡时发生的事件
    ''' </summary>
    Public Event Died(sender As Object, e As EventArgs)
    ''' <summary>
    ''' 人物贴图
    ''' </summary>
    Public Property Image As ResourceId Implements ICharacter.Image
    ''' <summary>
    ''' 角色方位
    ''' </summary>
    Public Property Location As Location Implements ICharacter.Location
    ''' <summary>
    ''' 角色名称
    ''' </summary>
    Public Property Name As String
    ''' <summary>
    ''' 生命值
    ''' </summary>
    Public Property HP As RemainingCounter Implements ICharacter.HP
    ''' <summary>
    ''' 能力管理器
    ''' </summary>
    Public Property AbilityManager As AblilityManager Implements ICharacter.AbilityManager

    Public Sub New()
        AbilityManager = New AblilityManager
        '添加基础能力
        AbilityManager.Add(New Attack)
        AbilityManager.Add(New Jump)
        '初始化HP
        HP = New RemainingCounter
        AddHandler HP.Expired, AddressOf HP_Expired
    End Sub

    Public Sub Attack() Implements ICharacter.Attack
        AbilityManager.FindAbilityByName("Attack").Release(Me)
    End Sub

    Public Sub Jump() Implements ICharacter.Jump
        AbilityManager.FindAbilityByName("Jump").Release(Me)
    End Sub

    Private Sub HP_Expired(sender As Object, e As ExpiredEventArgs)
        RaiseEvent Died(Me, Nothing)
    End Sub
End Class
