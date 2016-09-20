Imports System.Numerics
''' <summary>
''' 表示场景摄像机
''' </summary>
Public Interface ICamera
    Inherits IGameVisual
    ''' <summary>
    ''' 位置
    ''' </summary>
    Property Position As Vector2
    ''' <summary>
    ''' 初始化
    ''' </summary>
    Sub Start()
    ''' <summary>
    ''' 更新
    ''' </summary>
    Sub Update()
End Interface
