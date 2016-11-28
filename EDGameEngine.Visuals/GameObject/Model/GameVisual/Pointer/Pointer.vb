Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 游戏可视的指针
''' </summary>
Public Class Pointer
    Inherits GameBody
    Public Location As New Vector2
    Public LocQueue As New Concurrent.ConcurrentQueue(Of Vector2)
    Private MouseLocation As Vector2
    Public Overrides Sub StartEx()
        RemoveHandler Scene.Inputs.Mouse.MouseChanged, AddressOf MouseChanged
        AddHandler Scene.Inputs.Mouse.MouseChanged, AddressOf MouseChanged
    End Sub

    Public Overrides Sub UpdateEx()
        Location = MouseLocation - Scene.Camera.Position
        If LocQueue.Count > 1 Then
            LocQueue.TryDequeue(Nothing)
        End If
        'If LocQueue.Count = 1 Then
        '    LocQueue.TryDequeue(Nothing)
        '    LocQueue.Enqueue(Location)
        'End If
    End Sub
    Private Sub MouseChanged(loc As Vector2)
        MouseLocation = loc
        Location = MouseLocation - Scene.Camera.Position
        LocQueue.Enqueue(Location)
        If LocQueue.Count > 20 Then
            LocQueue.TryDequeue(Nothing)
        End If
    End Sub
End Class
