Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Windows.System
Imports Windows.UI
''' <summary>
''' 表示一个可以初始化、更新可视化对象空间
''' </summary>
Public MustInherit Class World
    Implements IDisposable
    Public Property ResourceCreator As ICanvasResourceCreator
    Public Property Width As Integer
    Public Property Height As Integer
    Public Property CurrentScene As IScene
    Public Sub New(ActualWidth#, ActualHeight#)
        OnSizeChanged(CInt(ActualWidth), CInt(ActualHeight))
        CreateScene()
    End Sub
    Public MustOverride Sub CreateScene()
    Public Sub Update(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)
        CurrentScene.Update()
    End Sub
    Public Sub Draw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        CurrentScene.OnDraw(args.DrawingSession)
    End Sub
    Public Sub OnMouseMove(mX As Integer, mY As Integer)
        CurrentScene.Inputs.Mouse.Location = New Vector2(mX, mY)
    End Sub
    Public Sub OnSizeChanged(sX As Integer, sY As Integer)
        Width = sX
        Height = sY
        If CurrentScene IsNot Nothing Then
            CurrentScene.Width = sX
            CurrentScene.Height = sY
        End If
    End Sub
    Public Sub OnKeyDown(keycode As VirtualKey)
        CurrentScene?.Inputs.Keyboard.RaiseKeyDown(keycode)
    End Sub
    Public Sub OnKeyUp(keycode As VirtualKey)
        CurrentScene?.Inputs.Keyboard.RaiseKeyUp(keycode)
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                CurrentScene?.Dispose()
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
