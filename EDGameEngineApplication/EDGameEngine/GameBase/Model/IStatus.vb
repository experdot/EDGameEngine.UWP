Imports System.Numerics
''' <summary>
''' 用于描述图层或物体的基本状态
''' </summary>
Public Interface IObjectStatus
    ''' <summary>
    ''' 位置
    ''' </summary>
    ''' <returns></returns>
    Property Location As Vector2
    ''' <summary>
    ''' 缩放
    ''' </summary>
    ''' <returns></returns>
    Property Scale As Vector2
    ''' <summary>
    ''' 旋转
    ''' </summary>
    ''' <returns></returns>
    Property Rotation As Single
    ''' <summary>
    ''' 可见性
    ''' </summary>
    ''' <returns></returns>
    Property Visible As Boolean
    ''' <summary>
    ''' 有效性
    ''' </summary>
    ''' <returns></returns>
    Property Enabled As Boolean
    ''' <summary>
    ''' 不透明度
    ''' </summary>
    ''' <returns></returns>
    Property Opacity As Boolean
End Interface
