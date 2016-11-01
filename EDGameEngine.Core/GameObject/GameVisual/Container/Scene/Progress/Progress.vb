''' <summary>
''' 进度
''' </summary>
Public Class Progress
    ''' <summary>
    ''' 进度值
    ''' </summary>
    Public Value As Single = 0.0F
    ''' <summary>
    ''' 进度描述
    ''' </summary>
    Public Property Description As String
    Public ReadOnly Property Percent As String
        Get
            Return (Value * 100) & "%"
        End Get
    End Property

    Public Sub New(val As Single, desc As String)
        Me.Value = val
        Me.Description = desc
    End Sub
End Class
