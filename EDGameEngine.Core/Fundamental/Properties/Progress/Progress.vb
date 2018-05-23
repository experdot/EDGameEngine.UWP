''' <summary>
''' 进度
''' </summary>
Public Class Progress
    ''' <summary>
    ''' 进度值
    ''' </summary>
    Public Property Value As Single = 0.0F
    ''' <summary>
    ''' 进度描述
    ''' </summary>
    Public Property Description As String
    ''' <summary>
    ''' 进度百分比
    ''' </summary>
    Public ReadOnly Property Percent As String
        Get
            Return $"{Value * 100}%"
        End Get
    End Property
    ''' <summary>
    ''' 创建并初始一个实例
    ''' </summary>
    Public Sub New(value As Single, description As String)
        Me.Value = value
        Me.Description = description
    End Sub
End Class
