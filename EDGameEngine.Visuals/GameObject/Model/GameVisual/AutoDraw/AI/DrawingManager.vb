Imports Windows.UI

Public Class DrawingManager
    Public Property Drawings As List(Of Drawing)

    Public Sub New()
        Drawings = New List(Of Drawing)
    End Sub

    Public Function NextPoint() As Point
        Static Flag As Integer = 0
        Static Index0, Index1, Index2 As Integer
        While (Index2 >= Drawings(Index0).Lines(Index1).Points.Count)
            Flag = 0
            Index2 = 0
            Index1 += 1
            While (Index1 >= Drawings(Index0).Lines.Count)
                Index1 = 0
                If Index0 < Drawings.Count - 1 Then Index0 += 1
            End While
        End While
        Dim tp As Point = Drawings(Index0).Lines(Index1).Points(Index2)
        Dim tc As Color = tp.Color
        tc.A = CByte(Drawings(Index0).PenAlpha)
        Index2 += 1
        Return New Point() With {.Color = tc, .Position = tp.Position, .Size = tp.Size * Drawings(Index0).PenSize}
    End Function
End Class
