''' <summary>
''' 可视化模型画廊
''' </summary>
Public Class GalleryVisualsViewModel
    Implements IGalleryViewModel
    ''' <summary>
    ''' 示例组集合
    ''' </summary>
    Public Property SampleGroups As New ObservableCollection(Of SampleGroupViewModel) Implements IGalleryViewModel.SampleGroups

    Public Sub New()
        Dim image As New BitmapImage(New Uri("ms-appx:///Assets/StoreLogo.png"))

        Dim samples1 As New List(Of Sample)
        samples1.Add(New Sample() With {.Id = 10000, .Title = "直线", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/Line.png"))})
        samples1.Add(New Sample() With {.Id = 10001, .Title = "矩形", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/Rectangle.png"))})
        samples1.Add(New Sample() With {.Id = 10002, .Title = "圆形", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/Circle.png"))})
        samples1.Add(New Sample() With {.Id = 10003, .Title = "多边形", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/Polygon.png"))})

        Dim samples2 As New List(Of Sample)
        samples2.Add(New Sample() With {.Id = 20000, .Title = "粒子集群", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/ParticlesCluster.png"))})
        samples2.Add(New Sample() With {.Id = 20001, .Title = "水花飞溅", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/ParticlesWater.png"))})
        samples2.Add(New Sample() With {.Id = 20002, .Title = "烟雾缭绕", .Description = "描述", .Image = image})
        samples2.Add(New Sample() With {.Id = 20003, .Title = "光芒四射", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/ParticlesLight.png"))})
        samples2.Add(New Sample() With {.Id = 20004, .Title = "枝繁叶茂", .Description = "描述", .Image = New BitmapImage(New Uri("ms-appx:///Assets/SampleImages/ParticlesTree.png"))})

        Dim samples3 As New List(Of Sample)
        samples3.Add(New Sample() With {.Id = 30000, .Title = "朱利亚集", .Description = "描述", .Image = image})
        samples3.Add(New Sample() With {.Id = 30001, .Title = "曼德布罗集", .Description = "描述", .Image = image})
        samples3.Add(New Sample() With {.Id = 30002, .Title = "迭代函数系统：树木", .Description = "描述", .Image = image})

        Dim samples4 As New List(Of Sample)
        samples4.Add(New Sample() With {.Id = 40000, .Title = "生命游戏", .Description = "描述", .Image = image})
        samples4.Add(New Sample() With {.Id = 40001, .Title = "水墨侵染", .Description = "描述", .Image = image})
        samples4.Add(New Sample() With {.Id = 40002, .Title = "植物摇曳", .Description = "描述", .Image = image})

        Dim samples5 As New List(Of Sample)
        samples5.Add(New Sample() With {.Id = 50000, .Title = "自动绘图", .Description = "描述", .Image = image})
        samples5.Add(New Sample() With {.Id = 50001, .Title = "自动拼图", .Description = "描述", .Image = image})

        Dim samples6 As New List(Of Sample)
        samples6.Add(New Sample() With {.Id = 60000, .Title = "L系统树木", .Description = "描述", .Image = image})

        SampleGroups.Add(New SampleGroupViewModel() With {.Title = "几何图元", .Sameples = samples1})
        SampleGroups.Add(New SampleGroupViewModel() With {.Title = "粒子系统", .Sameples = samples2})
        SampleGroups.Add(New SampleGroupViewModel() With {.Title = "分形几何", .Sameples = samples3})
        SampleGroups.Add(New SampleGroupViewModel() With {.Title = "模拟自然", .Sameples = samples4})
        SampleGroups.Add(New SampleGroupViewModel() With {.Title = "游戏智能", .Sameples = samples5})
        SampleGroups.Add(New SampleGroupViewModel() With {.Title = "L系统", .Sameples = samples6})
    End Sub
End Class
