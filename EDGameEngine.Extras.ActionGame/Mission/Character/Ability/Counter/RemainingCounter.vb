''' <summary>
''' 表示角色数值、物品或技能的可用量计数器
''' </summary>
Public Class RemainingCounter
    ''' <summary>
    ''' 计数器失效时发生的事件
    ''' </summary>
    Public Event Expired(sender As Object, e As ExpiredEventArgs)
    ''' <summary>
    ''' 可用的数量计数,若无效表示数量无限
    ''' </summary>
    Public Quantity As AvaliableValue(Of Integer)
    ''' <summary>
    ''' 可用的时间计数,若无效表示时间永久
    ''' </summary>
    Public Time As AvaliableValue(Of TimeSpan)
    ''' <summary>
    ''' 返回当前计数是否有效,有效表示可用
    ''' </summary>
    Public ReadOnly Property Availiable As Boolean
        Get
            Return (Not Quantity.Enabled OrElse Quantity.Value > 0) AndAlso
                   (Not Time.Enabled OrElse Time.Value.TotalMilliseconds > 0)
        End Get
    End Property
    ''' <summary>
    ''' 改变计数
    ''' </summary>
    Public Sub Change(type As CounterTypes, count As Integer)
        If type = CounterTypes.Count Then
            If Quantity.Enabled Then
                Quantity.Value += count
                If Quantity.Value <= 0 Then
                    RaiseEvent Expired(Me, New ExpiredEventArgs(CounterTypes.Count))
                End If
            End If
        ElseIf type = CounterTypes.Time Then
            If Time.Enabled Then
                Time.Value = TimeSpan.FromMilliseconds(Time.Value.TotalMilliseconds + count)
                If Time.Value.TotalMilliseconds <= 0 Then
                    RaiseEvent Expired(Me, New ExpiredEventArgs(CounterTypes.Time))
                End If
            End If
        End If
    End Sub
End Class
