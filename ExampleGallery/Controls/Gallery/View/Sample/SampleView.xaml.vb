' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Public NotInheritable Class SampleView
    Inherits UserControl
    Public Property ViewModel As SampleViewModel

    Private Sub SampleView_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles Me.PointerPressed
        ViewModel.AcitvedAsync()
    End Sub

    Private Sub SampleView_DataContextChanged(sender As FrameworkElement, args As DataContextChangedEventArgs) Handles Me.DataContextChanged
        If Me.DataContext IsNot Nothing Then
            Me.ViewModel = SampleViewModel.CreateFromSample(CType(Me.DataContext, Sample))
        End If
    End Sub
End Class
