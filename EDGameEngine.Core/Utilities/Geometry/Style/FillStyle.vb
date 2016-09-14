Imports Windows.UI
''' <summary>
''' 描述填充样式的数据类型
''' </summary>
Public Structure FillStyle
    Property State As Boolean
    Property Color As Color
    Sub New(state As Boolean, Optional color As Color = Nothing)
        Me.State = state
        Me.Color = If(color = Nothing, Colors.Black, color)
    End Sub
End Structure
