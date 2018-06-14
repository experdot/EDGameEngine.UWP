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
        For Each block In Blocks
            block.Start()
        Next
        For Each character In Characters
            character.Start()
        Next
    End Sub
    Public Sub Update() Implements IUpdateable.Update
        For Each block In Blocks
            block.Update()
        Next
        For Each character In Characters
            character.Update()
        Next
    End Sub
    ''' <summary>
    ''' 返回指定名称的角色
    ''' </summary>
    Public Function FindCharacterByName(name As String) As ICharacter
        For Each character In Characters
            If character.Name = name Then
                Return character
            End If
        Next
        Return Nothing
    End Function

End Class
