Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Text
Imports Windows.UI

Public MustInherit Class Scene
    Implements IScene
    Public Property ImageManager As ImageResourceManager Implements IScene.ImageManager
    Public Property GameLayers As New List(Of ILayer) Implements IScene.GameLayers
    Public Property GameVisuals As New List(Of IGameVisual) Implements IScene.GameVisuals
    Public Property Width As Single Implements IScene.Width
    Public Property Height As Single Implements IScene.Height
    Public Property World As World Implements IScene.World
    Public Property Camera As ICamera Implements IScene.Camera
    Public Property Inputs As Inputs Implements IScene.Inputs

    Dim TreeDraw As Action(Of CanvasDrawingSession)
    Dim TreeUpdate As Action
    Public Sub New(world As World, WindowSize As Size)
        Me.World = world
        Me.Inputs = New Inputs
        Me.Camera = New Camera With {.Scene = Me}
        Width = CSng(WindowSize.Width)
        Height = CSng(WindowSize.Height)
        Load()
    End Sub
    Public Async Sub Load()
        TreeDraw = New Action(Of CanvasDrawingSession)(Sub(ds As CanvasDrawingSession)
                                                           LoadingDraw(ds)
                                                       End Sub)
        TreeUpdate = New Action(Sub()
                                End Sub)
        While World.ResourceCreator Is Nothing
            Await Task.Delay(10)
        End While
        Await LoadAsync(World.ResourceCreator)
        Await Task.Run(New Action(Sub()
                                      CreateObject()
                                  End Sub))
        For Each SubGameVisual In GameVisuals
            SubGameVisual.Start()
        Next
        Camera.Start()

        TreeDraw = New Action(Of CanvasDrawingSession)(Sub(ds As CanvasDrawingSession)
                                                           LoadedDraw(ds)
                                                       End Sub)
        TreeUpdate = New Action(Sub()
                                    For Each SubGameVisual In GameVisuals
                                        SubGameVisual.Update()
                                    Next
                                    Camera.Update()
                                End Sub)
    End Sub
    Public Sub AddGameVisual(model As IGameVisual, view As IGameView, Optional LayerIndex As Integer = 0) Implements IScene.AddGameVisual
        model.Scene = Me
        model.Presenter = view
        While (GameLayers.Count <= LayerIndex)
            GameLayers.Add(New Layer With {.Scene = Me})
        End While
        GameVisuals.Add(model)
        GameLayers(LayerIndex).GameVisuals.Add(model)
    End Sub
    Public Async Function LoadAsync(resourceCreator As ICanvasResourceCreator) As Task Implements IScene.LoadAsync
        Dim resldr = New ImageResourceManager(resourceCreator)
        Await resldr.LoadAsync()
        ImageManager = resldr
    End Function
    Public Sub OnDraw(drawingSession As CanvasDrawingSession) Implements IScene.OnDraw
        TreeDraw(drawingSession)
    End Sub
    Public Sub Update() Implements IScene.Update
        TreeUpdate()
    End Sub
    Public Overridable Sub LoadingDraw(drawingSession As CanvasDrawingSession)
        drawingSession.DrawText("场景加载中，请稍后...", New Vector2(Width, Height) / 2, Colors.Black, TextFormat.Center)
    End Sub
    Public Overridable Sub LoadedDraw(drawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(drawingSession)
            Using dl = cmdList.CreateDrawingSession
                For Each SubLayer In GameLayers
                    SubLayer.OnDraw(dl)
                Next
            End Using
            drawingSession.DrawImage(cmdList, Camera.Position)
        End Using
    End Sub
    Public MustOverride Sub CreateObject() Implements IScene.CreateObject
#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用
    Public Event MouseMove() Implements IScene.MouseMove
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
    Public Sub Dispose() Implements IScene.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
