Imports Microsoft.Graphics.Canvas
''' <summary>
''' 表示一个游戏数据模型对应的视图
''' </summary>
Public Interface IGameView
    Sub BeginDraw(DrawingSession As CanvasDrawingSession)
    Sub OnDraw(DrawingSession As CanvasDrawingSession)
End Interface
