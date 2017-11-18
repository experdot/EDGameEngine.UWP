Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 位图缓存辅助类
''' </summary>
Public Class BitmapCacheHelper
    Public Shared Function CacheImage(resourceCreator As ICanvasResourceCreator, Source As ICanvasImage, Optional dpi As Integer = 96) As CanvasBitmap
        Dim rect As Rect = Source.GetBounds(resourceCreator)
        If rect.Width < 1 Then rect.Width = 1
        If rect.Height < 1 Then rect.Height = 1
        Using cac = New CanvasRenderTarget(resourceCreator, CSng(rect.Width), CSng(rect.Height), dpi)
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.Clear(Colors.Transparent)
                ds.DrawImage(Source, CSng(-rect.Left), CSng(-rect.Top))
            End Using
            Return CanvasBitmap.CreateFromColors(resourceCreator, cac.GetPixelColors, CInt(sizepx.Width), CInt(sizepx.Height))
        End Using
    End Function
    Public Shared Function CacheEntireImage(resourceCreator As ICanvasResourceCreator, Source As ICanvasImage, Optional dpi As Integer = 96) As CanvasBitmap
        Dim rect As Rect = Source.GetBounds(resourceCreator)
        If rect.Width < 1 Then rect.Width = 1
        If rect.Height < 1 Then rect.Height = 1
        If Double.IsInfinity(rect.Left) Then rect.X = 0
        If Double.IsInfinity(rect.Top) Then rect.Y = 0
        Return CacheImageClip(resourceCreator, Source, rect, dpi)
    End Function

    Public Shared Function CacheImageClip(resourceCreator As ICanvasResourceCreator, Source As ICanvasImage, rect As Rect, Optional dpi As Integer = 96) As CanvasBitmap
        Using cac = New CanvasRenderTarget(resourceCreator, CSng(rect.Width + rect.Left), CSng(rect.Height + rect.Top), dpi)
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
