''' <summary>
''' 表示一个关卡数据
''' </summary>
Public Class MissionData
    ''' <summary>
    ''' 背景音乐
    ''' </summary>
    ''' <returns></returns>
    Public Property Music As ResourceId
    ''' <summary>
    ''' 背景贴图
    ''' </summary>
    ''' <returns></returns>
    Public Property Background As ResourceId
    ''' <summary>
    ''' 关卡包含的地图块
    ''' </summary>
    Public Property Blocks As Block(,)
    ''' <summary>
    ''' 关卡包含的角色
    ''' </summary>
    Public Property Characters As List(Of ICharacter)
End Class
