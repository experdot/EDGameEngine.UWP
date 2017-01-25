Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Microsoft.Graphics.Canvas.Geometry
''' <summary>
''' 多边形网格元胞自动机基类
''' </summary>
Public MustInherit Class GeometyCABase
    Inherits GameBody
    Implements IGeometryCA

    Public Property Width As Integer
    Public Property Height As Integer

    Public Property Cells As ICell(,) Implements ICellularAutomata.Cells
    Public Property Geometry As CanvasGeometry Implements IGeometryCA.Geometry

End Class
