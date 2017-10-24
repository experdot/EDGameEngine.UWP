Imports ActionGameLib.UWP
''' <summary>
''' 关卡
''' </summary>
Public Class Mission
    Implements IMission
    ''' <summary>
    ''' 地图块集合
    ''' </summary>
    Public Property Blocks As List(Of IBlock) Implements IMission.Blocks
    ''' <summary>
    ''' 角色集合
    ''' </summary>
    Public Property Characters As List(Of ICharacter) Implements IMission.Characters
    ''' <summary>
    ''' 背景音乐
    ''' </summary>
    Public Property Music As ResourceId
    ''' <summary>
    ''' 背景贴图
    ''' </summary>
    Public Property Background As ResourceId
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        Characters = New List(Of ICharacter)
    End Sub

    Public Sub Start() Implements IUpdateable.Start
        For Each SubBlock In Blocks
            SubBlock.Start()
        Next
        For Each SubCharacter In Characters
            SubCharacter.Start()
        Next
    End Sub
    Public Sub Update() Implements IUpdateable.Update
        For Each SubBlock In Blocks
            SubBlock.Update()
        Next
        For Each SubCharacter In Characters
            SubCharacter.Update()
        Next
    End Sub
    ''' <summary>
    ''' 返回指定名称的角色
    ''' </summary>
    Public Function FindCharacterByName(name As String) As ICharacter
        For Each SubCharacter In Characters
            If SubCharacter.Name = name Then
                Return SubCharacter
            End If
        Next
        Return Nothing
    End Function

End Class
