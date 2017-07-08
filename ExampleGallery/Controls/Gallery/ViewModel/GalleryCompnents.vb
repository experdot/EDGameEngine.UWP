''' <summary>
''' 组件画廊
''' </summary>
Public Class GalleryCompnents
    Implements IGalleryViewModel
    ''' <summary>
    ''' 示例组集合
    ''' </summary>
    Public Property SampleGroups As New ObservableCollection(Of SampleGroup) Implements IGalleryViewModel.SampleGroups

    Public Sub New()
        Dim image As New BitmapImage(New Uri("ms-appx:///Assets/StoreLogo.png"))

        Dim samples1 As New ObservableCollection(Of Sample)
        samples1.Add(New Sample() With {.Title = "高斯模糊", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "磨砂玻璃", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "残影效果", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "光照效果", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "像素变换", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "水波效果", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "阴影效果", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "水流效果", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "二值变换", .Description = "描述", .Image = image})
        samples1.Add(New Sample() With {.Title = "波纹效果", .Description = "描述", .Image = image})

        Dim samples2 As New ObservableCollection(Of Sample)
        samples2.Add(New Sample() With {.Title = "标题2A", .Description = "描述", .Image = image})
        samples2.Add(New Sample() With {.Title = "标题2B", .Description = "描述", .Image = image})
        samples2.Add(New Sample() With {.Title = "标题2C", .Description = "描述", .Image = image})

        Dim samples3 As New ObservableCollection(Of Sample)
        samples3.Add(New Sample() With {.Title = "标题4A", .Description = "描述", .Image = image})
        samples3.Add(New Sample() With {.Title = "标题4B", .Description = "描述", .Image = image})
        samples3.Add(New Sample() With {.Title = "标题4C", .Description = "描述", .Image = image})

        Dim samples4 As New ObservableCollection(Of Sample)
        samples4.Add(New Sample() With {.Title = "音效控制", .Description = "描述", .Image = image})
        samples4.Add(New Sample() With {.Title = "创建物体", .Description = "描述", .Image = image})
        samples4.Add(New Sample() With {.Title = "物理仿真", .Description = "描述", .Image = image})
        samples4.Add(New Sample() With {.Title = "键盘输入", .Description = "描述", .Image = image})
        samples4.Add(New Sample() With {.Title = "平面变换", .Description = "描述", .Image = image})

        Dim samples5 As New ObservableCollection(Of Sample)
        samples5.Add(New Sample() With {.Title = "标题5A", .Description = "描述", .Image = image})
        samples5.Add(New Sample() With {.Title = "标题5B", .Description = "描述", .Image = image})
        samples5.Add(New Sample() With {.Title = "标题5C", .Description = "描述", .Image = image})

        SampleGroups.Add(New SampleGroup() With {.Title = "效果器", .Sameples = samples1})
        SampleGroups.Add(New SampleGroup() With {.Title = "音效器", .Sameples = samples2})
        SampleGroups.Add(New SampleGroup() With {.Title = "动画器", .Sameples = samples3})
        SampleGroups.Add(New SampleGroup() With {.Title = "行为器", .Sameples = samples4})
        SampleGroups.Add(New SampleGroup() With {.Title = "触发器", .Sameples = samples5})
    End Sub
End Class
