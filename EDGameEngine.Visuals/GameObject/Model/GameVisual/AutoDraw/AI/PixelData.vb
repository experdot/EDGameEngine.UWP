Imports Windows.UI
''' <summary>
''' 像素数据
''' </summary>
Public Structure PixelData
    Public Colors() As Color
    Public Width As Integer
    Public Height As Integer
    Sub New(col As Color(), w As Integer, h As Integer)
        Colors = col
        Width = w
        Height = h
    End Sub
End Structure
