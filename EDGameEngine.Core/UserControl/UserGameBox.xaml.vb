Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Windows.UI.Core
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
            Return AnimBox
        End Get
    End Property
    WithEvents Form As CoreWindow = Window.Current.CoreWindow

    Dim TreeDraw As Action(Of ICanvasAnimatedControl, CanvasAnimatedDrawEventArgs)
    Dim TreeUpdate As Action(Of ICanvasAnimatedControl, CanvasAnimatedUpdateEventArgs)

    Private Sub AnimBox_CreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs) Handles AnimBox.CreateResources
        World.ResourceCreator = sender
        TreeDraw = AddressOf World.Draw
        TreeUpdate = AddressOf World.Update
    End Sub
    Private Sub AnimBox_Update(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs) Handles AnimBox.Update
        TreeUpdate?.Invoke(sender, args)
    End Sub
    Private Sub AnimBox_Draw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs) Handles AnimBox.Draw
        TreeDraw?.Invoke(sender, args)
    End Sub
    Private Sub AnimBox_PointerMoved(sender As Object, e As PointerRoutedEventArgs) Handles AnimBox.PointerMoved
        Dim p = e.GetCurrentPoint(AnimBox).Position
        World?.OnPointerMove(CInt(p.X), CInt(p.Y))
    End Sub
    Private Sub AnimBox_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles AnimBox.SizeChanged
        World?.OnSizeChanged(CInt(AnimBox.ActualWidth), CInt(AnimBox.ActualHeight))
    End Sub
    Private Sub UserGameBox_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        Canvas.RemoveFromVisualTree()
        AnimBox = Nothing
        Debug.WriteLine("画布已从控件分离")
    End Sub
    Private Sub Form_KeyDown(sender As CoreWindow, args As KeyEventArgs) Handles Form.KeyDown
        World?.OnKeyDown(args.VirtualKey)
    End Sub
    Private Sub Form_KeyUp(sender As CoreWindow, args As KeyEventArgs) Handles Form.KeyUp
        World?.OnKeyUp(args.VirtualKey)
    End Sub
    Private Sub UserGameBox_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles AnimBox.PointerPressed
        Dim point As Point = e.GetCurrentPoint(AnimBox).Position
        World?.OnPointerPressed(New Numerics.Vector2(CSng(point.X), CSng(point.Y)))
    End Sub
    Private Sub UserGameBox_PointerReleased(sender As Object, e As PointerRoutedEventArgs) Handles AnimBox.PointerReleased
        Dim point As Point = e.GetCurrentPoint(AnimBox).Position
        World?.OnPointerReleased(New Numerics.Vector2(CSng(point.X), CSng(point.Y)))
    End Sub
End Class
