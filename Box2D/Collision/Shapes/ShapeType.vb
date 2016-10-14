Imports System

Namespace Global.Box2D
    ''' <summary>
    ''' 用于描述<see cref="Shape"/>的形状类型的枚举
    ''' </summary>
    Public Enum ShapeType
        ''' <summary>
        ''' 未知的
        ''' </summary>
        Unknown = -1
        ''' <summary>
        ''' 圆形
        ''' </summary>
        Circle = 0
        ''' <summary>
        ''' 多边形
        ''' </summary>
        Polygon = 1
        ''' <summary>
        ''' 形状数量
        ''' </summary>
        TypeCount = 2
    End Enum
End Namespace

