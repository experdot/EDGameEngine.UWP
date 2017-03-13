''' <summary>
''' 为计数器失效时发生的事件提供数据
''' </summary>
Public Class ExpiredEventArgs
    ''' <summary>
    ''' 失效类型
    ''' </summary>
    Public Flag As CounterTypes
    ''' <summary>
    ''' 由指定的计数器类型创建并初始化一个实例
    ''' </summary>
    Public Sub New(flag As CounterTypes)
        Me.Flag = flag
    End Sub
End Class
