Imports EDGameEngine.Visuals
''' <summary>
''' 状态
''' </summary>
Public Class State
    ''' <summary>
    ''' 标识
    ''' </summary>
    Public Property Id As Integer
    ''' <summary>
    ''' 父代
    ''' </summary>
    Public Property Parent As State
    ''' <summary>
    ''' 子代
    ''' </summary>
    Public Property Children As List(Of State)
    ''' <summary>
    ''' 代数
    ''' </summary>
    Public Property Generation As Integer

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(id As Integer, parent As State, generation As Integer)
        Me.Id = id
        Me.Parent = parent
        Me.Generation = generation
        Me.Children = New List(Of State)
    End Sub
End Class
