Imports Windows.UI.Popups

Public Class SampleViewModel

    Public Property Title As String
    Public Property Description As String
    Public Property Image As BitmapImage

    Public Property Source As Sample

    ''' <summary>
    ''' 激活指定的示例
    ''' </summary>
    Public Async Sub AcitvedAsync()
        If Source.Id = -1 Then
            Await New MessageDialog($"示例{Source.Id}激活,这个示例是未实现的").ShowAsync()
        Else
            GameStarter.Start(Source.Id, Source.Title)
        End If
    End Sub

    Public Shared Function CreateFromSample(sample As Sample) As SampleViewModel
        If sample Is Nothing Then Return Nothing
        Return New SampleViewModel With {
            .Description = sample.Description,
            .Image = sample.Image,
            .Title = sample.Title,
            .Source = sample
            }
    End Function
End Class
