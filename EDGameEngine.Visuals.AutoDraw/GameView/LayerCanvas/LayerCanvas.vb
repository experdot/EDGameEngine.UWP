Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 包含多层渲染目标的画布
''' </summary>
Public Class LayerCanvas
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
    Public Function CreateLayerRender() As LayerRender
        Return New LayerRender(Me)
    End Function
    ''' <summary>
    ''' 绘制
    ''' </summary>
    Public Sub OnDraw(session As CanvasDrawingSession)
        For i = 0 To Canvas.Count - 1
            session.DrawImage(Canvas(i))
        Next
        Using shadow = New Effects.ShadowEffect With {.Source = Foreground}
            session.DrawImage(shadow)
        End Using
    End Sub
End Class

