Imports System.Numerics
Imports ActionGameLib.UWP
Imports EDGameEngine.Core
Imports FarseerPhysics.Dynamics
Imports Windows.System

Public Class ActionGameModel
    Inherits GameBody
    ''' <summary>
    ''' 游戏世界
    ''' </summary>
    ''' <returns></returns>
    Public Property ActionGame As GameWorld
    ''' <summary>
    ''' 活动的关卡
    ''' </summary>
    Public Property Mission As Mission
    ''' <summary>
    ''' 缩放
    ''' </summary>
    Public Property Scale As Single = 64

    Public Overrides Sub StartEx()
        Dim w As Integer = 20
        Dim h As Integer = 10

        Me.Rect = New Rect(0, 0, w * Scale, h * Scale)

        '初始化关卡
        Mission = New Mission
        '初始化地图块
        Dim blocks(w - 1, h - 1) As Block
        For i = 0 To w - 1
            For j = 0 To h - 1
                If Rnd.NextDouble > 0.618 Then
                    blocks(i, j) = New Block()
                    blocks(i, j).Location = New Vector2(i, j)
                    If Rnd.NextDouble > 0.618 Then
                        blocks(i, j).Image = ResourceId.Create(BlockImageID.Question)
                    Else
                        blocks(i, j).Image = ResourceId.Create(BlockImageID.Blank)
                    End If
                End If
            Next
        Next
        Mission.Blocks = blocks
        '初始化角色
        Mission.Characters.Add(New Player With {.Location = New Vector2(2, -2),
                                                .Image = ResourceId.Create(CharacterImageID.Default)})

        '初始化世界
        ActionGame = New GameWorld() With {.Mission = Mission}
        '开始游戏
        ActionGame.Start()
    End Sub

    Public Overrides Sub UpdateEx()
        '更新
        ActionGame.Update()

        '按键测试
        KeyDown()
    End Sub

    Private Sub KeyDown()
        Static KeyArr() As VirtualKey = {VirtualKey.A, VirtualKey.D, VirtualKey.W, VirtualKey.S}

        For i = 0 To 3
            If Scene.Inputs.Keyboard.KeyStatus(KeyArr(i)) Then
                ActionGame.KeyDown(ChrW(KeyArr(i)))
            End If
        Next
    End Sub

End Class
