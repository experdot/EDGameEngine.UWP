Imports Windows.UI
''' <summary>
''' 碎片侧边
''' </summary>
Public Class FragmentSide
    ''' <summary>
    ''' 颜色
    ''' </summary>
    Public Property Colors As Color()
    ''' <summary>
    ''' 阈值
    ''' </summary>
    Public Property Threshold As Integer()
    ''' <summary>
    ''' 边沿长度
    ''' </summary>
    Public Property Length As Integer
    ''' <summary>
    ''' 是否空白
    ''' </summary>
    Public Property IsBlank As Boolean
    ''' <summary>
    ''' 由指定的长度数值创建并初始化一个碎片侧边对象
    ''' </summary>
    Public Sub New(len As Integer)
        ReDim Colors(len - 1)
        ReDim Threshold(len - 1)
        Length = len
        IsBlank = True
    End Sub
End Class
