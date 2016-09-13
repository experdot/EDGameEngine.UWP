Imports System.Numerics
Imports EDGameEngine
''' <summary>
''' 场景摄像机
''' </summary>
Public Class Camera
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

    Public Property Transform As Transform = Transform.Normal Implements IVisualObject.Transform
    Public Property Appearance As Appearance = Appearance.Normal Implements IVisualObject.Appearance
    Public Property Scene As IScene Implements IVisualObject.Scene
    Public Property GameComponents As New GameComponents(Me) Implements IVisualObject.GameComponents

    Public Overridable Sub Start() Implements ICamera.Start
        GameComponents.Start()
    End Sub
    Public Overridable Sub Update() Implements ICamera.Update
        GameComponents.Update()
    End Sub
End Class
