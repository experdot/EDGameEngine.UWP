Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Windows.UI
Imports System.Numerics
Imports EDGameEngine.Core.Utilities
''' <summary>
''' 自动绘图的视图
''' </summary>
Public Class AutoDrawView
    Inherits TypedCanvasView(Of IAutoDrawModel)
    Public Sub New(Target As IAutoDrawModel)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Static Paint As New Paint(drawingSession, Target.ImageSize, Target.LayerCount)
        Using drawing = Paint.CreateLayerDrawing()
            Dim point As New PointWithLayer
            While Target.CurrentPoints.TryDequeue(point)
                If point IsNot Nothing Then
                    If Target.CircleLayers.Contains(point.LayerIndex) Then
                        drawing.FillCircle(point)
                    Else
                        drawing.DrawLine(point)
                    End If
                    'drawing.FillCircle(point)
                End If
            End While
            Paint.OnDraw(drawingSession)
        End Using
    End Sub

    ''' <summary>
    ''' 画布
    ''' </summary>
    Private Class Paint
        ''' <summary>
        ''' 画布集
        ''' </summary>
        Public Property Canvas As CanvasRenderTarget()
        ''' <summary>
        ''' 前景
        ''' </summary>
        Public Property Foreground As CanvasRenderTarget
        ''' <summary>
        ''' 创建并初始化一个实例
        ''' </summary>
        Public Sub New(session As CanvasDrawingSession, size As Size, count As Integer)
            ReDim Canvas(count - 1)
            For i = 0 To Canvas.Count - 1
                Canvas(i) = New CanvasRenderTarget(session, size)
                Using ds = Canvas(i).CreateDrawingSession
                    ds.Clear(Colors.Transparent)
                End Using
            Next
            Foreground = New CanvasRenderTarget(session, size)
        End Sub
        ''' <summary>
        ''' 创建层绘制器
        ''' </summary>
        Public Function CreateLayerDrawing() As LayerDrawing
            Return New LayerDrawing(Me)
        End Function
        ''' <summary>
        ''' 绘制
        ''' </summary>
        Public Sub OnDraw(drawingSession As CanvasDrawingSession)
            For i = 0 To Canvas.Count - 1
                drawingSession.DrawImage(Canvas(i))
            Next
            Using shadow = New Effects.ShadowEffect With {.Source = Foreground}
                drawingSession.DrawImage(shadow)
            End Using
        End Sub
    End Class

    ''' <summary>
    ''' 层绘制器
    ''' </summary>
    Private Class LayerDrawing
        Implements IDisposable
        ''' <summary>
        ''' 绘图会话集
        ''' </summary>
        Public Property Sessions As CanvasDrawingSession()
        ''' <summary>
        ''' 创建并初始化一个实例
        ''' </summary>
        Public Sub New(paint As Paint)
            ReDim Sessions(paint.Canvas.Count)
            For i = 0 To Sessions.Count - 2
                Sessions(i) = paint.Canvas(i).CreateDrawingSession
            Next
            Sessions(Sessions.Count - 1) = paint.Foreground.CreateDrawingSession
            Sessions(Sessions.Count - 1).Clear(Colors.Transparent)
        End Sub
        ''' <summary>
        ''' 画圆
        ''' </summary>
        Public Sub FillCircle(point As PointWithLayer)
            Sessions(point.LayerIndex).FillCircle(point.Position, point.UserSize, point.UserColor)
            Sessions.Last.FillCircle(point.Position, point.UserSize, Colors.Black)
        End Sub
        ''' <summary>
        ''' 画线
        ''' </summary>
        Public Sub DrawLine(point As PointWithLayer)
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
        ''' <summary>
        ''' 画曲线
        ''' </summary>
        Public Sub DrawCurve(point As PointWithLayer)
            Sessions(point.LayerIndex).DrawLine(point.Position, point.NextPoint.Position, point.UserColor, 1.0F)
            Sessions.Last.DrawLine(point.Position, point.NextPoint.Position, point.UserColor, 1.0F)
            'Sessions(point.LayerIndex).DrawLine(point.Position, point.NextPoint.Position, point.UserColor, point.UserSize * 2)
            'Sessions.Last.DrawLine(point.Position, point.NextPoint.Position, Colors.Black, point.UserSize * 2)
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
End Class
