Public MustInherit Class GeometryBase
    Inherits GameBody
    Implements IGeometry
    Public Property Border As BorderStyle = New BorderStyle(True) Implements IGeometry.Border
    Public Property Fill As FillStyle = New FillStyle(True) Implements IGeometry.Fill
End Class
