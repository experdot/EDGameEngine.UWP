''' <summary>
''' 可视化模型画廊
''' </summary>
Public Class GalleryVisuals
    Implements IGalleryViewModel
    ''' <summary>
    ''' 示例组集合
    ''' </summary>
    Public Property SampleGroups As New ObservableCollection(Of SampleGroup) Implements IGalleryViewModel.SampleGroups

    Public Sub New()
        Dim image As New BitmapImage(New Uri("ms-appx:///Assets/StoreLogo.png"))

        Dim samples1 As New ObservableCollection(Of Sample)
        samples1.Add(New Sample() With {.Title = "直线", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/Line.png"))})
        samples1.Add(New Sample() With {.Title = "矩形", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/Rectangle.png"))})
        samples1.Add(New Sample() With {.Title = "圆形", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/Circle.png"))})
        samples1.Add(New Sample() With {.Title = "多边形", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/Polygon.png"))})

        Dim samples2 As New ObservableCollection(Of Sample)
        samples2.Add(New Sample() With {.Title = "粒子集群", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/ParticlesCluster.png"))})
        samples2.Add(New Sample() With {.Title = "水花飞溅", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/ParticlesWater.png"))})
        samples2.Add(New Sample() With {.Title = "光芒四射", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/ParticlesLight.png"))})
        samples2.Add(New Sample() With {.Title = "枝繁叶茂", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/ParticlesTree.png"))})

        Dim samples3 As New ObservableCollection(Of Sample)
        samples3.Add(New Sample() With {.Title = "朱利亚集", .Description = "描述", .Image = image})
        samples3.Add(New Sample() With {.Title = "曼德布罗集", .Description = "描述", .Image = image})
        samples3.Add(New Sample() With {.Title = "迭代函数系统：树木", .Description = "描述", .Image = image})

        Dim samples4 As New ObservableCollection(Of Sample)
        samples4.Add(New Sample() With {.Title = "生命游戏", .Description = "描述", .Image = image})
        samples4.Add(New Sample() With {.Title = "水墨侵染", .Description = "描述", .Image = image})
        samples4.Add(New Sample() With {.Title = "植物摇曳", .Description = "描述", .Image = image})

        Dim samples5 As New ObservableCollection(Of Sample)
        samples5.Add(New Sample() With {.Title = "自动绘图", .Description = "描述", .Image = image})
        samples5.Add(New Sample() With {.Title = "自动拼图", .Description = "描述", .Image = image})

        SampleGroups.Add(New SampleGroup() With {.Title = "几何图元", .Sameples = samples1})
        SampleGroups.Add(New SampleGroup() With {.Title = "粒子系统", .Sameples = samples2})
        SampleGroups.Add(New SampleGroup() With {.Title = "分形几何", .Sameples = samples3})
        SampleGroups.Add(New SampleGroup() With {.Title = "模拟自然", .Sameples = samples4})
        SampleGroups.Add(New SampleGroup() With {.Title = "游戏智能", .Sameples = samples5})
    End Sub
End Class
