Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
''' <summary>
''' 游戏盒子
''' </summary>
Public NotInheritable Class UserGameBox
    Inherits UserControl
    ''' <summary>
    ''' 游戏世界
    ''' </summary>
    Public Property World As World
    ''' <summary>
    ''' 游戏画布
    ''' </summary>
    Public ReadOnly Property Canvas As CanvasAnimatedControl
        Get
            Return DirectCast(AnimBox, CanvasAnimatedControl)
        End Get
    End Property

    Dim TreeDraw As Action(Of CanvasAnimatedControl, CanvasAnimatedDrawEventArgs)
    Dim TreeUpdate As Action(Of ICanvasAnimatedControl, CanvasAnimatedUpdateEventArgs)

    Private Sub AnimBox_CreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs) Handles AnimBox.CreateResources
        World.ResourceCreator = sender
        TreeDraw = AddressOf World.Draw
        TreeUpdate = AddressOf World.Update
    End Sub
    Private Sub AnimBox_Update(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs) Handles AnimBox.Update
        'Try
        TreeUpdate(sender, args)
        'Catch ex As Exception
        'Debug.WriteLine("更新时发生错误:" & ex.Message)
        'End Try
    End Sub
    Private Sub AnimBox_Draw(sender As CanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs) Handles AnimBox.Draw
        'Try
        TreeDraw(sender, args)
        'Catch ex As Exception
        '    Debug.WriteLine("绘制时发生错误:" & ex.Message)
        'End Try
    End Sub
    Private Sub AnimBox_PointerMoved(sender As Object, e As PointerRoutedEventArgs) Handles AnimBox.PointerMoved
        Dim p = e.GetCurrentPoint(AnimBox).Position
        World?.OnMouseMove(p.X, p.Y)
    End Sub
    Private Sub AnimBox_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles AnimBox.SizeChanged
        World?.OnSizeChanged(AnimBox.ActualWidth, AnimBox.ActualHeight)
    End Sub
    Private Sub UserGameBox_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        Canvas.RemoveFromVisualTree()
        AnimBox = Nothing
        Debug.WriteLine("画布已从控件分离")
    End Sub
End Class
