''' <summary>
''' AI管理器
''' </summary>
Public Class AIManager
    ''' <summary>
    ''' AI控制器集合
    ''' </summary>
    Public Property AIControllers As List(Of IAIController)
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        AIControllers = New List(Of IAIController)
    End Sub
    ''' <summary>
    ''' 开始
    ''' </summary>
    Public Sub Start(mission As IMission)
        For Each controller In AIControllers
            controller.Start(CType(mission, Mission))
        Next
    End Sub
    ''' <summary>
    ''' 更新
    ''' </summary>
    Public Sub Update(mission As IMission)
        For Each controller In AIControllers
            controller.Update(CType(mission, Mission))
        Next
    End Sub
End Class
