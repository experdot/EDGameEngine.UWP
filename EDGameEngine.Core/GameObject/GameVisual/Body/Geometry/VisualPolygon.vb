Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Geometry
''' <summary>
''' 基础几何图元，多边形
''' </summary>
Public Class VisualPolygon
    Inherits GeometryBase
    Public Property Geometry As CanvasGeometry
    ''' <summary>
    ''' 由指定的边数创建正多边形
    ''' </summary>
    ''' <param name="count"></param>
    Public Sub New(resourceCreator As ICanvasResourceCreator, count As Integer, r As Single)
        Dim points As New List(Of Vector2)
        Dim vec As New Vector2(0, -r)
        Dim rotate As Single = CSng(Math.PI / count * 2)
        points.Add(vec)
        For i = 1 To count - 1
            points.Add(vec.RotateNew(rotate * i))
        Next
        Geometry = CanvasGeometry.CreatePolygon(resourceCreator, points.ToArray)
    End Sub
    ''' <summary>
    ''' 由指定的顶点集合创建多边形
    ''' </summary>
    Public Sub New(resourceCreator As ICanvasResourceCreator, points As Vector2())
        Geometry = CanvasGeometry.CreatePolygon(resourceCreator, points)
    End Sub
    Public Overrides Sub StartEx()

    End Sub

    Public Overrides Sub UpdateEx()

    End Sub
End Class
