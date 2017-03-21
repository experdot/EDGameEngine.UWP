Imports FarseerPhysics.Dynamics
''' <summary>
''' [Experimental]碰撞器
''' </summary>
Public Class Collide
    ''' <summary>
    ''' 碰撞矩形
    ''' </summary>
    Public Property Rect As Rect = New Rect(0, 0, 1, 1)
    ''' <summary>
    ''' [Disabled]有效性
    ''' </summary>
    Public Property Enabeld As Boolean = True
    ''' <summary>
    ''' 碰撞实体
    ''' </summary>
    Public Property Body As Body
    ''' <summary>
    ''' 同步平面变换
    ''' </summary>
    Public Sub SyncTransform(target As ITransform)
        target.Location = Body.Position
        target.Rotation = Body.Rotation
    End Sub
End Class
