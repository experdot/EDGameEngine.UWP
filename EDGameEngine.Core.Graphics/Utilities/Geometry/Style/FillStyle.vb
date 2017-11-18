Imports Windows.UI
''' <summary>
''' 描述填充样式的对象
''' </summary>
Public Class FillStyle
    ''' <summary>
    ''' 样式是否有效
    ''' </summary>
    Public Property State As Boolean = True
    ''' <summary>
    ''' 填充颜色
    ''' </summary>
    Public Property Color As Color = Colors.Black
    ''' <summary>
    ''' 由指定的参数创建并初始化一个实例
    ''' </summary>
    Public Sub New(state As Boolean, Optional color As Color = Nothing)
        Me.State = state
        Me.Color = If(color = Nothing, Colors.Black, color)
    End Sub
End Class
