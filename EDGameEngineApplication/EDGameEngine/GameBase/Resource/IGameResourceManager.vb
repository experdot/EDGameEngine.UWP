Public Interface IGameResourceManager(Of TResID, TResource)
    Inherits IDisposable
    Function LoadAsync() As Task
    Function GetResource(ID As TResID) As TResource
End Interface
