Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    ''' <summary>
    ''' 距离计算输出结构体
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure DistanceOutput
        ''' <summary>
        ''' 点A
        ''' </summary>
        Public Property PointA As Vector2
        ''' <summary>
        ''' 点B
        ''' </summary>
        Public Property PointB As Vector2
        ''' <summary>
        ''' 距离
        ''' </summary>
        Public Property Distance As Single
        ''' <summary>
        ''' 迭代次数
        ''' </summary>
        Public Property Iterations As Integer
    End Structure
End Namespace

