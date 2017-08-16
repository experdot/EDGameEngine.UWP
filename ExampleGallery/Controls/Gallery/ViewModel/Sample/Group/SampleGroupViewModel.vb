Public Class SampleGroupViewModel
    ''' <summary>
    ''' 标题
    ''' </summary>
    Public Property Title As String
    ''' <summary>
    ''' 示例集合
    ''' </summary>
    Public Property Sameples As List(Of Sample)

    Public Property Source As SampleGroup

    Public Shared Function CreateFromSampleGroup(group As SampleGroup) As SampleGroupViewModel
        Return New SampleGroupViewModel() With {
            .Title = group.Title,
            .Sameples = group.Sameples,
            .Source = group
        }
    End Function
End Class
