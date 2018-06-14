''' <summary>
''' 提供聚类层转换为线条的转换器接口
''' </summary>
Public Interface IHierarchyToLinesConverter
    ''' <summary>
    ''' 转换
    ''' </summary>
    Function Convert(hierarchy As IHierarchy) As List(Of ILine)
End Interface
