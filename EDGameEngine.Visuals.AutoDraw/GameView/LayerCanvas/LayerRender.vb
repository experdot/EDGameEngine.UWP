
Imports System.Numerics
Imports EDGameEngine.Core.Utilities
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 支持多层渲染目标的画布的渲染器
''' </summary>
Public Class LayerRender
    Implements IDisposable
    ''' <summary>
    ''' 绘图会话集
    ''' </summary>
    Public Property Sessions As CanvasDrawingSession()
    ''' <summary>
    ''' 前景层会话
    ''' </summary>
    Public Property ForegroundSession As CanvasDrawingSession

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(paint As LayerCanvas)
        ReDim Sessions(paint.Canvas.Count - 1)
        For i = 0 To Sessions.Count - 1
            Sessions(i) = paint.Canvas(i).CreateDrawingSession
        Next

        ForegroundSession = paint.Foreground.CreateDrawingSession
        ForegroundSession.Clear(Colors.Transparent)
    End Sub
    ''' <summary>
    ''' 画圆
    ''' </summary>
    Public Sub FillCircle(point As VertexWithLayer)
        Sessions(point.LayerIndex).FillCircle(point.Position, point.UserSize, point.UserColor)
        ForegroundSession.FillCircle(point.Position, point.UserSize, Colors.Black)
    End Sub
    ''' <summary>
    ''' 画连线
    ''' </summary>
    Public Sub DrawLine(point As VertexWithLayer)
        If point IsNot Nothing AndAlso point.NextPoint IsNot Nothing Then
            Sessions(point.LayerIndex).DrawLine(point.Position, point.NextPoint.Position, point.UserColor, 1.0F)
            ForegroundSession.DrawLine(point.Position, point.NextPoint.Position, point.UserColor, 1.0F)
            'Sessions(point.LayerIndex).DrawLine(point.Position, point.NextPoint.Position, point.UserColor, point.UserSize * 2)
            'Sessions.Last.DrawLine(point.Position, point.NextPoint.Position, Colors.Black, point.UserSize * 2)
        End If
    End Sub
    ''' <summary>
    ''' 画随机方向的线条
    ''' </summary>
    Public Sub DrawRandomLine(point As VertexWithLayer)
        Static Rnd As New Random
        Dim offset As Vector2 = If(Rnd.NextDouble > 0.5F, New Vector2(-1, 1), New Vector2(1, 1))
        offset *= (RandomHelper.NextNorm(100, 400) / 100.0F) * CSng(12 - point.LayerIndex) / 2
        offset.Rotate(RandomHelper.NextNorm(-1000, 1000) / 300.0F)
        'Dim col = Color.FromArgb(CByte(20 - point.LayerIndex * 2), 0, 0, 0)
        'Sessions(point.LayerIndex).DrawLine(point.Position, point.Center, point.Color, 1)
        Sessions(point.LayerIndex).DrawLine(point.Position, point.Position + offset, point.UserColor, 1)
        Sessions(point.LayerIndex).DrawLine(point.Position, point.Position - offset, point.UserColor, 1)
        'Sessions.Last.DrawLine(point.Position, point.Center, Colors.Black, 1.0F)
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                For i = 0 To Sessions.Count - 1
                    Sessions(i).Dispose()
                Next
            End If

            ForegroundSession.Dispose()

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
