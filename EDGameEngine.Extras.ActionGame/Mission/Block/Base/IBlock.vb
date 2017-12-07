Imports System.Numerics

Public Interface IBlock
    Inherits ITransform, ICollision, IUpdateable
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
