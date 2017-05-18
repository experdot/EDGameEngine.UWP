Imports Windows.UI
''' <summary>
''' 描述填充样式的数据类型
''' </summary>
Public Structure FillStyle
    ''' <summary>
    ''' 样式是否有效
    ''' </summary>
    Property State As Boolean
    ''' <summary>
    ''' 填充颜色
    ''' </summary>
    Property Color As Color
    ''' <summary>
    ''' 由指定的参数创建并初始化一个实例
    ''' </summary>
    Sub New(state As Boolean, Optional color As Color = Nothing)
        Me.State = state
        Me.Color = If(color = Nothing, Colors.Black, color)
    End Sub
End Structure
