Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class BitmapCacheHelper
    Public Shared Function CacheImage(DrawingSession As CanvasDrawingSession, Source As ICanvasImage) As CanvasBitmap
        Dim reg = Source.GetBounds(DrawingSession)
        Using cac = New CanvasRenderTarget(DrawingSession, reg.Width, reg.Height)
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.Clear(Colors.Transparent)
                ds.DrawImage(Source, -reg.Left, -reg.Top)
            End Using
            Return CanvasBitmap.CreateFromColors(DrawingSession, cac.GetPixelColors, sizepx.Width, sizepx.Height)
        End Using
    End Function
    Public Shared Function CacheEntireImage(DrawingSession As CanvasDrawingSession, Source As ICanvasImage) As CanvasBitmap
        Dim reg = Source.GetBounds(DrawingSession)
        Return CacheImageClip(DrawingSession, Source, reg)
    End Function

    Public Shared Function CacheImageClip(DrawingSession As CanvasDrawingSession, Source As ICanvasImage, reg As Rect) As CanvasBitmap
        Using cac = New CanvasRenderTarget(DrawingSession, reg.Width + reg.Left, reg.Height + reg.Top)
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.Clear(Colors.Transparent)
                ds.DrawImage(Source)
            End Using
            Return CanvasBitmap.CreateFromColors(DrawingSession, cac.GetPixelColors, sizepx.Width, sizepx.Height)
        End Using
    End Function

    Public Shared Sub SaveAsPng(cac As CanvasRenderTarget)
        Dim f = Task.Run(Async Function()
                             Dim fi = Task.Run(Async Function()
                                                   Return Await (Await Windows.Storage.KnownFolders.SavedPictures.CreateFileAsync($"cache{Date.Now.ToBinary.ToString("X")}.png")).OpenAsync(Windows.Storage.FileAccessMode.ReadWrite)
                                               End Function)
                             fi.Wait()
                             Await cac.SaveAsync(fi.Result, CanvasBitmapFileFormat.Png)
                         End Function)
        f.Wait()
    End Sub
End Class
