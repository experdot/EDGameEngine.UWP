Imports System.Numerics

Public Module Vector2Extensions
    ''' <summary>
    ''' 设置模长的上限
    ''' </summary>
    <Extension>
    Public Sub LimitMag(ByRef this As Vector2, ByVal upon As Single)
        Dim tempMag As Single = this.Length
        If tempMag > upon Then
            this = New Vector2(this.X * (upon / tempMag), this.Y * (upon / tempMag))
        End If
    End Sub
    ''' <summary>
    ''' 设置模长
    ''' </summary>
    <Extension>
    Public Sub SetMag(ByRef this As Vector2, ByVal magtitude As Single)
        Dim length As Single = this.Length
        If length > 0 Then
            this = New Vector2(this.X * (magtitude / length), this.Y * (magtitude / length))
        End If
    End Sub
    ''' <summary>
    ''' 旋转指定的弧度
    ''' </summary>
    <Extension>
    Public Sub Rotate(ByRef this As Vector2, ByVal rotation As Single)
        Dim x1, y1 As Single
        x1 = this.X * CSng(Math.Cos(rotation)) - this.Y * CSng(Math.Sin(rotation))
        y1 = this.Y * CSng(Math.Cos(rotation)) + this.X * CSng(Math.Sin(rotation))
        this.X = x1
        this.Y = y1
    End Sub
    ''' <summary>
    ''' 返回当前旋转指定弧度后的新向量
    ''' </summary>
    <Extension>
    Public Function RotateNew(ByRef this As Vector2, ByVal rotation As Single) As Vector2
        Dim x1, y1 As Single
        x1 = this.X * CSng(Math.Cos(rotation)) - this.Y * CSng(Math.Sin(rotation))
        y1 = this.Y * CSng(Math.Cos(rotation)) + this.X * CSng(Math.Sin(rotation))
        Return New Vector2(x1, y1)
    End Function
End Module
