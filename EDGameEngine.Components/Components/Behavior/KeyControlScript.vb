Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.System
Imports Windows.UI
''' <summary>
''' 键盘控制脚本
''' </summary>
Public Class KeyControlScript
    Inherits BehaviorBase
    Public Overrides Sub Start()
        AddHandler Scene.Inputs.Mouse.PointerPressed, AddressOf PointerPressed
    End Sub
    Public Property MaxSpeed As Single = 5.0F
    Public Overrides Sub Update()
        Static VecArr() As Vector2 = {New Vector2(0, -1), New Vector2(-1, 0), New Vector2(0, 1), New Vector2(1, 0)}
        Static KeyArr() As VirtualKey = {VirtualKey.W, VirtualKey.A, VirtualKey.S, VirtualKey.D}
        Static SpeedArr() As Single = {0, 0, 0, 0}
        Dim TempVec As New Vector2
        For i = 0 To 3
            If Scene.Inputs.Keyboard.KeyStatus(KeyArr(i)) Then
                SpeedArr(i) = 1
            Else
                SpeedArr(i) *= CSng(0.96)
            End If
            TempVec += VecArr(i) * SpeedArr(i) * MaxSpeed
        Next
        Target.Transform.Translation += TempVec
    End Sub
    Private Sub PointerPressed(loc As Vector2)
        Dim fill As New FillStyle(True) With {.Color = Colors.Red}
        Dim border As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}
        Dim circleModel As New VisualCircle() With {.Radius = 50, .Border = border, .Fill = fill}
        circleModel.Transform.Translation = loc - New Vector2(circleModel.Radius / 2, circleModel.Radius / 2)
        Scene.AddGameVisual(circleModel, New CircleView(circleModel) With {.CacheAllowed = True}, 0)
        circleModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 2.0F})
    End Sub
End Class
