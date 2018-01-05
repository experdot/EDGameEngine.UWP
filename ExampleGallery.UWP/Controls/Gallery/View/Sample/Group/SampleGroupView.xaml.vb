' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Public NotInheritable Class SampleGroupView
    Inherits UserControl
    Public Property ViewModel As SampleGroupViewModel

    Private Sub SampleGroupView_DataContextChanged(sender As FrameworkElement, args As DataContextChangedEventArgs) Handles Me.DataContextChanged
        Me.ViewModel = CType(Me.DataContext, SampleGroupViewModel)
    End Sub
End Class
