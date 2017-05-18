﻿Imports Microsoft.Graphics.Canvas
''' <summary>
''' 表示一个游戏数据模型对应的视图
''' </summary>
Public Interface IGameView
    ''' <summary>
    ''' 位图缓存
    ''' </summary>
    Property Cache As CanvasBitmap
    ''' <summary>
    ''' 是否允许位图缓存
    ''' </summary>
    Property CacheAllowed As Boolean
    ''' <summary>
    ''' 绘图命令
    ''' </summary>
    Property CommandList As CanvasCommandList
    ''' <summary>
    ''' 预绘制
    ''' </summary>
    Sub BeginDraw(drawingSession As CanvasDrawingSession)
    ''' <summary>
    ''' 绘制
    ''' </summary>
    Sub OnDraw(drawingSession As CanvasDrawingSession)
End Interface
