Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.System
''' <summary>
''' 键盘控制脚本
''' </summary>
Public Class KeyControlScript
    Inherits BehaviorBase
    Public Overrides Sub Start()
    End Sub
    Public Property MaxSpeed As Single = 5.0F
    Public Overrides Sub Update()
        Static VecArr() As Vector2 = {New Vector2(0, -1), New Vector2(-1, 0), New Vector2(0, 1), New Vector2(1, 0)}
        Static KeyArr() As VirtualKey = {VirtualKey.Down, VirtualKey.Right, VirtualKey.Up, VirtualKey.Left}
        Static SpeedArr() As Single = {0, 0, 0, 0}
        Dim tempVec As New Vector2
        For i = 0 To 3
            If Scene.Inputs.Keyboard.KeyStatus(KeyArr(i)) Then
                SpeedArr(i) = 1
            Else
                SpeedArr(i) *= CSng(0.96)
            End If
            tempVec += VecArr(i) * SpeedArr(i) * MaxSpeed
        Next
        Target.Transform.Translation += tempVec
    End Sub
End Class
