Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 线条画管理器
''' </summary>
Public Class DrawingManager
    ''' <summary>
    ''' 线条画集合
    ''' </summary>
    Public Property Drawings As List(Of Drawing)

    Public Sub New()
        Drawings = New List(Of Drawing)
    End Sub

    Public Function NextPoint() As Point
        Static Flag As Integer = 0
        Static Index0, Index1, Index2 As Integer
        Static IsOver As Boolean
        If IsOver Then
            Return New Point() With {.Color = Colors.Transparent, .Position = Vector2.Zero, .Size = 1}
        End If
        While (Index2 >= Drawings(Index0).Lines(Index1).Points.Count)
            Flag = 0
            Index2 = 0
            Index1 += 1
            While (Index1 >= Drawings(Index0).Lines.Count)
                Index1 = 0
                Index0 += 1
                If Index0 >= Drawings.Count Then
                    IsOver = True
                    Return New Point() With {.Color = Colors.Transparent, .Position = Vector2.Zero, .Size = 0}
                End If
            End While
        End While
        Dim tp As Point = Drawings(Index0).Lines(Index1).Points(Index2)
        Dim tc As Color = tp.Color
        tc.A = CByte(Drawings(Index0).PenAlpha)
        Dim size As Single = tp.Size * Drawings(Index0).PenSize
        Index2 += 1
        Return New Point() With {.Color = tc, .Position = tp.Position, .Size = size}
    End Function
End Class
