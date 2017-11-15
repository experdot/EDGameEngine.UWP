Imports System.Collections.Concurrent
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Microsoft.Graphics.Canvas

Public Interface IAutoDrawModel
    Inherits IGameVisual
    Property CircleLayers As Integer()
    Property CurrentPoints As ConcurrentQueue(Of VertexWithLayer)
    Property Image As CanvasBitmap
    Property ImageSize As Size
    Property LayerCount As Integer
    Property PointsCount As Integer
    Property PointsCountMax As Integer
End Interface
