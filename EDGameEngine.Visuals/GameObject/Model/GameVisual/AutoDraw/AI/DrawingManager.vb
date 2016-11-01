Imports Windows.UI

Public Class DrawingManager
    Public Property Drawings As List(Of Drawing)

    Public Sub New()
        Drawings = New List(Of Drawing)
    End Sub

    Public Function NextPoint() As Point
        Static Index0, Index1, Index2 As Integer
        While (Index2 >= Drawings(Index0).Lines(Index1).Points.Count)
            Index2 = 0
            Index1 += 1
            If Index1 >= Drawings(Index0).Lines.Count Then
                Index1 = 0
                If Index0 < Drawings.Count - 1 Then Index0 += 1
            End If
        End While

        Dim tp As Point = Drawings(Index0).Lines(Index1).Points(Index2)
        Dim tc As Color = tp.Color
        tc.A = CByte(Drawings(Index0).PenAlpha)
        Index2 += 1
        Return New Point() With {.Color = tc, .Position = tp.Position, .Size = tp.Size * Drawings(Index0).PenSize}
    End Function
End Class
