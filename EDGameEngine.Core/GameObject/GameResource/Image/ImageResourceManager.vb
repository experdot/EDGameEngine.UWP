Imports Microsoft.Graphics.Canvas
Public Class ImageResourceManager
    Inherits GameResourceManager(Of Integer, ICanvasImage)
    Dim ResourceCreator As ICanvasResourceCreator
    Sub New(ResourceCreator As ICanvasResourceCreator)
        Me.ResourceCreator = ResourceCreator
    End Sub
    Public Overrides Async Function Add(id As Integer, filename As String) As Task
        Resources.Add(id, Await CanvasBitmap.LoadAsync(ResourceCreator, filename))
    End Function
End Class
