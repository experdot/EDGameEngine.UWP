Imports EDGameEngine.Core
''' <summary>
''' 分形
''' </summary>
Public Interface IFractal
    Inherits IGameBody
    ''' <summary>
    ''' 顶点集合
    ''' </summary>
    Property Vertexs As Concurrent.ConcurrentQueue(Of Point）
End Interface
