''' <summary>
''' 表示指定参数类型的可更新状态的元素
''' </summary>
Public Interface ITypedUpdateable(Of T)
    ''' <summary>
    ''' 开始
    ''' </summary>
    Sub Start(target As T)
    ''' <summary>
    ''' 更新
    ''' </summary>
    Sub Update(target As T)
End Interface