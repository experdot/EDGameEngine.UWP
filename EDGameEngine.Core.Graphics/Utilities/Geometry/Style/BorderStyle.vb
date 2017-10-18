Imports Windows.UI
''' <summary>
''' 描述边框样式的数据结构
''' </summary>
Public Structure BorderStyle
    ''' <summary>
    ''' 样式是否有效
    ''' </summary>
    Property State As Boolean
    ''' <summary>
    ''' 边框宽度
    ''' </summary>
    Property Width As Single
    ''' <summary>
    ''' 边框颜色
    ''' </summary>
    Property Color As Color
    ''' <summary>
    ''' 由指定的参数创建并初始化一个实例
    ''' </summary>
    Sub New(state As Boolean, Optional width As Single = 1.0F, Optional color As Color = Nothing)
        Me.State = state
        Me.Width = width
        Me.Color = If(color = Nothing, Colors.Black, color)
    End Sub
End Structure
