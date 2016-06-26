Public MustInherit Class GameResourceManager(Of TResID, TResource)
    Implements IGameResourceManager(Of TResID, TResource)
    Protected payload As New Dictionary(Of TResID, TResource)
    Sub New()

    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        payload.Clear()
    End Sub
    Public Function GetResource(ID As TResID) As TResource Implements IGameResourceManager(Of TResID, TResource).GetResource
        Return payload(ID)
    End Function
    MustOverride Async Function LoadAsync() As Task Implements IGameResourceManager(Of TResID, TResource).LoadAsync
End Class
