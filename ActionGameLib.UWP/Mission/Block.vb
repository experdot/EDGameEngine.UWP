''' <summary>
''' 地图块
''' </summary>
Public Class Block
    ''' <summary>
    ''' 地图块贴图
    ''' </summary>
    Public Property Image As ResourceId
    ''' <summary>
    ''' 地图块方位
    ''' </summary>
    Public Property Location As Location
    ''' <summary>
    ''' 是否可见
    ''' </summary>
    Public Property Visible As Boolean
    ''' <summary>
    ''' 是否可碰撞
    ''' </summary>
    Public Property Collision As Boolean
End Class
