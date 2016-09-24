Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 游戏可视的指针
''' </summary>
Public Class Pointer
    Inherits GameBody
    Public Location As New Vector2
    Public LocQueue As New Concurrent.ConcurrentQueue(Of Vector2)
    Public Overrides Sub StartEx()
        RemoveHandler Scene.Inputs.Mouse.MouseChanged, AddressOf MouseChanged
        AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf MouseChanged
    End Sub

    Public Overrides Sub UpdateEx()

    End Sub
    Private Sub MouseChanged(loc As Vector2)
        Location = loc - Scene.Camera.Position
        LocQueue.Enqueue(loc)
        If LocQueue.Count > 20 Then
            LocQueue.TryDequeue(Nothing)
        End If
    End Sub
End Class
