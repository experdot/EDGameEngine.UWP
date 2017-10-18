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
    Public Event KeyDown(ByVal keyCode As VirtualKey)
    ''' <summary>
    ''' 按键释放时发生
    ''' </summary>
    Public Event KeyUp(ByVal keyCode As VirtualKey)
    ''' <summary>
    ''' 触发按键按下
    ''' </summary>
    Public Sub RaiseKeyDown(ByVal keyCode As VirtualKey)
        KeyStatus(keyCode) = True
        RaiseEvent KeyDown(keyCode)
    End Sub
    ''' <summary>
    ''' 触发按键释放
    ''' </summary>
    Public Sub RaiseKeyUp(ByVal keyCode As VirtualKey)
        KeyStatus(keyCode) = False
        RaiseEvent KeyUp(keyCode)
    End Sub
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        ReDim KeyStatus(255)
    End Sub
End Class
