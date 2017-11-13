﻿Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Components.Behavior
Imports EDGameEngine.Components.Effect
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports EDGameEngine.Core.UI
Imports EDGameEngine.Visuals
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class Scene_Compnents
    Inherits SceneWithUI
    Public Property Id As Integer
    Public Sub New(world As WorldWithUI, WindowSize As Size, id As Integer)
        MyBase.New(world, WindowSize)
        Me.Id = id
    End Sub

    Protected Overrides Sub CreateObject()
        Select Case Id
            Case 1000 '高斯模糊
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New GaussianBlurEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1001 '磨砂玻璃
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New FrostedEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1002 '残影效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New GhostEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1003 '光照效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New LightEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1004 '像素变换
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New PixelEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1005 '水波效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim bmp As CanvasBitmap = CType(image, CanvasBitmap)
                Dim tempModel As New Sprite() With {.Image = bmp}
                tempModel.GameComponents.Effects.Add(New RippleEffect(CInt(bmp.Bounds.Width), CInt(bmp.Bounds.Height)))
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1006 '阴影效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New ShadowEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1007 '水流效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New StreamEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1008 '二值变换
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New ThresholdEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))
            Case 1009 '波纹效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New WaveEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView(tempModel))

            Case 4000 '音效控制
            Case 4001 '创建物体
            Case 4002 '物理仿真
            Case 4003 '键盘输入
            Case 4004 '平面变换

        End Select

        '键盘控制摄像机
        Me.Camera.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
    End Sub

    Protected Overrides Sub CreateUI()
        Return
    End Sub

    Protected Overrides Async Function CreateResourcesAsync(imageResourceManager As ImageResource) As Task
        Await imageResourceManager.Add(ImageResourceId.Scenery1, "Game/Resources/Images/Scenery14.png")
    End Function
End Class
