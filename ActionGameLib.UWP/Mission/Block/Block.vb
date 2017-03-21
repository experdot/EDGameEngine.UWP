Imports System.Numerics
Imports ActionGameLib.UWP
''' <summary>
''' 地图块
''' </summary>
Public Class Block
    Implements IBlock
    ''' <summary>
    ''' 地图块贴图
    ''' </summary>
    Public Property Image As ResourceId Implements IBlock.Image
    ''' <summary>
    ''' 位置
    ''' </summary>
    Public Property Location As Vector2 Implements IBlock.Location
    ''' <summary>
    ''' 角度
    ''' </summary>
    Public Property Rotation As Single Implements IRotateble.Rotation
    ''' <summary>
    ''' 方向
    ''' </summary>
    Property Direction As Directions Implements IBlock.Direction
    ''' <summary>
    ''' 是否可见
    ''' </summary>
    Public Property Visible As Boolean Implements IBlock.Visible
    ''' <summary>
    ''' 物理碰撞器
    ''' </summary>
    Public Property Collide As Collide Implements IBlock.Collide

    Public Sub New()
        Collide = New Collide
    End Sub

    Public Sub Start() Implements IUpdateable.Start
        Collide.SyncTransform(Me)
    End Sub

    Public Sub Update() Implements IUpdateable.Update
        Collide.SyncTransform(Me)
    End Sub
End Class
