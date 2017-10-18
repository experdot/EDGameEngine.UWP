﻿Imports Windows.Graphics.Effects
''' <summary>
''' 效果器
''' </summary>
Public Interface IEffect
    Inherits IGameComponent
    ''' <summary>
    ''' 返回当前效果变换
    ''' </summary>
    Function Effect(source As IGraphicsEffectSource) As IGraphicsEffectSource
End Interface
