Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Public Interface ILayer
    Inherits IObjectStatus
    Property GameVisuals As List(Of IGameVisualModel)
    Sub OnDraw(drawingSession As CanvasDrawingSession)
End Interface
