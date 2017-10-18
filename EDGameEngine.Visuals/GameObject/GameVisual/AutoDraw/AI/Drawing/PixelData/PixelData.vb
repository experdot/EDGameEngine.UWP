Imports Windows.UI
''' <summary>
''' 像素数据
''' </summary>
Public Structure PixelData
    ''' <summary>
    ''' 颜色数组
    ''' </summary>
    Public Colors() As Color
    ''' <summary>
    ''' 宽度
    ''' </summary>
    Public Width As Integer
    ''' <summary>
    ''' 高度
    ''' </summary>
    Public Height As Integer
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Sub New(col As Color(), w As Integer, h As Integer)
        Colors = col
        Width = w
        Height = h
    End Sub
End Structure
