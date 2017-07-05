Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
Public Class Scene1
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()

        '渲染模式
        World.RenderMode = RenderMode.Sync

        '几何形状
        'Dim points As Vector2() = {New Vector2(0, 0), New Vector2(20, 0), New Vector2(20, 20)}
        'Dim geo = GeometryHelper.CreateRegularPolygon(World.ResourceCreator, 6, 20)
        'Dim rect As New Rect(50, 50, 30, 30)
        'Dim fill As New FillStyle(True) With {.Color = Colors.Red}
        'Dim border As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}
        'Dim rectModel As New VisualRectangle() With {.Rect = rect, .Border = border, .Fill = fill}
        'Dim circleModel As New VisualCircle() With {.Radius = 50, .Border = border, .Fill = fill}
        'Dim polygonModel As New VisualPolygon() With {.Geometry = geo, .Border = border, .Fill = fill}

        'Me.AddGameVisual(rectModel, New RectangleView(rectModel) With {.CacheAllowed = True}, 1)
        'Me.AddGameVisual(circleModel, New CircleView(circleModel) With {.CacheAllowed = True}, 0)
        'Me.AddGameVisual(polygonModel, New PolygonView(polygonModel))
        'circleModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 2.0F})
        'rectModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
        'circleModel.GameComponents.Behaviors.Add(New PhysicsScript)

        '粒子系统
        'Dim image As CanvasBitmap = CType(ImageManager.GetResource(ImageResourceId.Scenery1), CanvasBitmap)
        'Dim pixels As Color() = image.GetPixelColors()
        'Dim bounds As Rect = image.Bounds
        'Dim tempModel As New ParticlesTree()
        'Me.AddGameVisual(tempModel, New ParticlesImageView(tempModel) With {.ImageResourceId = ImageResourceId.YellowFlower1, .ImageScale = 4.0F, .Colors = pixels, .Bounds = bounds})
        'tempModel.GameComponents.Effects.Add(New GaussianBlurEffect())
        'tempModel.GameComponents.Effects.Add(New FrostedEffect() With {.Amount = 10})
        'Me.GameLayers(0).Background = Colors.Black
        'Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height)})

        '植物
        'Dim tempModel2 As New Plant(New Vector2(Width / 2, Height * 0.8))
        'Me.AddGameVisual(tempModel2, New PlantView(tempModel2))

        '自动画画
        'Dim tempModel3 As New AutoDrawModel() With {.Image = ImageManager.GetResource(ImageResourceId.Scenery1)}
        'tempModel3.GameComponents.Behaviors.Add(New TransformScript)
        'Me.AddGameVisual(tempModel3, New AutoDrawView(tempModel3))

        '指针
        'Dim tempModel4 As New Pointer
        'Me.AddGameVisual(tempModel4, New PointerView(tempModel4), 1)
        'tempModel4.GameComponents.Effects.Add(New StreamEffect())

        '分形
        'Dim tempModel5 As New Mandelbrot
        'Me.AddGameVisual(tempModel5, New FractalView(tempModel5))

        '贴图
        Dim tempModel6 As New Sprite() With {.Image = ImageManager.GetResource(ImageResourceId.Scenery1)}
        Me.AddGameVisual(tempModel6, New SpriteView(tempModel6) With {.CacheAllowed = False})
        tempModel6.GameComponents.Behaviors.Add(New TransformScript)
        tempModel6.GameComponents.Effects.Add(New PixelEffect)
        'Me.GameLayers.First.Background = Colors.Black
        'tempModel7.GameComponents.Behaviors.Add(New TransformScript)
        'tempModel7.GameComponents.Behaviors.Add(New AudioControlScript)

        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c1.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c2.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c3.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c4.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c5.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c6.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c7.wav"})

        '元胞自动机
        'Dim tempModel7 As New SquareCA With {.Image = ImageManager.GetResource(ImageResourceID.Scenery1)}
        'Me.AddGameVisual(tempModel7, New GeometryCAView(tempModel7))
        'tempModel7.GameComponents.Behaviors.Add(New TransformScript)

        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c1.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c2.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c3.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c4.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c5.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c6.wav"})
        'tempModel7.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Audio\c7.wav"})


        '创建物体脚本
        'Me.GameComponents.Behaviors.Add(New CreateBodyScript())
        '键盘控制摄像机
        'Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
        'Me.GameComponents.Effects.Add(New StreamEffect)
        '场景全局残影
        'Me.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 1})
    End Sub

    Public Overrides Sub CreateUI()

    End Sub

    Public Overrides Async Function CreateResoucesAsync(imgResManager As ImageResourceManager) As Task
        Await imgResManager.Add(ImageResourceId.TreeBranch1, "Image/Tree_Black.png")
        Await imgResManager.Add(ImageResourceId.TreeBranch2, "Image/Tree_White.png")
        Await imgResManager.Add(ImageResourceId.YellowFlower1, "Image/Flower_Yellow.png")
        Await imgResManager.Add(ImageResourceId.SmokeParticle1, "Image/smoke.dds")
        Await imgResManager.Add(ImageResourceId.ExplosionPartial1, "Image/explosion.dds")
        Await imgResManager.Add(ImageResourceId.Back1, "Image/back.png")
        Await imgResManager.Add(ImageResourceId.Water1, "Image/Water.png")
        Await imgResManager.Add(ImageResourceId.Scenery1, "Image/Scenery14.png")
    End Function
End Class
