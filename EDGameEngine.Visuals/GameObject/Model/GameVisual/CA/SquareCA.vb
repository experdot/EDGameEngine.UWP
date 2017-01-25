Imports System.Numerics
Imports Microsoft.Graphics.Canvas.Geometry
Imports Windows.UI
''' <summary>
''' 正方形网格元胞自动机
''' </summary>
Public Class SquareCA
    Inherits GeometyCABase

    Public Property Size As Integer = 2

    Public Overrides Sub StartEx()
        Width = 300
        Height = 200

        Geometry = CanvasGeometry.CreateRectangle(Scene.World.ResourceCreator, New Rect(-Size / 2, -Size / 2, Size, Size))

        ReDim Cells(Width - 1, Height - 1)
        For i = 0 To Width - 1
            For j = 0 To Height - 1
                If Rnd.NextDouble > 0.618 Then
                    Cells(i, j) = New Cell With {.Location = New Vector2(i * Size, j * Size),
                                                 .Color = Colors.Black,
                                                 .Size = Size}
                Else
                    Cells(i, j) = Nothing
                End If
            Next
        Next

        Rect = New Rect(0, 0, Width * Size, Height * Size)
    End Sub

    Public Overrides Sub UpdateEx()
        Static count As Integer = 0
        If count < 2 Then
            count += 1
            Exit Sub
        Else
            count = 0
        End If

        Dim generation(Width - 1, Height - 1) As ICell
        For i = 0 To Width - 1
            For j = 0 To Height - 1
                Dim temp As Integer = GetSquareAroud(Cells, i, j, Width, Height)
                If Cells(i, j) Is Nothing Then
                    If temp = 3 Then
                        generation(i, j) = New Cell With {.Location = New Vector2(i * Size, j * Size), .Size = Size}
                    End If
                Else
                    If temp = 2 OrElse temp = 3 Then
                        generation(i, j) = Cells(i, j)
                    End If
                End If
            Next
        Next
        Cells = generation
    End Sub

    ''' <summary>
    ''' 返回正方形网格细胞邻居数量
    ''' </summary>
    Private Function GetSquareAroud(ByRef cellArr(,) As ICell, x As Integer, y As Integer, w As Integer, h As Integer) As Integer
        Static xArray() As Integer = {-1, 0, 1, 1, 1, 0, -1, -1}
        Static yArray() As Integer = {-1, -1, -1, 0, 1, 1, 1, 0}
        Dim dx, dy, ResultValue As Integer
        For i = 0 To 7
            dx = x + xArray(i)
            dy = y + yArray(i)
            If dx >= 0 AndAlso dy >= 0 AndAlso dx < w AndAlso dy < h Then
                If cellArr(dx, dy) IsNot Nothing Then
                    ResultValue += 1
                End If
            End If
        Next
        Return ResultValue
    End Function

End Class
