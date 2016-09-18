Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.System
''' <summary>
''' 摄像机控制脚本
''' </summary>
Public Class CameraScript
    Inherits BehaviorBase
    Public Overrides Sub Start()

    End Sub

    Public Overrides Sub Update()
        Static Mutlti As Single = 3.0F
        Static VecArr() As Vector2 = {New Vector2(0, -Mutlti), New Vector2(-Mutlti, 0), New Vector2(0, Mutlti), New Vector2(Mutlti, 0)}
        Static KeyArr() As VirtualKey = {VirtualKey.W, VirtualKey.A, VirtualKey.S, VirtualKey.D}
        Static SpeedArr() As Single = {0, 0, 0, 0}
        Dim TempVec As New Vector2
        For i = 0 To 3
            If Scene.Inputs.Keyboard.KeyStatus(KeyArr(i)) Then
                SpeedArr(i) = 1
            Else
                SpeedArr(i) *= 0.96
            End If
            TempVec += VecArr(i) * SpeedArr(i)
        Next
        Camera.Position += TempVec
    End Sub
End Class
