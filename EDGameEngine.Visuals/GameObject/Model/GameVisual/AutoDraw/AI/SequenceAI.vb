Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 表示自动循迹并生成绘制序列的AI
''' </summary>
Public Class SequenceAI
    Implements IDisposable
    ''' <summary>
    ''' 线条集
    ''' </summary>
    Public Property Lines As List(Of Line)
    ''' <summary>
    ''' 扫描方式
    ''' </summary>
    Public Property ScanMode As ScanMode = ScanMode.Rect
    Dim xArray() As Integer = {-1, 0, 1, 1, 1, 0, -1, -1}
    Dim yArray() As Integer = {-1, -1, -1, 0, 1, 1, 1, 0}
    Dim NewStart As Boolean
    ''' <summary>
    ''' 创建并初始化一个可自动生成绘制序列AI的实例
    ''' </summary>
    Public Sub New(BolArr(,) As Integer, Optional mode As ScanMode = ScanMode.Rect)
        Me.ScanMode = mode
        Lines = New List(Of Line)
        CalculateSequence(BolArr)
    End Sub

    ''' <summary>
    ''' 新增一个序列
    ''' </summary>
    Private Sub CreateNewSequence()
        Lines.Add(New Line)
    End Sub
    ''' <summary>
    ''' 在序列List末尾项新增一个点
    ''' </summary>
    Private Sub AddPoint(p As Vector2)
        Lines.Last.Points.Add(New PointWithLayer() With {.Position = p, .Size = 1})
    End Sub
    ''' <summary>
    ''' 计算序列
    ''' </summary>
    Private Sub CalculateSequence(BolArr(,) As Integer)
        If ScanMode = ScanMode.Rect Then
            ScanRect(BolArr)
        Else
            ScanCircle(BolArr)
        End If
    End Sub
    ''' <summary>
    ''' 圆形扫描
    ''' </summary>
    Private Sub ScanCircle(BolArr(,) As Integer)
        Dim xlength As Integer = BolArr.GetLength(0)
        Dim ylength As Integer = BolArr.GetLength(1)
        Dim center As New Vector2(CSng(xlength / 2), CSng(ylength / 2))
        Dim radius As Integer = 0
        For radius = 0 To CInt(Math.Sqrt(xlength * xlength + ylength * ylength))
            For Theat = 0 To Math.PI * 2 Step 1 / radius
                Dim dx As Integer = CInt(center.X + radius * Math.Cos(Theat))
                Dim dy As Integer = CInt(center.Y + radius * Math.Sin(Theat))
                If Not (dx >= 0 AndAlso dy >= 0 AndAlso dx < xlength AndAlso dy < ylength) Then Continue For
                If BolArr(dx, dy) = 1 Then
                    BolArr(dx, dy) = 0
                    Me.CreateNewSequence()
                    Me.AddPoint(New Vector2(dx, dy))
                    CheckMove(BolArr, dx, dy, 0)
                    NewStart = True
                End If
            Next
        Next
    End Sub
    ''' <summary>
    ''' 矩形扫描
    ''' </summary>
    ''' <param name="BolArr"></param>
    Private Sub ScanRect(BolArr(,) As Integer)
        Dim xCount As Integer = BolArr.GetUpperBound(0)
        Dim yCount As Integer = BolArr.GetUpperBound(1)
        For i = 0 To xCount - 1
            For j = 0 To yCount - 1
                Dim dx As Integer = i
                Dim dy As Integer = j
                If Not (dx > 0 And dy > 0 And dx < xCount And dy < yCount) Then Continue For
                If BolArr(dx, dy) = 1 Then
                    BolArr(dx, dy) = 0
                    Me.CreateNewSequence()
                    Me.AddPoint(New Vector2(dx, dy))
                    CheckMove(BolArr, dx, dy, 0)
                    NewStart = True
                End If
            Next
        Next
    End Sub
    ''' <summary>
    ''' 递归循迹算法
    ''' </summary>
    Private Sub CheckMove(ByRef bolArr(,) As Integer, ByVal x As Integer, ByVal y As Integer, ByVal StepNum As Integer)
        If StepNum > 1000 Then Return
        Dim xBound As Integer = bolArr.GetUpperBound(0)
        Dim yBound As Integer = bolArr.GetUpperBound(1)
        Dim dx, dy As Integer
        Dim AroundValue As Integer = GetAroundValue(bolArr, x, y)
        '根据点权值轨迹将在当前点断开
        'If AroundValue > 2 AndAlso AroundValue < 8 Then
        'Return
        'End If
        For i = 0 To 7
            dx = x + xArray(i)
            dy = y + yArray(i)
            If Not (dx > 0 And dy > 0 And dx < xBound And dy < yBound) Then
                Return
            ElseIf bolArr(dx, dy) = 1 Then
                bolArr(dx, dy) = 0
                If NewStart = True Then
                    Me.CreateNewSequence()
                    Me.AddPoint(New Vector2(dx, dy))
                    NewStart = False
                Else
                    Me.AddPoint(New Vector2(dx, dy))
                End If
                CheckMove(bolArr, dx, dy, StepNum + 1)
                NewStart = True
            End If
        Next
    End Sub
    ''' <summary>
    ''' 返回点权值
    ''' </summary>
    Private Function GetAroundValue(ByRef BolArr(,) As Integer, ByVal x As Integer, ByVal y As Integer) As Integer
        Dim dx, dy, ResultValue As Integer
        Dim xBound As Integer = BolArr.GetUpperBound(0)
        Dim yBound As Integer = BolArr.GetUpperBound(1)
        For i = 0 To 7
            dx = x + xArray(i)
            dy = y + yArray(i)
            If dx > 0 And dy > 0 And dx < xBound And dy < yBound Then
                If BolArr(dx, dy) = 1 Then
                    ResultValue += 1
                End If
            End If
        Next
        Return ResultValue
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                Lines.Clear()
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
