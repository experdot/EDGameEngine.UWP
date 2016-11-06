Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class BitmapCacheHelper
    Public Shared Function CacheImage(drawingSession As CanvasDrawingSession, Source As ICanvasImage) As CanvasBitmap
        Dim reg = Source.GetBounds(DrawingSession)
        Using cac = New CanvasRenderTarget(DrawingSession, CSng(reg.Width), CSng(reg.Height))
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.Clear(Colors.Transparent)
                ds.DrawImage(Source, CSng(-reg.Left), CSng(-reg.Top))
            End Using
            Return CanvasBitmap.CreateFromColors(DrawingSession, cac.GetPixelColors, CInt(sizepx.Width), CInt(sizepx.Height))
        End Using
    End Function
    Public Shared Function CacheEntireImage(drawingSession As CanvasDrawingSession, Source As ICanvasImage) As CanvasBitmap
        Dim reg = Source.GetBounds(DrawingSession)
        Return CacheImageClip(DrawingSession, Source, reg)
    End Function

    Public Shared Function CacheImageClip(drawingSession As CanvasDrawingSession, Source As ICanvasImage, reg As Rect) As CanvasBitmap
        Using cac = New CanvasRenderTarget(DrawingSession, CSng(reg.Width + reg.Left), CSng(reg.Height + reg.Top))
            Dim sizepx = cac.SizeInPixels
            Using ds = cac.CreateDrawingSession
                ds.Clear(Colors.Transparent)
                ds.DrawImage(Source)
            End Using
            Return CanvasBitmap.CreateFromColors(DrawingSession, cac.GetPixelColors, CInt(sizepx.Width), CInt(sizepx.Height))
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
