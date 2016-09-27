Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 包含位置和颜色信息的顶点
''' </summary>
Public Structure ColorVertex
    Public Position As Vector2
    Public Color As Color

    Public Sub New(pos As Vector2, col As Color)
        Position = pos
        Color = col
    End Sub
End Structure
