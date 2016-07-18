Public Class MathPid
    Public SetSpeed As Single
    '定义设定值    
    Public ActualSpeed As Single
    '定义实际值   
    Public err As Single
    '定义偏差值  
    Public err_next As Single
    '定义上一个偏差值    
    Public err_last As Single
    '定义最上前的偏差值     
    Public Kp As Single, Ki As Single, Kd As Single
    '定义比例、积分、微分系数
    ''' <summary>
    ''' PID算法初始化
    ''' </summary>
    Public Sub New()
        SetSpeed = 0
        ActualSpeed = 0
        err = 0
        err_last = 0
        err_next = 0
    End Sub
    ''' <summary>
    ''' 增量PID算法实现的方法
    ''' </summary>
    ''' <param name="a"></param>
    ''' <returns></returns>返回值为最终的输出值
    Public Function PID_realize(a As Single) As Double
        '这里的参数a是设定值，即你要输出量为//多大
        Kp = 0.4F
        '比例系数
        Ki = 0.53F
        '积分系数
        Kd = 0.1F
        '微分系数
        SetSpeed = a
        err = SetSpeed - ActualSpeed
        Dim incrementSpeed As Single
        incrementSpeed = Kp * (err - err_next + Ki * err + Kd * (err - 2 * err_next + err_last))
        '这是增量PID算法的公式
        ActualSpeed = ActualSpeed + incrementSpeed
        err_last = err_next
        err_next = err
        Return ActualSpeed
    End Function
End Class
