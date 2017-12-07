''' <summary>
''' AI管理器
''' </summary>
Public Class AIManager
    ''' <summary>
    ''' AI控制器集合
    ''' </summary>
    Public Property AIConrollers As List(Of IAIController)
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        AIConrollers = New List(Of IAIController)
    End Sub
    ''' <summary>
    ''' 开始
    ''' </summary>
    Public Sub Start(mission As IMission)
        For Each SubAIConroller In AIConrollers
            SubAIConroller.Start(CType(mission, Mission))
        Next
    End Sub
    ''' <summary>
    ''' 更新
    ''' </summary>
    Public Sub Update(mission As IMission)
        For Each SubAIConroller In AIConrollers
            SubAIConroller.Update(CType(mission, Mission))
        Next
    End Sub
End Class
