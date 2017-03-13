''' <summary>
''' 游戏资源管理器基类
''' </summary>
Public MustInherit Class GameResourceManager(Of TResID, TResource)
    Implements IGameResourceManager(Of TResID, TResource)
    Protected Resources As New Dictionary(Of TResID, TResource)
    ''' <summary>
    ''' 添加资源
    ''' </summary>
    Public MustOverride Function Add(id As TResID, filename As String) As Task Implements IGameResourceManager(Of TResID, TResource).Add
    ''' <summary>
    ''' 返回指定的资源
    ''' </summary>
    Public Function GetResource(id As TResID) As TResource Implements IGameResourceManager(Of TResID, TResource).GetResource
        Return Resources(id)
    End Function
    ''' <summary>
    ''' 释放资源
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        Resources.Clear()
    End Sub
End Class
