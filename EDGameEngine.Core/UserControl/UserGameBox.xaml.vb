Imports EDGameEngine.Core
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

    Dim TreeDraw As Action(Of ICanvasAnimatedControl, CanvasAnimatedDrawEventArgs)
    Dim TreeUpdate As Action(Of ICanvasAnimatedControl, CanvasAnimatedUpdateEventArgs)

    Private Sub AnimBox_CreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs) Handles AnimBox.CreateResources
        World.ResourceCreator = sender
        TreeDraw = AddressOf World.Draw
        TreeUpdate = AddressOf World.Update
        Me.AddHandler(Button.KeyDownEvent, New KeyEventHandler(AddressOf AnimBox_KeyDown), True)
        Me.AddHandler(Button.KeyUpEvent, New KeyEventHandler(AddressOf AnimBox_KeyUp), True)
    End Sub
    Private Sub AnimBox_Update(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs) Handles AnimBox.Update
        TreeUpdate(sender, args)
    End Sub
    Private Sub AnimBox_Draw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs) Handles AnimBox.Draw
        TreeDraw(sender, args)
    End Sub
    Private Sub AnimBox_PointerMoved(sender As Object, e As PointerRoutedEventArgs) Handles AnimBox.PointerMoved
        Dim p = e.GetCurrentPoint(AnimBox).Position
        World?.OnMouseMove(CInt(p.X), CInt(p.Y))
    End Sub
    Private Sub AnimBox_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles AnimBox.SizeChanged
        World?.OnSizeChanged(CInt(AnimBox.ActualWidth), CInt(AnimBox.ActualHeight))
    End Sub
    Private Sub UserGameBox_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        Canvas.RemoveFromVisualTree()
        AnimBox = Nothing
        Debug.WriteLine("画布已从控件分离")
    End Sub

    Private Sub AnimBox_KeyDown(sender As Object, e As KeyRoutedEventArgs)
        World?.OnKeyDown(e.Key)
    End Sub

    Private Sub AnimBox_KeyUp(sender As Object, e As KeyRoutedEventArgs)
        World?.OnKeyUp(e.Key)
    End Sub
End Class
