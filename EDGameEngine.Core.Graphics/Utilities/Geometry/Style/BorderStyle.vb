Imports Windows.UI
''' <summary>
''' 描述边框样式的对象
''' </summary>
Public Class BorderStyle
    ''' <summary>
    ''' 样式是否有效
    ''' </summary>
    Public Property State As Boolean = True
    ''' <summary>
    ''' 边框宽度
    ''' </summary>
    Public Property Width As Single = 1.0F
    ''' <summary>
    ''' 边框颜色
    ''' </summary>
    Public Property Color As Color = Colors.Black
    ''' <summary>
    ''' 由指定的参数创建并初始化一个实例
    ''' </summary>
    Public Sub New(state As Boolean, Optional width As Single = 1.0F, Optional color As Color = Nothing)
        Me.State = state
        Me.Width = width
        Me.Color = If(color = Nothing, Colors.Black, color)
    End Sub
End Class
