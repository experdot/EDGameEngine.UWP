''' <summary>
''' 媒体效果器，动画器与音效器继承自它
''' </summary>
Public Interface IMedia
    Inherits IGameComponent
    ''' <summary>
    ''' 播放
    ''' </summary>
    Sub Play()
    ''' <summary>
    ''' 停止
    ''' </summary>
    Sub [Stop]()
    ''' <summary>
    ''' 暂停
    ''' </summary>
    Sub Pause()
End Interface
