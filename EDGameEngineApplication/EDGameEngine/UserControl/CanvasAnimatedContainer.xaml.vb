' The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml

Public NotInheritable Class CanvasAnimatedContainer
    Inherits UserControl
    Public Event Draw As TypedEventHandler(Of CanvasAnimatedControl, CanvasAnimatedDrawEventArgs)
    Public Event CreateResources As TypedEventHandler(Of CanvasAnimatedControl, CanvasCreateResourcesEventArgs)
    Public ReadOnly Property Canvas As CanvasAnimatedControl
        Get
            Return DirectCast(Content, CanvasAnimatedControl)
        End Get
    End Property
    Sub OnDraw(snd As CanvasAnimatedControl, ev As CanvasAnimatedDrawEventArgs)
        RaiseEvent Draw(snd, ev)
    End Sub
    Private Sub Win2DContainer_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        Canvas.RemoveFromVisualTree()
        Content = Nothing
        Debug.WriteLine("画布已从控件分离")
    End Sub
    Protected Overrides Sub Finalize()
        Debug.WriteLine("回收动态画布")
        MyBase.Finalize()
    End Sub

    Private Sub CanvasAnimatedControl_CreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        RaiseEvent CreateResources(sender, args)
    End Sub
End Class
