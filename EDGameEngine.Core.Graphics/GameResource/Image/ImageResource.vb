Imports Microsoft.Graphics.Canvas
''' <summary>
''' 图像资源
''' </summary>
Public Class ImageResource
    Inherits GameResourceBase(Of Integer, ICanvasImage)
    Private ResourceCreator As ICanvasResourceCreator
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(resourceCreator As ICanvasResourceCreator)
        Me.ResourceCreator = resourceCreator
    End Sub
    Public Overrides Async Function Add(id As Integer, filename As String) As Task
        Resources.Add(id, Await CanvasBitmap.LoadAsync(ResourceCreator, filename))
    End Function
End Class
