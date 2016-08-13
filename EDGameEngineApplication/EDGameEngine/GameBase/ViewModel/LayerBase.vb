Imports System.Numerics
Imports EDGameEngine
Imports Microsoft.Graphics.Canvas

Public MustInherit Class LayerBase
    Implements ILayer
    Public Overridable Property Appearance As Appearance = Appearance.Normal Implements IObjectStatus.Appearance
    Public Overridable Property Transform As Transform = Transform.Normal Implements IObjectStatus.Transform
    Public Overridable Property GameVisuals As New List(Of IGameVisualModel) Implements ILayer.GameVisuals
    Public Overridable Property Scene As Scene
    Public MustOverride Sub OnDraw(drawingSession As CanvasDrawingSession) Implements ILayer.OnDraw
End Class
