Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 容纳多个可视化对象的空间
''' </summary>
Public Class WorldSpace
    Inherits WorldBase
    Public Shared SpaceWidth As Single = 100
    Public Shared SpaceHeight As Single = 100
    Public Sub New(WindowSize As Size)
        SpaceWidth = WindowSize.Width
        SpaceHeight = WindowSize.Height
    End Sub
End Class
