Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Public Class ImageResourceManager
    Inherits GameResourceManager(Of ImageResourceID, ICanvasImage)
    Dim ResourceCreator As ICanvasResourceCreator
    Sub New(ResourceCreator As ICanvasResourceCreator)
        Me.ResourceCreator = ResourceCreator
    End Sub
    Public Overrides Async Function LoadAsync() As Task
        payload.Add(ImageResourceID.TreeBranch1, Await CanvasBitmap.LoadAsync(ResourceCreator, "Image/Tree_Black.png"))
        payload.Add(ImageResourceID.TreeBranch2, Await CanvasBitmap.LoadAsync(ResourceCreator, "Image/Tree_White.png"))
        payload.Add(ImageResourceID.YellowFlower1, Await CanvasBitmap.LoadAsync(ResourceCreator, "Image/Flower_Yellow.png"))
        payload.Add(ImageResourceID.SmokePartial1, Await CanvasBitmap.LoadAsync(ResourceCreator, "Image/smoke.dds"))
        payload.Add(ImageResourceID.ExplosionPartial1, Await CanvasBitmap.LoadAsync(ResourceCreator, "Image/explosion.dds"))
        payload.Add(ImageResourceID.Back1, Await CanvasBitmap.LoadAsync(ResourceCreator, "Image/back.png"))
        payload.Add(ImageResourceID.Water1, Await CanvasBitmap.LoadAsync(ResourceCreator, "Image/Water.png"))
        payload.Add(ImageResourceID.Scenery1, Await CanvasBitmap.LoadAsync(ResourceCreator, "Image/Scenery1.png"))
    End Function
End Class
