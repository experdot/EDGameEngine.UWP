Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Geometry
''' <summary>
''' 几何体帮助类
''' </summary>
Public Class GeometryHelper
    ''' <summary>
    ''' 由指定的边数、半径和旋转角度创建正多边形
    ''' </summary>
    Public Shared Function CreateRegularPolygon(resourceCreator As ICanvasResourceCreator, count As Integer, radius As Single, Optional rotation As Single = 0) As CanvasGeometry
        Dim points As New List(Of Vector2)
        Dim vec As New Vector2(0, -radius)
        vec.Rotate(rotation)
        Dim rotate As Single = CSng(Math.PI / count * 2)
        points.Add(vec)
        For i = 1 To count - 1
            points.Add(vec.RotateNew(rotate * i))
        Next
        Return CanvasGeometry.CreatePolygon(resourceCreator, points.ToArray)
    End Function

    ''' <summary>
    ''' 由指定的顶点集合创建多边形
    ''' </summary>
    Public Shared Function CreatePolygon(resourceCreator As ICanvasResourceCreator, points As Vector2()) As CanvasGeometry
        Return CanvasGeometry.CreatePolygon(resourceCreator, points)
    End Function
End Class
