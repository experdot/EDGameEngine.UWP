Imports System.Numerics
''' <summary>
''' 用于描述图层或物体的基本状态
''' </summary>
Public Interface IObjectStatus
    ''' <summary>
    ''' 转换
    ''' </summary>
    ''' <returns></returns>
    Property Transform As Transform
    ''' <summary>
    ''' 外观
    ''' </summary>
    ''' <returns></returns>
    Property Appearance As Appearance
End Interface
