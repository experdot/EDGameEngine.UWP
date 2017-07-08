Imports ExampleGallery
''' <summary>
''' 画廊视图模型接口
''' </summary>
Public Interface IGalleryViewModel
    ''' <summary>
    ''' 示例组集合
    ''' </summary>
    Property SampleGroups As ObservableCollection(Of SampleGroup)
End Interface
