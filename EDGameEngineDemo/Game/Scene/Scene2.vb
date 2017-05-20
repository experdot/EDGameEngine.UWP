Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals

Public Class Scene2
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        Dim tempModel As New ParticlesFollow()
        Me.AddGameVisual(tempModel, New ParticalsView(tempModel))
        tempModel.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height)})
    End Sub
    Public Overrides Sub CreateUI()
        Dim b As New Button
        Me.AddUIElement(b, 1)
        AddHandler b.Click, Sub()
                                World.SwitchScene("Main")
                            End Sub
    End Sub

    Public Overrides Async Function CreateResoucesAsync(imgResManager As ImageResourceManager) As Task
        Await imgResManager.Add(ImageResourceID.TreeBranch1, "Image/Tree_Black.png")
        Await imgResManager.Add(ImageResourceID.TreeBranch2, "Image/Tree_White.png")
        Await imgResManager.Add(ImageResourceID.YellowFlower1, "Image/Flower_Yellow.png")
        Await imgResManager.Add(ImageResourceID.SmokePartial1, "Image/smoke.dds")
        Await imgResManager.Add(ImageResourceID.ExplosionPartial1, "Image/explosion.dds")
        Await imgResManager.Add(ImageResourceID.Back1, "Image/back.png")
        Await imgResManager.Add(ImageResourceID.Water1, "Image/Water.png")
        Await imgResManager.Add(ImageResourceID.Scenery1, "Image/Scenery1.png")
    End Function
End Class
