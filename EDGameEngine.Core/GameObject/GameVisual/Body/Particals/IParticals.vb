﻿Imports EDGameEngine.Core
''' <summary>
''' 粒子系统接口
''' </summary>
Public Interface IParticals
    Inherits IGameBody
    ''' <summary>
    ''' 粒子数量
    ''' </summary>
    Property Count As Integer
    ''' <summary>
    ''' 粒子集合
    ''' </summary>
    Property Particals As IEnumerable(Of IPartical)
End Interface
