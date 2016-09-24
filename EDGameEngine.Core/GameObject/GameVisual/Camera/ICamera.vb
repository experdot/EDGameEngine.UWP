Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 表示场景摄像机
''' </summary>
Public Interface ICamera
    Inherits IGameVisual
    ''' <summary>
    ''' 摄像机位置
    ''' </summary>
    Property Position As Vector2
End Interface
