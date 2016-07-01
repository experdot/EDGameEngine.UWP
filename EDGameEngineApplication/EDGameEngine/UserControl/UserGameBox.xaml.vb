' The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml

Public NotInheritable Class UserGameBox
    Inherits UserControl
    Public Property World As World
    Dim treedraw As Action(Of CanvasAnimatedControl, CanvasAnimatedDrawEventArgs)
    Sub Draw(sender As CanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Try
            treedraw(sender, args)
        Catch ex As Exception
            Debug.WriteLine("尝试在加载未完成的时候绘制")
        End Try
    End Sub
    Private Sub AnimBox_CreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs) Handles AnimBox.CreateResources
        args.TrackAsyncAction((Async Function() As Task
                                   Debug.WriteLine("加载图片资源")
                                   Await World.LoadAsync(sender)
                                   treedraw = AddressOf World.Draw
                               End Function).Invoke.AsAsyncAction)
    End Sub
    Private Sub AnimBox_PointerMoved(sender As Object, e As PointerRoutedEventArgs) Handles AnimBox.PointerMoved
        Dim p = e.GetCurrentPoint(AnimBox).Position
        World?.OnMouseMove(p.X, p.Y)
    End Sub

    Private Sub AnimBox_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles AnimBox.SizeChanged
        World?.OnSizeChanged(AnimBox.ActualWidth, AnimBox.ActualHeight)
    End Sub
End Class
