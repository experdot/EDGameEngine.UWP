''' <summary>
''' 资源标识符
''' </summary>
Public Class ResourceId
    ''' <summary>
    ''' 值
    ''' </summary>
    Public Value As Integer

    Public Shared Function Create(value As Integer) As ResourceId
        Return New ResourceId With {.Value = value}
    End Function

End Class
