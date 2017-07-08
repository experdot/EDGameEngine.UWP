' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Public NotInheritable Class SampleView
    Inherits UserControl

    Private Sub SampleView_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles Me.PointerPressed
        Sample.AcitvedAsync(CType(Me.DataContext, Sample))
    End Sub
End Class
