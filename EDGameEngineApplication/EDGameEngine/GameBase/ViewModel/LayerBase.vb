Imports System.Numerics
Imports EDGameEngine
Imports Microsoft.Graphics.Canvas

Public MustInherit Class LayerBase
    Implements ILayer
    Public Overridable Property Location As Vector2 = Vector2.Zero Implements IObjectStatus.Location
    Public Overridable Property Scale As Vector2 = Vector2.One Implements IObjectStatus.Scale
    Public Overridable Property Rotation As Single = 0.0F Implements IObjectStatus.Rotation
    Public Overridable Property Visible As Boolean = True Implements IObjectStatus.Visible
    Public Overridable Property Enabled As Boolean = True Implements IObjectStatus.Enabled
    Public Overridable Property Opacity As Boolean = 1.0F Implements IObjectStatus.Opacity

    Public Property GameVisuals As New List(Of IGameVisualModel) Implements ILayer.GameVisuals
    Public MustOverride Sub OnDraw(drawingSession As CanvasDrawingSession) Implements ILayer.OnDraw
End Class
