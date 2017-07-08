Imports Windows.UI.Popups
''' <summary>
''' 示例
''' </summary>
Public Class Sample
    ''' <summary>
    ''' 标题
    ''' </summary>
    Public Property Title As String
    ''' <summary>
    ''' 描述
    ''' </summary>
    Public Property Description As String
    ''' <summary>
    ''' 图像
    ''' </summary>
    Public Property Image As BitmapImage
    ''' <summary>
    ''' 示例编号
    ''' </summary>
    Public Property Id As Integer

    ''' <summary>
    ''' 激活指定的示例
    ''' </summary>
    Public Shared Async Sub AcitvedAsync(target As Sample)
        If target.Id = -1 Then
            Await New MessageDialog($"示例{target.Id}激活,这个示例是未实现的").ShowAsync()
        Else
            GameStarter.Start(target.Title)
        End If
    End Sub

End Class
