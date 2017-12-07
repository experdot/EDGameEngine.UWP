''' <summary>
''' 描述跳跃过程对于垂直速度影响的状态。
''' 小跳跃：站立 -> 按住跳跃按钮小于等于 <see cref="MinFlyTime"/> -> 松开。
''' 中跳跃：站立 -> 按住跳跃按钮大于 <see cref="MinFlyTime"/> 小于 <see cref="MaxFlyTimeCountDown"/> -> 松开。
''' 大跳跃：站立 -> 按住跳跃按钮大于等于 <see cref="MinFlyTime"/> -> 松开。
''' 多段跳：<see cref="MaxJumpCombo"/> 大于 1，站立 -> 按住跳跃按钮 -> 落地前松开 -> 落地前再次按住跳跃按钮 -> <see cref="JumpCombo"/> 达到 <see cref="MaxJumpCombo"/> 时落地前暂停相应跳跃按钮 -> 松开。
''' </summary>
''' <typeparam name="TCharacter">表示这个跳跃状态是给哪位角色准备的。可以为特定的角色设置不同的 Shared 字段 (在 Visual C# 为 static) 让不同的人物有不同的跳跃能力。</typeparam>
Public Structure JumpStateMachine(Of TCharacter)
    ''' <summary>
    ''' 表示现在跳跃按钮是否按下。与键盘或触摸输入同步。
    ''' </summary>
    Dim IsJumpButtonPressing As Boolean
    ''' <summary>
    ''' 表示上次跳跃按钮是否按下。与键盘或触摸输入同步。
    ''' </summary>
    Dim WasJumpButtonPressed As Boolean
    ''' <summary>
    ''' 当前在进行第几次连续跳跃。未起跳时为 0 。
    ''' </summary>
    Dim JumpCombo As Integer
    ''' <summary>
    ''' 当前的纵向速度。与物理引擎同步。
    ''' </summary>
    Dim VelocityY As Single
    ''' <summary>
    ''' 上次的纵向速度。大多用于判断静止。
    ''' </summary>
    Dim LastVelocityY As Single
    ''' <summary>
    ''' 控制最小向上匀速飞行的时间。未开始计时的情况下是 -1，倒计时从 <see cref="MinFlyTime"/> 开始，到达规定时间时为 0。
    ''' </summary>
    Dim MinFlyTimeCountDown As Integer
    ''' <summary>
    ''' 控制最大向上匀速飞行的时间。未开始计时的情况下是 -1，倒计时从 <see cref="MaxFlyTime"/> 开始，到达规定时间时为 0。
    ''' </summary>
    Dim MaxFlyTimeCountDown As Integer

#Region "跳跃能力"
    ''' <summary>
    ''' 表示当前 <typeparamref name="TCharacter"/> 最多能够连跳多少次。
    ''' </summary>
    Shared MaxJumpCombo As Integer = 3
    ''' <summary>
    ''' 表示当前 <typeparamref name="TCharacter"/> 跳跃时匀速向上移动的速度。由于游戏的 y 轴向下，这个值小于 0 才有意义。
    ''' </summary>
    Shared JumpStartVelocityY As Single = -5.0F
    ''' <summary>
    ''' 表示当前 <typeparamref name="TCharacter"/> 最小匀速向上腾空的时间。
    ''' </summary>
    Shared MinFlyTime As Integer = 30
    ''' <summary>
    ''' 表示当前 <typeparamref name="TCharacter"/> 最大匀速向上腾空的时间。
    ''' </summary>
    Shared MaxFlyTime As Integer = 120
#End Region
    ''' <summary>
    ''' 初始化跳跃状态。
    ''' </summary>
    Public Sub Initialize()
        MaxFlyTimeCountDown = -1
        MinFlyTimeCountDown = -1
    End Sub
End Structure
