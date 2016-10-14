Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D

    ''' <summary>
    ''' 用于描述<see cref="Shape"/>对象的质量数据
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure MassData
        ''' <summary>
        ''' 质量
        ''' </summary>
        Public Property Mass As Single
        ''' <summary>
        ''' 质心
        ''' </summary>
        Public Property Centroid As Vector2
        ''' <summary>
        ''' 转动惯量
        ''' </summary>
        Public Property InertiaMoment As Single
    End Structure
End Namespace