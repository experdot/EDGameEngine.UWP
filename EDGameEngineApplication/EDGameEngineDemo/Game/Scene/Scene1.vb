Imports EDGameEngine
Public Class Scene1
    Inherits Scene
    Public Sub New(WindowSize As Size)
        MyBase.New(WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        Dim rect As New Rect(60, 40, 20, 20)
        Dim rectModel As New VisualRectangle(rect)
        Me.AddGameVisual(rectModel, New RectangleView(rectModel))

        rectModel.GameComponents.Effects.Add(New LightEffect())
        rectModel.GameComponents.Behaviors.Add(New TransformScript())

        'Dim tempModel As New ParticalFollow()
        'Me.AddGameVisual(tempModel, New ParticalView(tempModel))
        'tempModel.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, MyScene.Width, MyScene.Height)})

        'Dim tempModel2 As New Plant(New Vector2(MyScene.Width / 2, MyScene.Height * 0.8))
        'Me.AddGameVisual(tempModel2, New PlantView(tempModel2))

        'Dim tempModel3 As New AutoDrawModel() With {.Image = MyScene.ImageManager.GetResource(ImageResourceID.Scenery1)}
        'Me.AddGameVisual(tempModel3, New AutoDrawView(tempModel3))
    End Sub
End Class
