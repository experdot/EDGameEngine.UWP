''' <summary>
''' 顶点集提供器接口
''' </summary>
Public Interface IVertexWithLayerProvider
    ''' <summary>
    ''' 是否结束
    ''' </summary>
    Property IsOver As Boolean
    ''' <summary>
    ''' 返回下一个顶点
    ''' </summary>
    Function NextPoint() As VertexWithLayer
End Interface
