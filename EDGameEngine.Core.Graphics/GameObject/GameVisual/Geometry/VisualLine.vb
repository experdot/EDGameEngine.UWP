Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 基础几何图元，线段
''' </summary>
Public Class VisualLine
    Inherits GeometryBase
    ''' <summary>
    ''' 点集
    ''' </summary>
    Public Property Points As New List(Of Vector2)
    ''' <summary>
    ''' 线段宽度
    ''' </summary>
    Public Property Width As Single = 1.0F

    Public Overrides Sub StartEx()

    End Sub

    Public Overrides Sub UpdateEx()

    End Sub
End Class
