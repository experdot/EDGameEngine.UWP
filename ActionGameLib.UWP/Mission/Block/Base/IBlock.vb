Imports System.Numerics
Imports ActionGameLib.UWP

Public Interface IBlock
    Inherits ITransform, ICollision
    ''' <summary>
    ''' 方向
    ''' </summary>
    Property Direction As Directions
    ''' <summary>
    ''' 贴图
    ''' </summary>
    Property Image As ResourceId
    ''' <summary>
    ''' 可见性
    ''' </summary>
    Property Visible As Boolean
End Interface
