' The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml

Public NotInheritable Class UserGameBox
    Inherits UserControl
    Public Property Space As WorldManager
    Private Sub UserGameBox_PointerMoved(sender As Object, e As PointerRoutedEventArgs) Handles Me.PointerMoved
        Dim pos = e.GetCurrentPoint(Me).Position
        If Space IsNot Nothing Then
            Space.MouseX = pos.X
            Space.MouseY = pos.Y
        End If
    End Sub
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
                                   Await Space.LoadAsync(sender)
                                   treedraw = AddressOf Space.Draw
                               End Function).Invoke.AsAsyncAction)
    End Sub
End Class
