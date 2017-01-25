Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI

Public Class HexgonCA
    Inherits GeometyCABase

    Public Property Size As Integer = 8

    Public Overrides Sub StartEx()
        Width = 40
        Height = 30

        Geometry = GeometryHelper.CreateRegularPolygon(Scene.World.ResourceCreator, 6, Size)

        Dim dx As Single = CSng(Math.Sqrt(3) * Size)
        Dim dy As Single = CSng(Size / 2 * 3)
        ReDim Cells(Width - 1, Height - 1)
        For i = 0 To Width - 1
            For j = 0 To Height - 1
                If Rnd.NextDouble > 0.618 Then
                    Dim loc As New Vector2(If(j Mod 2 = 0, i * dx, i * dx + dx / 2), j * dy)
                    Cells(i, j) = New Cell With {.Location = loc,
                                                 .Color = Colors.Black,
                                                 .Size = Size}
                Else
                    Cells(i, j) = Nothing
                End If
            Next
        Next
        Rect = New Rect(0, 0, Width * dx, Height * dy)
    End Sub

    Public Overrides Sub UpdateEx()
        Static count As Integer = 0
        If count < 2 Then
            count += 1
            Exit Sub
        Else
            count = 0
        End If

        Dim dx As Single = CSng(Math.Sqrt(3) * Size)
        Dim dy As Single = CSng(Size / 2 * 3)
        Dim generation(Width - 1, Height - 1) As ICell
        For i = 0 To Width - 1
            For j = 0 To Height - 1
                Dim temp As Integer = GetHexgonAroud(Cells, i, j, Width, Height)
                If Cells(i, j) Is Nothing Then
                    If temp = 3 Then
                        Dim loc As New Vector2(If(j Mod 2 = 0, i * dx, i * dx + dx / 2), j * dy)
                        generation(i, j) = New Cell With {.Location = loc, .Size = Size}
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
    ''' 返回六边形网格细胞邻居数量
    ''' </summary>
    Private Function GetHexgonAroud(ByRef cellArr(,) As ICell, x As Integer, y As Integer, w As Integer, h As Integer) As Integer
        Static xArrayL() As Integer = {-1, 0, 1, 0, -1, -1}
        Static yArrayL() As Integer = {-1, -1, 0, 1, 1, 0}
        Static xArrayR() As Integer = {0, 1, 1, 1, 0, -1}
        Static yArrayR() As Integer = {-1, -1, 0, 1, 1, 0}
        Dim dx, dy, ResultValue As Integer
        For i = 0 To 5
            If y Mod 2 = 0 Then
                dx = x + xArrayL(i)
                dy = y + yArrayL(i)
            Else
                dx = x + xArrayR(i)
                dy = y + yArrayR(i)
            End If

            If dx >= 0 AndAlso dy >= 0 AndAlso dx < w AndAlso dy < h Then
                If cellArr(dx, dy) IsNot Nothing Then
                    ResultValue += 1
                End If
            End If
        Next
        Return ResultValue
    End Function
End Class
