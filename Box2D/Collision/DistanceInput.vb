Imports System
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    ''' <summary>
    ''' 距离计算输入结构体
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure DistanceInput
        ''' <summary>
        ''' 平面信息A
        ''' </summary>
        ''' <returns></returns>
        Public Property TransformA As XForm
        ''' <summary>
        ''' 平面信息B
        ''' </summary>
        ''' <returns></returns>
        Public Property TransformB As XForm
        ''' <summary>
        ''' 是否使用半径计算模式
        ''' </summary>
        Public Property useRadii As Boolean
    End Structure
End Namespace

