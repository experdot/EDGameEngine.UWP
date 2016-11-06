Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 场景摄像机
''' </summary>
Public Class Camera
    Inherits GameBody
    Implements ICamera
    ''' <summary>
    ''' 摄像机位置
    ''' </summary>
    Public Property Position As Vector2 Implements ICamera.Position
        Get
            Return Transform.Translation
        End Get
        Set(value As Vector2)
            Transform.Translation = value
        End Set
    End Property
    Public Overrides Sub StartEx()

    End Sub
    Public Overrides Sub UpdateEx()

    End Sub
End Class
