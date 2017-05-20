Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 粒子接口
''' </summary>
Public Interface IParticle
    ''' <summary>
    ''' 颜色
    ''' </summary>
    Property Color As Color
    ''' <summary>
    ''' 位置
    ''' </summary>
    Property Location As Vector2
    ''' <summary>
    ''' 大小
    ''' </summary>
    Property Size As Single
End Interface
