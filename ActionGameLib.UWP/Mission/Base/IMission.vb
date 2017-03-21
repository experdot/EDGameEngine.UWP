Imports ActionGameLib.UWP
''' <summary>
''' 关卡接口
''' </summary>
Public Interface IMission
    Inherits IUpdateable
    ''' <summary>
    ''' 地图块集合
    ''' </summary>
    Property Blocks As IBlock(,)
    ''' <summary>
    ''' 角色集合
    ''' </summary>
    Property Characters As List(Of ICharacter)
End Interface
