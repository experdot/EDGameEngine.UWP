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
    ''' <summary>
    ''' 进度百分比
    ''' </summary>
    Public ReadOnly Property Percent As String
        Get
            Return (Value * 100) & "%"
        End Get
    End Property
    ''' <summary>
    ''' 创建并初始一个实例
    ''' </summary>
    Public Sub New(val As Single, desc As String)
        Me.Value = val
        Me.Description = desc
    End Sub
End Class
