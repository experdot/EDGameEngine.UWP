Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class Scene_Visuals
    Inherits Scene
    Public Property Id As Integer
    Public Sub New(world As World, WindowSize As Size, id As Integer)
        MyBase.New(world, WindowSize)
        Me.Id = id
    End Sub

    Public Overrides Sub CreateObject()
        Select Case Id
            Case 10000 '直线
                Dim points As Vector2() = {New Vector2(0, 0), New Vector2(20, 0), New Vector2(20, 20)}
                Dim geo = GeometryHelper.CreateRegularPolygon(World.ResourceCreator, 6, 20)
                Dim rect As New Rect(50, 50, 30, 30)
                Dim fill As New FillStyle(True) With {.Color = Colors.Red}
                Dim border As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}
                Dim rectModel As New VisualRectangle() With {.Rect = rect, .Border = border, .Fill = fill}
                Dim circleModel As New VisualCircle() With {.Radius = 50, .Border = border, .Fill = fill}
                Dim polygonModel As New VisualPolygon() With {.Geometry = geo, .Border = border, .Fill = fill}

                Me.AddGameVisual(rectModel, New RectangleView(rectModel) With {.CacheAllowed = True}, 1)
                Me.AddGameVisual(circleModel, New CircleView(circleModel) With {.CacheAllowed = True}, 0)
                Me.AddGameVisual(polygonModel, New PolygonView(polygonModel))
                circleModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 2.0F})
                rectModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
            Case 10001 '矩形
                Throw New NotImplementedException()
            Case 10002 '圆形
                Throw New NotImplementedException()
            Case 10003 '多边形
                Throw New NotImplementedException()
            Case 20000 '粒子集群
                Throw New NotImplementedException()
            Case 20001 '水花飞溅
                Throw New NotImplementedException()
            Case 20002 '光芒四射
                Throw New NotImplementedException()
            Case 20003 '枝繁叶茂
                '渲染模式
                World.RenderMode = RenderMode.Sync
                Dim image As CanvasBitmap = CType(ImageManager.GetResource(ImageResourceId.Scenery1), CanvasBitmap)
                Dim pixels As Color() = image.GetPixelColors()
                Dim bounds As Rect = image.Bounds
                Dim tempModel As New ParticlesTree()
                'Me.AddGameVisual(tempModel, New ParticlesImageView(tempModel) With {.ImageResourceId = ImageResourceId.YellowFlower1, .ImageScale = 4.0F, .Colors = pixels, .Bounds = bounds})
                Me.AddGameVisual(tempModel, New ParticlesView(tempModel))
                Me.GameLayers(0).Background = Colors.Black
                Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height)})
            Case 30000 '朱利亚集
                Dim tempModel As New GastonJulia
                Me.AddGameVisual(tempModel, New FractalView(tempModel))
            Case 30001 '曼德布罗集
                Dim tempModel As New Mandelbrot
                Me.AddGameVisual(tempModel, New FractalView(tempModel))
            Case 30002 '迭代函数系统：树木
                Dim tempModel As New NatureTree
                Me.AddGameVisual(tempModel, New FractalView(tempModel))
            Case 40000 '生命游戏
                Dim tempModel As New SquareCA With {.Image = CType(ImageManager.GetResource(ImageResourceId.Scenery1), CanvasBitmap)}
                Me.AddGameVisual(tempModel, New GeometryCAView(tempModel))
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
            Case 40001 '水墨侵染

            Case 40002 '植物摇曳
                Dim tempModel As New Plant(New Vector2(Width / 2, Height * 0.8F))
                Me.AddGameVisual(tempModel, New PlantView(tempModel))
            Case 50000 '自动绘图
                Dim tempModel As New AutoDrawModel() With {.Image = CType(ImageManager.GetResource(ImageResourceId.Scenery1), CanvasBitmap)}
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New AutoDrawView(tempModel))
            Case 50001 '自动拼图
                Throw New NotImplementedException()
        End Select

        '指针
        'Dim tempModel4 As New Pointer
        'Me.AddGameVisual(tempModel4, New PointerView(tempModel4), 1)
        'tempModel4.GameComponents.Effects.Add(New StreamEffect())

        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c1.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c2.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c3.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c4.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c5.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c6.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c7.wav"})


        '键盘控制摄像机
        Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
        '场景全局残影
        'Me.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 1})
    End Sub

    Public Overrides Sub CreateUI()
        Return
    End Sub

    Public Overrides Async Function CreateResoucesAsync(imgResManager As ImageResourceManager) As Task
        Await imgResManager.Add(ImageResourceId.TreeBranch1, "Game/Resources/Images/Tree_Black.png")
        Await imgResManager.Add(ImageResourceId.TreeBranch2, "Game/Resources/Images/Tree_White.png")
        Await imgResManager.Add(ImageResourceId.YellowFlower1, "Game/Resources/Images/Flower_Yellow.png")
        Await imgResManager.Add(ImageResourceId.SmokeParticle1, "Game/Resources/Images/smoke.dds")
        Await imgResManager.Add(ImageResourceId.ExplosionPartial1, "Game/Resources/Images/explosion.dds")
        Await imgResManager.Add(ImageResourceId.Back1, "Game/Resources/Images/back.png")
        Await imgResManager.Add(ImageResourceId.Water1, "Game/Resources/Images/Water.png")
        Await imgResManager.Add(ImageResourceId.Scenery1, "Game/Resources/Images/Scenery14.png")
    End Function
End Class
