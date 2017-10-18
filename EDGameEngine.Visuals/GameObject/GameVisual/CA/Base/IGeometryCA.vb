Imports Microsoft.Graphics.Canvas.Geometry
''' <summary>
''' 多边形网格元胞自动机
''' </summary>
Public Interface IGeometryCA
    Inherits ICellularAutomata
    ''' <summary>
    ''' 几何体
    ''' </summary>
    Property Geometry As CanvasGeometry
End Interface
