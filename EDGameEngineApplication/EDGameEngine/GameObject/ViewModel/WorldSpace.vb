Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 可视化对象空间
''' </summary>
Public Class WorldSpace
    Implements IDisposable
    Public Shared SpaceWidth As Single = 100
    Public Shared SpaceHeight As Single = 100
    Public Shared ImageManager As ImageResourceManager
    Public GameVisuals As New List(Of IGameVisualModel)
    Public Sub New(WindowSize As Size)
        SpaceWidth = WindowSize.Width
        SpaceHeight = WindowSize.Height
    End Sub
    Public Async Function LoadAsync(ResourceCreator As ICanvasResourceCreator) As Task
        Dim resldr = New ImageResourceManager(ResourceCreator)
        Await resldr.LoadAsync()
        ImageManager = resldr
    End Function
    Public Sub AddGameVisual(gV As IGameVisualModel)
        GameVisuals.Add(gV)
    End Sub
    Public Sub DrawSpace(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession.Device)
            Using Dl = cmdList.CreateDrawingSession
                For Each SubGameVisual In GameVisuals
                    SubGameVisual.Presenter.Display(Dl)
                Next
            End Using
            DrawingSession.Clear(Colors.Black)
            DrawingSession.DrawImage(cmdList)
        End Using
    End Sub
    Public Sub Update()
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
