Imports System.Numerics
''' <summary>
''' 游戏可视的指针
''' </summary>
Public Class Pointer
    Inherits Core.GameVisual
    Public Location As New Vector2
    Public LocQueue As New Concurrent.ConcurrentQueue(Of Vector2)
    Public Overrides Sub StartEx()
        AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf MouseChanged
    End Sub

    Public Overrides Sub UpdateEx()

    End Sub
    Private Sub MouseChanged(loc As Vector2)
        Location = loc
        LocQueue.Enqueue(loc)
        If LocQueue.Count > 20 Then
            LocQueue.TryDequeue(Nothing)
        End If
    End Sub
End Class
