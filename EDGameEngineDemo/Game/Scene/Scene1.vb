Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Windows.UI
Public Class Scene1
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        'Dim rect As New Rect(50, 50, 30, 30)
        'Dim fill As New FillStyle(True) With {.Color = Colors.Red}
        'Dim border As New BorderStyle(True) With {.Color = Colors.Black, .Width = 1}
        'Dim rectModel As New VisualRectangle() With {.Rect = rect, .Border = border, .Fill = fill}
        'Dim circleModel As New VisualCircle() With {.Radius = 50, .Border = border, .Fill = fill}

        '几何形状
        'Me.AddGameVisual(rectModel, New RectangleView(rectModel) With {.CacheAllowed = True}, 1)
        'Me.AddGameVisual(circleModel, New CircleView(circleModel) With {.CacheAllowed = True}, 0)
        'circleModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 2.0F})
        'rectModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
        'circleModel.GameComponents.Behaviors.Add(New PhysicsScript)

        '粒子系统
        'Dim tempModel As New ParticalFollow()
        'Me.AddGameVisual(tempModel, New ParticalView(tempModel))
        'tempModel.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height)})
        'tempModel.GameComponents.Effects.Add(New FrostedEffect() With {.Amount = 10})

        '植物
        'Dim tempModel2 As New Plant(New Vector2(Width / 2, Height * 0.8))
        'Me.AddGameVisual(tempModel2, New PlantView(tempModel2))

        '自动画画
        Dim tempModel3 As New AutoDrawModel() With {.Image = ImageManager.GetResource(ImageResourceID.Scenery1)}
        'tempModel3.GameComponents.Behaviors.Add(New TransformScript)
        Me.AddGameVisual(tempModel3, New AutoDrawView(tempModel3))

        '指针
        'Dim tempModel4 As New Pointer
        'Me.AddGameVisual(tempModel4, New PointerView(tempModel4), 1)
        'tempModel4.GameComponents.Effects.Add(New StreamEffect())

        '分形
        'Dim tempModel5 As New GastonJulia
        'Me.AddGameVisual(tempModel5, New FractalView(tempModel5))

        '贴图
        'Dim tempModel6 As New Sprite() With {.Image = ImageManager.GetResource(ImageResourceID.Scenery1)}
        'Me.AddGameVisual(tempModel6, New SpriteView(tempModel6) With {.CacheAllowed = True})
        'tempModel6.GameComponents.Effects.Add(New FrostedEffect() With {.Amount = 10})
        'tempModel6.GameComponents.Behaviors.Add(New TransformScript)

        '创建物体脚本
        'Me.GameComponents.Behaviors.Add(New CreateBodyScript())
        '键盘控制摄像机
        'Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
        '场景全局残影
        'Me.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 0.5})
    End Sub
End Class
