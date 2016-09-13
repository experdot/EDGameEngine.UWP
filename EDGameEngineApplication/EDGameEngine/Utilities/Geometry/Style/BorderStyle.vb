Imports Windows.UI
''' <summary>
''' 描述边框样式的数据结构
''' </summary>
Public Structure BorderStyle
    Property State As Boolean
    Property Width As Single
    Property Color As Color
    Sub New(state As Boolean, Optional width As Single = 1.0F, Optional color As Color = Nothing)
        Me.State = state
        Me.Width = width
        Me.Color = If(color = Nothing, Colors.Black, color)
    End Sub
End Structure
