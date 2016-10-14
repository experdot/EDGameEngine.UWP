Imports System

Namespace Global.Box2D
    ''' <summary>
    ''' 描述<see cref="Segment"/>对象切割结果的枚举
    ''' </summary>
    Public Enum SegmentCollide
        ' Fields
        ''' <summary>
        ''' 切割成功
        ''' </summary>
        Hit = 1
        ''' <summary>
        ''' 无法切割
        ''' </summary>
        Miss = 0
        ''' <summary>
        ''' 切割起始点在形状内
        ''' </summary>
        StartsInside = -1
    End Enum
End Namespace

