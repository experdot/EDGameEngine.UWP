Imports System.Numerics
Imports EDGameEngine.Components.Behavior
Imports EDGameEngine.Components.Effect
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports EDGameEngine.Core.UI
Imports EDGameEngine.Visuals
Imports EDGameEngine.Visuals.AutoDraw
Imports EDGameEngine.Visuals.CA
Imports EDGameEngine.Visuals.Fractal
Imports EDGameEngine.Visuals.Particles
Imports EDGameEngine.Visuals.Plant
Imports EDGameEngine.Visuals.StateMachine
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class Scene_Visuals
    Inherits SceneWithUI
    Public Property Id As Integer
    Shared Rnd As New Random

    Public Sub New(world As WorldWithUI, WindowSize As Size, id As Integer)
        MyBase.New(world, WindowSize)
        Me.Id = id
    End Sub

    Protected Overrides Sub CreateObject()
        Select Case Id
            Case 10000 '直线
                For i = 0 To 10
                    Dim line As New VisualLine With {.Width = CSng(1 + Rnd.NextDouble * 5)}
                    line.Fill.Color = Utilities.ColorUtilities.GetRandomColor
                    For j = 0 To Rnd.Next(1, 5)
                        line.Points.Add(New Vector2(CSng(Width * Rnd.NextDouble), CSng(Height * Rnd.NextDouble)))
                    Next
                    Me.AddGameVisual(line, New LineView() With {.CacheAllowed = True})
                    line.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = CSng(1 + Rnd.NextDouble * 5)})
                Next
                Dim text As New VisualText With {.Text = "Press the arrow keys to move the lines."}
                text.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(text, New TextView(), 1)
            Case 10001 '矩形
                For i = 0 To 10
                    Dim fill As New FillStyle(True, Utilities.ColorUtilities.GetRandomColor)
                    Dim border As New BorderStyle(True, CSng(Rnd.NextDouble * 5), Utilities.ColorUtilities.GetRandomColor)
                    Dim rect As New Rect(0, 0, Rnd.Next(20, 400), Rnd.Next(20, 400))
                    Dim rectModel As New VisualRectangle() With {.Rectangle = rect, .Border = border, .Fill = fill}
                    rectModel.Transform.Translation = New Vector2(CSng(Width * Rnd.NextDouble), CSng(Height * Rnd.NextDouble))
                    Me.AddGameVisual(rectModel, New RectangleView() With {.CacheAllowed = True})
                    rectModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = CSng(1 + Rnd.NextDouble * 5)})
                Next
                Dim text As New VisualText With {.Text = "Press the arrow keys to move the rectangels."}
                text.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(text, New TextView(), 1)
                Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect() With {.Offset = Vector2.One, .Opacity = 0.96F})
            Case 10002 '圆形
                For i = 0 To 10
                    Dim fill As New FillStyle(True, Utilities.ColorUtilities.GetRandomColor)
                    Dim border As New BorderStyle(True, CSng(Rnd.NextDouble * 5), Utilities.ColorUtilities.GetRandomColor)
                    Dim circleModel As New VisualCircle() With {.Radius = CSng(Rnd.NextDouble * 200), .Border = border, .Fill = fill}
                    circleModel.Transform.Translation = New Vector2(CSng(Width * Rnd.NextDouble), CSng(Height * Rnd.NextDouble))
                    Me.AddGameVisual(circleModel, New CircleView() With {.CacheAllowed = True})
                    circleModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = CSng(1 + Rnd.NextDouble * 5)})
                Next
                Dim text As New VisualText With {.Text = "Press the arrow keys to move the circles."}
                text.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(text, New TextView(), 1)
                Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect() With {.Offset = Vector2.One, .Opacity = 0.96F})
            Case 10003 '多边形
                For i = 0 To 10
                    Dim fill As New FillStyle(True, Utilities.ColorUtilities.GetRandomColor)
                    Dim border As New BorderStyle(True, CSng(Rnd.NextDouble * 5), Utilities.ColorUtilities.GetRandomColor)
                    Dim geo = GeometryHelper.CreateRegularPolygon(CType(Scene.World, IObjectWithResourceCreator).ResourceCreator, Rnd.Next(3, 8), Rnd.Next(20, 200), CSng(Rnd.NextDouble * Math.PI))
                    Dim polygonModel As New VisualPolygon() With {.Geometry = geo, .Border = border, .Fill = fill}
                    polygonModel.Transform.Translation = New Vector2(CSng(Width * Rnd.NextDouble), CSng(Height * Rnd.NextDouble))
                    Me.AddGameVisual(polygonModel, New PolygonView())
                    polygonModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = CSng(1 + Rnd.NextDouble * 5)})
                Next
                Dim text As New VisualText With {.Text = "Press the arrow keys to move the polygons."}
                text.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(text, New TextView(), 1)
                Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect() With {.Offset = Vector2.One, .Opacity = 0.96F})
            Case 20000 '粒子集群
                'World.RenderMode = RenderMode.Sync
                Dim tempModel As New ParticlesWander() With {.Count = 100, .IsMoveToCenter = True}
                tempModel.GameComponents.Effects.Add(New GaussianBlurEffect() With {.IsDrawRaw = True})
                Me.AddGameVisual(tempModel, New ParticlesCircleView())

                Me.GameLayers(0).Background = Colors.Black
                Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect() With {.Offset = Vector2.Zero, .Opacity = 0.96F})
            Case 20001 '水花飞溅
                'World.RenderMode = RenderMode.Sync
                Dim tempModel As New ParticlesFollow() With {.Count = 500}
                tempModel.GameComponents.Effects.Add(New StreamEffect)
                Me.AddGameVisual(tempModel, New ParticlesCircleView())

                Me.GameLayers(0).Background = Colors.Black
                tempModel.GameComponents.Effects.Add(New GaussianBlurEffect() With {.IsDrawRaw = True})
            Case 20002 '烟雾缭绕
                'World.RenderMode = RenderMode.Sync
                Dim tempModel As New ParticlesSmoke() With {.Count = 500}
                tempModel.GameComponents.Effects.Add(New GaussianBlurEffect() With {.IsDrawRaw = False})
                Me.AddGameVisual(tempModel, New ParticlesImageView() With {.ImageResourceId = ImageResourceId.SmokeParticle1})

                Me.GameLayers(0).Background = Colors.Black
                Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect() With {.Offset = Vector2.Zero, .Opacity = 0.96F})
            Case 20003 '光芒四射
                Throw New NotImplementedException()
            Case 20004 '枝繁叶茂
                World.RenderMode = RenderMode.Sync
                Dim tempModel As New ParticlesTree()
                Dim tempView As IGameView
                tempView = New ParticlesCircleView()
                'tempView = New ParticlesBackgroundImageView() With
                '{
                '    .ImageResourceId = ImageResourceId.Scenery1,
                '    .ImageScale = 4.0F,
                '    .Opacity = 0.3F
                '}

                Me.AddGameVisual(tempModel, tempView)

                Me.GameLayers(0).Background = Colors.Black
                Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect)
                Me.GameLayers(0).GameComponents.Effects.Add(New StreamEffect)
                'Me.GameLayers(0).GameComponents.Effects.Add(New StreamEffect)
            Case 30000 '朱利亚集
                Dim tempModel As New GastonJulia
                Me.AddGameVisual(tempModel, New FractalView())
            Case 30001 '曼德布罗集
                Dim tempModel As New Mandelbrot
                Me.AddGameVisual(tempModel, New FractalView())
            Case 30002 '迭代函数系统：树木
                Dim tempModel As New NatureTree
                Me.AddGameVisual(tempModel, New FractalView())
            Case 40000 '生命游戏
                World.RenderMode = RenderMode.Sync
                Dim tempModel As New SquareCA With {.Image = CType(ImageResource.GetResource(ImageResourceId.YellowFlower1), CanvasBitmap)}
                Me.AddGameVisual(tempModel, New GeometryCAView())
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
            Case 40001 '水墨侵染
                World.RenderMode = RenderMode.Sync
                Dim tempModel As New InkCA With {.Image = CType(ImageResource.GetResource(ImageResourceId.InkText1), CanvasBitmap)}
                Me.AddGameVisual(tempModel, New GeometryCAView())
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                tempModel.GameComponents.Effects.Add(New GhostEffect With {.Opacity = 1.0F})
            Case 40002 '植物摇曳
                Dim tempModel As New Plant(New Vector2(Width / 2, Height * 0.8F))
                'tempModel.GameComponents.Effects.Add(New StreamEffect)
                Dim tempView As New PlantView() With
                {
                    .BranchResourceId = ImageResourceId.TreeBranch1,
                    .LeafResourceId = ImageResourceId.RedLeaf1,
                    .FlowerResourceId = ImageResourceId.YellowFlower1
                }
                Me.AddGameVisual(tempModel, tempView)
                Me.GameLayers(0).GameComponents.Effects.Add(New StreamEffect)
            Case 50000 '自动绘图
                World.RenderMode = RenderMode.Sync
                Dim image = CType(ImageResource.GetResource(ImageResourceId.Scenery_Anime), CanvasBitmap)
                Dim tempModel As New AutoDrawByFastAIModel() With {.Image = image}
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New AutoDrawView())
            Case 50001 '自动拼图
                Throw New NotImplementedException()
            Case 60000 'L系统:树
                World.RenderMode = RenderMode.Sync
                Dim tempModel As New LSystemTree
                Dim tempView As New LSystemTreeView() With
                {
                    .BranchResourceId = ImageResourceId.TreeBranch1,
                    .LeafResourceId = ImageResourceId.RedLeaf1,
                    .FlowerResourceId = ImageResourceId.YellowFlower1
                }
                Me.AddGameVisual(tempModel, tempView)
                Me.GameLayers(0).GameComponents.Effects.Add(New GhostEffect)
                Me.GameLayers(0).GameComponents.Effects.Add(New FrostedEffect With {.Amount = 2})
        End Select

        '指针
        'Dim tempModel4 As New Pointer
        'Me.AddGameVisual(tempModel4, New PointerView(tempModel4), 1)
        'tempModel4.GameComponents.Effects.Add(New StreamEffect())


        '键盘控制摄像机
        'Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
    End Sub

    Protected Overrides Sub CreateUI()
        Return
    End Sub

    Protected Overrides Async Function CreateResourcesAsync(imageResource As ImageResource) As Task
        Await imageResource.Add(ImageResourceId.TreeBranch1, "Game/Resources/Images/Tree_Black.png")
        Await imageResource.Add(ImageResourceId.TreeBranch2, "Game/Resources/Images/Tree_White.png")
        Await imageResource.Add(ImageResourceId.YellowFlower1, "Game/Resources/Images/Flower_Yellow.png")
        Await imageResource.Add(ImageResourceId.GreenLeaf1, "Game/Resources/Images/Leaf_Green.png")
        Await imageResource.Add(ImageResourceId.RedLeaf1, "Game/Resources/Images/Leaf_Red.png")
        Await imageResource.Add(ImageResourceId.SmokeParticle1, "Game/Resources/Images/Smoke.dds")
        Await imageResource.Add(ImageResourceId.ExplosionPartial1, "Game/Resources/Images/Explosion.dds")
        Await imageResource.Add(ImageResourceId.Back1, "Game/Resources/Images/Back.png")
        Await imageResource.Add(ImageResourceId.Water1, "Game/Resources/Images/Water.png")
        Await imageResource.Add(ImageResourceId.Scenery_Mountain, "Game/Resources/Images/Scenery11.png")
        Await imageResource.Add(ImageResourceId.Scenery_Painting, "Game/Resources/Images/Scenery12.png")
        Await imageResource.Add(ImageResourceId.Scenery_Meepo, "Game/Resources/Images/Scenery13.png")
        Await imageResource.Add(ImageResourceId.Scenery_Anime, "Game/Resources/Images/Scenery14.png")
        Await imageResource.Add(ImageResourceId.InkText1, "Game/Resources/Images/InkText.png")
    End Function
End Class
