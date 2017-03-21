Imports System.Numerics
Imports ActionGameLib.UWP
Imports EDGameEngine.Core
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
    Public Property Scale As Single = 32
    ''' <summary>
    ''' 是否启用摄像机跟随
    ''' </summary>
    Public Property IsCameraFollow As Boolean = True
    ''' <summary>
    ''' 键盘
    ''' </summary>
    WithEvents Keyboard As KeyboardDescription

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
                    'blocks(i, j).Collide.Rect = New Rect(0, 0, 0.5 + Rnd.NextDouble * 0.5, 0.5 + Rnd.NextDouble * 0.5)
                End If
            Next
        Next
        Mission.Blocks = blocks
        '添加角色
        Mission.Characters.Add(New Player With {.Location = New Vector2(2, -2),
                                                .Image = ResourceId.Create(CharacterImageID.Default)})
        Mission.Characters.Last.Collide.Rect = New Rect(0, 0, 0.5, 0.5)
        '添加怪物
        For i = 0 To 5
            Mission.Characters.Add(New Monster With {.Location = New Vector2(5 + i, -2),
                                                .Image = ResourceId.Create(CharacterImageID.Default)})
            Mission.Characters.Last.Collide.Rect = New Rect(0, 0, 0.5 + Rnd.NextDouble * 0.5, 0.5 + Rnd.NextDouble * 0.5)
        Next

        '初始化世界
        ActionGame = New GameWorld() With {.Mission = Mission}
        '开始游戏
        ActionGame.Start()
        Keyboard = Scene.Inputs.Keyboard
    End Sub

    Public Overrides Sub UpdateEx()
        '更新
        ActionGame.Update()
        '摄像机跟随
        If IsCameraFollow Then
            CameraFollowTarget(ActionGame.Mission.Characters.First.Location)
        End If

        '按键测试
        KeyDown()
    End Sub

    Private Sub KeyDown()
        Static KeyArr() As VirtualKey = {VirtualKey.A, VirtualKey.D, VirtualKey.W, VirtualKey.S}
        For i = 0 To 1
            If Scene.Inputs.Keyboard.KeyStatus(KeyArr(i)) Then
                ActionGame.KeyDown(ChrW(KeyArr(i)))
            End If
        Next
    End Sub

    Private Sub KeyBoard_KeyDown(keyCode As VirtualKey) Handles Keyboard.KeyDown
        If keyCode = VirtualKey.W Then
            ActionGame.KeyDown(ChrW(keyCode))
        End If
    End Sub

    Private Sub KeyBoard_KeyUp(keyCode As VirtualKey) Handles Keyboard.KeyUp

    End Sub

    Private Sub CameraFollowTarget(loc As Vector2)
        Scene.Camera.Transform.Translation = New Vector2(Scene.Width, Scene.Height) / 2 - loc * Scale
    End Sub
End Class
