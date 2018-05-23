''' <summary>
''' 描述场景当前状态
''' </summary>
Public Enum SceneState
    ''' <summary>
    ''' 等待初始化
    ''' </summary>
    Wait
    ''' <summary>
    ''' 正在初始化
    ''' </summary>
    Initialize
    ''' <summary>
    ''' 暂停中
    ''' </summary>
    Pause
    ''' <summary>
    ''' 运行中
    ''' </summary>
    [Loop]
End Enum
