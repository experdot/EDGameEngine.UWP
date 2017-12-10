''' <summary>
''' 可视化的游戏物体基类
''' </summary>
Public MustInherit Class GameBody
    Inherits GameVisualBase
    Implements IGameBody
    ''' <summary>
    ''' <see cref="Random"/>类的静态实例
    ''' </summary>
    Public Shared Property Rnd As New Random

    Public Overrides Sub Start()
        StartEx()
        GameComponents.Start()
    End Sub
    Public Overrides Sub Update()
        UpdateEx()
        GameComponents.Update()
    End Sub

    Public MustOverride Sub StartEx()
    Public MustOverride Sub UpdateEx()

End Class
