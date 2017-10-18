''' <summary>
''' 游戏资源接口
''' </summary>
Public Interface IGameResource(Of TResourceId, TResource)
    Inherits IDisposable
    ''' <summary>
    ''' 添加资源
    ''' </summary>
    Function Add(id As TResourceId, fileName As String) As Task
    ''' <summary>
    ''' 返回指定资源
    ''' </summary>
    Function GetResource(id As TResourceId) As TResource
End Interface
