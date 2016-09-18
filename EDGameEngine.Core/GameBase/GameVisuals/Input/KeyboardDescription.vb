Imports Windows.System
''' <summary>
''' 用户键盘状态描述
''' </summary>
Public Class KeyboardDescription
    ''' <summary>
    ''' 当前激活的键
    ''' </summary>
    Public Property KeyStatus As Boolean()
    ''' <summary>
    ''' 按键按下时发生
    ''' </summary>
    Public Event KeyDown(ByVal KeyCode As VirtualKey)
    ''' <summary>
    ''' 按键释放时发生
    ''' </summary>
    Public Event KeyUp(ByVal KeyCode As VirtualKey)
    ''' <summary>
    ''' 触发按键按下
    ''' </summary>
    Friend Sub RaiseKeyDown(ByVal KeyCode As VirtualKey)
        KeyStatus(KeyCode) = True
        RaiseEvent KeyDown(KeyCode)
    End Sub
    ''' <summary>
    ''' 触发按键释放
    ''' </summary>
    Friend Sub RaiseKeyUp(ByVal KeyCode As VirtualKey)
        KeyStatus(KeyCode) = False
        RaiseEvent KeyUp(KeyCode)
    End Sub
    Public Sub New()
        ReDim KeyStatus(255)
    End Sub
End Class
