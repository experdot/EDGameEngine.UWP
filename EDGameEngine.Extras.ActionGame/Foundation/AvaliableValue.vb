''' <summary>
''' 可设置数据有效性的数据类型
''' </summary>
Public Structure AvaliableValue(Of T)
    ''' <summary>
    ''' 值
    ''' </summary>
    Public Property Value As T
    ''' <summary>
    ''' 有效性
    ''' </summary>
    Public Property Enabled As Boolean
End Structure
