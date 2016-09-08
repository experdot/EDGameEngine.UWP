Imports System.Numerics
''' <summary>
''' 场景摄像机
''' </summary>
Public Class Camera
    Implements ICamera
    ''' <summary>
    ''' 摄像机2D变换
    ''' </summary>
    Public Property Transform As Transform = Transform.Normal
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
    Public Sub Start() Implements ICamera.Start
    End Sub
    Public Sub Update() Implements ICamera.Update
    End Sub
End Class
