Public Interface IGameResourceManager(Of TResID, TResource)
    Inherits IDisposable
    ''' <summary>
    ''' 添加资源
    ''' </summary>
    Function Add(id As TResID, filename As String) As Task
    ''' <summary>
    ''' 返回指定资源
    ''' </summary>
    Function GetResource(ID As TResID) As TResource
End Interface
