Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.System
''' <summary>
''' 音效控制脚本
''' </summary>
Public Class AudioControlScript
    Inherits BehaviorBase

    Public Overrides Sub Start()
        AddHandler Scene.Inputs.Keyboard.KeyDown, AddressOf KeyDown
    End Sub

    Public Overrides Sub Update()

    End Sub

    Private Sub KeyDown(KeyCode As VirtualKey)
        Static KeyArr() As VirtualKey = {VirtualKey.W, VirtualKey.A, VirtualKey.S, VirtualKey.D}
        For i = 0 To 3
            If KeyCode = (KeyArr(i)) Then
                Try
                    Target.GameComponents.Sounds.Items(i).Play()
                Catch
                End Try
                Exit For
            End If
        Next
    End Sub
End Class
