''' <summary>
''' 游戏资源基类
''' </summary>
Public MustInherit Class GameResourceBase(Of TResourceId, TResource)
    Implements IGameResource(Of TResourceId, TResource)
    Protected Resources As New Dictionary(Of TResourceId, TResource)
    ''' <summary>
    ''' 添加资源
    ''' </summary>
    Public MustOverride Function Add(id As TResourceId, fileName As String) As Task Implements IGameResource(Of TResourceId, TResource).Add
    ''' <summary>
    ''' 返回指定的资源
    ''' </summary>
    Public Function GetResource(id As TResourceId) As TResource Implements IGameResource(Of TResourceId, TResource).GetResource
        Return Resources(id)
    End Function
    ''' <summary>
    ''' 释放资源
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        Resources.Clear()
    End Sub
End Class
