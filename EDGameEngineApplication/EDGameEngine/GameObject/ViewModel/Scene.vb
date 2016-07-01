Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 容纳多个可视化对象的空间
''' </summary>
Public Class Scene
    Inherits SceneBase
    Public Sub New(WindowSize As Size)
        Width = WindowSize.Width
        Height = WindowSize.Height
    End Sub
End Class
