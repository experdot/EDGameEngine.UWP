Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Components.Behavior
Imports EDGameEngine.Components.Effect
Imports EDGameEngine.Components.Audio
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
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 1001 '磨砂玻璃
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New FrostedEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 1002 '残影效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Behaviors.Add(New KeyControlScript With {.MaxSpeed = 5.0F})
                Me.AddGameVisual(tempModel, New SpriteView())
                GameLayers.First.GameComponents.Effects.Add(New GhostEffect() With {.Offset = New Vector2(1, 1), .Opacity = 0.96F})
            Case 1003 '光照效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New LightEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 1004 '像素变换
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New PixelEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 1005 '水波效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim bmp As CanvasBitmap = CType(image, CanvasBitmap)
                Dim tempModel As New Sprite() With {.Image = bmp}
                tempModel.GameComponents.Effects.Add(New RippleEffect(CInt(bmp.Bounds.Width), CInt(bmp.Bounds.Height)))
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 1006 '阴影效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New ShadowEffect With {.IsDrawRaw = True, .Offset = New Vector2(1, 1)})
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 1007 '水流效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New StreamEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 1008 '二值变换
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New ThresholdEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 1009 '波纹效果
                Dim image As ICanvasImage = ImageResource.GetResource(ImageResourceId.Scenery1)
                Dim tempModel As New Sprite() With {.Image = CType(image, CanvasBitmap)}
                tempModel.GameComponents.Effects.Add(New WaveEffect)
                tempModel.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(tempModel, New SpriteView())
            Case 4000 '音效控制
                Dim tempModel As New EmptyBody
                tempModel.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Game\Resources\Sounds\c1.wav"})
                tempModel.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Game\Resources\Sounds\c2.wav"})
                tempModel.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Game\Resources\Sounds\c3.wav"})
                tempModel.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Game\Resources\Sounds\c4.wav"})
                tempModel.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Game\Resources\Sounds\c5.wav"})
                tempModel.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Game\Resources\Sounds\c6.wav"})
                tempModel.GameComponents.Sounds.Add(New Audio With {.AudioFileName = "Game\Resources\Sounds\c7.wav"})
                tempModel.GameComponents.Behaviors.Add(New AudioControlScript())
                Me.AddGameVisual(tempModel, Nothing)
                Dim text As New VisualText With {.Text = "Press the Num0~Num6 keys to play sounds."}
                text.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(text, New TextView(), 1)
            Case 4001 '创建物体
                Dim tempModel As New EmptyBody
                tempModel.GameComponents.Behaviors.Add(New CreateBodyScript)
                Me.AddGameVisual(tempModel, Nothing)
                Dim text As New VisualText With {.Text = "Click left mouse button to create objects."}
                text.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(text, New TextView(), 1)
            Case 4002 '物理仿真
                Dim tempModel As New EmptyBody
                tempModel.GameComponents.Behaviors.Add(New PhysicsScript With {.IgnoreGravity = True})
                Me.AddGameVisual(tempModel, Nothing)
                Dim text As New VisualText With {.Text = "Click left mouse button to create objects."}
                text.GameComponents.Behaviors.Add(New TransformScript)
                Me.AddGameVisual(text, New TextView(), 1)

                GameLayers.First.GameComponents.Effects.Add(New GhostEffect() With {.Offset = New Vector2(0, 0), .Opacity = 0.96F})
            Case 4003 '键盘输入
            Case 4004 '平面变换
        End Select
    End Sub

    Protected Overrides Sub CreateUI()
        Return
    End Sub

    Protected Overrides Async Function CreateResourcesAsync(imageResourceManager As ImageResource) As Task
        Await imageResourceManager.Add(ImageResourceId.Scenery1, "Game/Resources/Images/Scenery14.png")
    End Function
End Class

