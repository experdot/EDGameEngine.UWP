Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 位图缓存辅助类
''' </summary>
Public Class BitmapCacheHelper
    Public Shared Function CacheImage(resourceCreator As ICanvasResourceCreator, Source As ICanvasImage) As CanvasBitmap
        Dim reg = Source.GetBounds(resourceCreator)
        If reg.Width < 1 Then reg.Width = 1
        If reg.Height < 1 Then reg.Height = 1
        Using cac = New CanvasRenderTarget(CType(resourceCreator, ICanvasResourceCreatorWithDpi), CSng(reg.Width), CSng(reg.Height))
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.Clear(Colors.Transparent)
                ds.DrawImage(Source, CSng(-reg.Left), CSng(-reg.Top))
            End Using
            Return CanvasBitmap.CreateFromColors(resourceCreator, cac.GetPixelColors, CInt(sizepx.Width), CInt(sizepx.Height))
        End Using
    End Function
    Public Shared Function CacheEntireImage(resourceCreator As ICanvasResourceCreator, Source As ICanvasImage) As CanvasBitmap
        Dim reg = Source.GetBounds(resourceCreator)
        If reg.Width < 1 Then reg.Width = 1
        If reg.Height < 1 Then reg.Height = 1
        If Double.IsInfinity(reg.Left) Then reg.X = 0
        If Double.IsInfinity(reg.Top) Then reg.Y = 0
        Return CacheImageClip(resourceCreator, Source, reg)
    End Function

    Public Shared Function CacheImageClip(resourceCreator As ICanvasResourceCreator, Source As ICanvasImage, reg As Rect) As CanvasBitmap
        Using cac = New CanvasRenderTarget(CType(resourceCreator, ICanvasResourceCreatorWithDpi), CSng(reg.Width + reg.Left), CSng(reg.Height + reg.Top))
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.Clear(Colors.Transparent)
                ds.DrawImage(Source)
            End Using
            Return CanvasBitmap.CreateFromColors(resourceCreator, cac.GetPixelColors, CInt(sizepx.Width), CInt(sizepx.Height))
        End Using
    End Function

    Public Shared Sub SaveAsPng(cac As CanvasRenderTarget)
        Dim func = Task.Run(Async Function()
                                Dim fi = Task.Run(Async Function()
                                                      Return Await (Await Windows.Storage.KnownFolders.SavedPictures.CreateFileAsync($"cache{Date.Now.ToBinary.ToString("X")}.png")).OpenAsync(Windows.Storage.FileAccessMode.ReadWrite)
                                                  End Function)
                                fi.Wait()
                                Await cac.SaveAsync(fi.Result, CanvasBitmapFileFormat.Png)
                            End Function)
        func.Wait()
    End Sub
End Class
