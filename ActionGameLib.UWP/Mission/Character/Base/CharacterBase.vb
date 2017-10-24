Imports System.Numerics
Imports ActionGameLib.UWP
''' <summary>
''' 表示游戏角色的基类
''' </summary>
Public Class CharacterBase
    Implements ICharacter
    ''' <summary>
    ''' 角色死亡时发生的事件
    ''' </summary>
    Public Event Died(sender As Object, e As EventArgs)
    ''' <summary>
    ''' 角色名称
    ''' </summary>
    Public Property Name As String Implements ICharacter.Name
    ''' <summary>
    ''' 人物贴图
    ''' </summary>
    Public Property Image As ResourceId Implements ICharacter.Image
    ''' <summary>
    ''' 角色方位
    ''' </summary>
    Public Property Location As Vector2 Implements ICharacter.Location
    ''' <summary>
    ''' 角色角度
    ''' </summary>
    Public Property Rotation As Single Implements ICharacter.Rotation
    ''' <summary>
    ''' 生命值
    ''' </summary>
    Public Property HP As RemainingCounter Implements ICharacter.HP
    ''' <summary>
    ''' 能力管理器
    ''' </summary>
    Public Property AbilityManager As AbilityManager Implements ICharacter.AbilityManager
    ''' <summary>
    ''' 物理碰撞器
    ''' </summary>
    Public Property Collide As Collide Implements ICharacter.Collide

    Public Sub New()
        Collide = New Collide
        '初始化能力管理器实例
        AbilityManager = New AbilityManager
        '初始化HP
        HP = New RemainingCounter
        '绑定事件
        AddHandler HP.Expired, AddressOf HP_Expired
    End Sub

    Private Sub HP_Expired(sender As Object, e As ExpiredEventArgs)
        RaiseEvent Died(Me, Nothing)
    End Sub

    Public Sub Start() Implements IUpdateable.Start
        For Each SubAbility In AbilityManager.Abilities.Values
            SubAbility.Start(Me)
        Next
        Collide.SyncTransform(Me)
    End Sub

    Public Sub Update() Implements IUpdateable.Update
        For Each SubAbility In AbilityManager.Abilities.Values
            SubAbility.Update(Me)
        Next
        Collide.SyncTransform(Me)
    End Sub
End Class
