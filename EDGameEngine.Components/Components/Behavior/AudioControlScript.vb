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
        Static KeyArr() As VirtualKey = {VirtualKey.Number0, VirtualKey.Number1, VirtualKey.Number2, VirtualKey.Number3, VirtualKey.Number4, VirtualKey.Number5, VirtualKey.Number6, VirtualKey.Number7}
        For i = 0 To 6
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
