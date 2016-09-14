Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 摄像机控制脚本
''' </summary>
Public Class CameraScript
    Inherits BehaviorBase
    Public Overrides Sub Start()

    End Sub
    Public Overrides Sub Update()
        Static TempSingle As Single
        TempSingle = (TempSingle + 0.01) Mod (Math.PI * 2)
        Camera.Position = New Vector2(100, 0) * Math.Sin(TempSingle)
    End Sub
End Class
