Imports System.Numerics
Public Class PhysicUnit

    ''' <summary>
    ''' 所有方向
    ''' </summary>
    Public Shared VecAll() As Vector2 = {New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, -0.7), New Vector2(0.7, 0.7), New Vector2(-0.7, -0.7), New Vector2(-0.7, 0.7)}

    ''' <summary>
    ''' 向上方向
    ''' </summary>
    Public Shared VecUp() As Vector2 = {New Vector2(0, -1), New Vector2(0, -1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, -0.7), New Vector2(0.7, -0.7), New Vector2(-0.7, -0.7), New Vector2(-0.7, -0.7)}
    ''' <summary>
    ''' 向下方向
    ''' </summary>
    Public Shared VecDown() As Vector2 = {New Vector2(0, 1), New Vector2(0, 1), New Vector2(-1, 0), New Vector2(1, 0), New Vector2(0.7, 0.7), New Vector2(0.7, 0.7), New Vector2(-0.7, 0.7), New Vector2(-0.7, 0.7)}

    '''' <summary>
    '''' 摩擦力
    '''' </summary>
    '''' <param name="SubWalker"></param>
    'Public Shared Sub ApplyFriction(SubWalker As Partical)
    '    Dim frictionMag As Single = 0.5 '系数
    '    Dim frictionVec As New Vector2(SubWalker.Velocity.X, SubWalker.Velocity.Y) '由当前速度向量初始化
    '    frictionVec = -frictionVec '向量反向
    '    frictionVec.SetMag(frictionMag) '重新设定向量长度
    '    SubWalker.ApplyForce(frictionVec)
    'End Sub
    '''' <summary>
    '''' 引力
    '''' </summary>
    '''' <param name="SubWalker"></param>
    '''' <param name="SubWalker2"></param>
    'Public Shared Sub ApplyAttract(SubWalker As Partical, SubWalker2 As Partical)
    '    Dim gValue As Single = 1 '引力常量
    '    Dim attractVec As Vector2 = SubWalker2.Location - SubWalker.Location '由二者向量差初始化
    '    Dim aLen As Single = attractVec.Length '距离
    '    If aLen < 1 Then aLen = 1
    '    Dim attractMag As Single = (gValue * SubWalker.Mass * SubWalker2.Mass) / (aLen * aLen) '计算模长
    '    attractVec.SetMag(attractMag) '重置向量长度
    '    SubWalker.ApplyForce(-attractVec)
    '    SubWalker2.ApplyForce(-attractVec)
    'End Sub
    '''' <summary>
    '''' 流体阻力
    '''' </summary>
    '''' <param name="SubWalker"></param>
    'Public Shared Sub ApplyDrag(SubWalker As Partical)
    '    Dim vLen As Single = SubWalker.Velocity.Length
    '    Dim dragMag As Single = 0.5 * vLen * vLen '系数
    '    Dim dragVec As New Vector2(SubWalker.Velocity.X, SubWalker.Velocity.Y) '由当前速度向量初始化
    '    dragVec = -dragVec '向量反向
    '    dragVec.SetMag(dragMag) '重新设定向量长度
    '    SubWalker.ApplyForce(dragVec)
    'End Sub
End Class
