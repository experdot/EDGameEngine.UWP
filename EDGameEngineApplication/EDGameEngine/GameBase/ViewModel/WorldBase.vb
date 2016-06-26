Imports Microsoft.Graphics.Canvas
Imports Windows.UI
Public MustInherit Class WorldBase
    Implements IWorld, IDisposable
    Public Shared Property ImageManager As ImageResourceManager
    Public Property GameVisuals As New List(Of IGameVisualModel)
    Public Sub AddGameVisual(gV As IGameVisualModel)
        GameVisuals.Add(gV)
    End Sub
    Public Async Function LoadAsync(resourceCreator As ICanvasResourceCreator) As Task Implements IWorld.LoadAsync
        Dim resldr = New ImageResourceManager(resourceCreator)
        Await resldr.LoadAsync()
        ImageManager = resldr
    End Function
    Public Overridable Sub OnDraw(drawingSession As CanvasDrawingSession) Implements IWorld.OnDraw
        Using cmdList = New CanvasCommandList(drawingSession.Device)
            Using Dl = cmdList.CreateDrawingSession
                For Each SubGameVisual In GameVisuals
                    SubGameVisual.Presenter.OnDraw(Dl)
                Next
            End Using
            drawingSession.Clear(Colors.Black)
            drawingSession.DrawImage(cmdList)
        End Using
    End Sub
    Public Overridable Sub Update() Implements IWorld.Update
        For Each SubGameVisual In GameVisuals
            SubGameVisual.Update()
        Next
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用
    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                ImageManager?.Dispose()
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
