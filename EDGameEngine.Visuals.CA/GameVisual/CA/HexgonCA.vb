Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class HexgonCA
    Inherits GeometyCABase

    Public Property Size As Single = 5

    Public Overrides Sub StartEx()
        Width = 50
        Height = 50

        Geometry = GeometryHelper.CreateRegularPolygon(6, Size)

        Dim dx As Single = CSng(Math.Sqrt(3) * Size)
        Dim dy As Single = CSng(Size / 2 * 3)
        ReDim Cells(Width - 1, Height - 1)
        For i = 0 To Width - 1
            For j = 0 To Height - 1
                If Rnd.NextDouble > 0.618 Then
                    Dim loc As New Vector2(If(j Mod 2 = 0, i * dx, i * dx + dx / 2), j * dy)
                    Cells(i, j) = New Cell With {.Location = New Vector2(i * Size, j * Size),
                                                 .Color = Colors.Black,
                                                 .Size = Size}
                Else
                    Cells(i, j) = Nothing
                End If
            Next
        Next
        Rect = New Rect(0, 0, Width * dx, Height * dy)
        'GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width * dx, Height * dy), .Opacity = 1})
    End Sub

    Public Overrides Sub UpdateEx()
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
                        'generation(i, j).Color = GetAroundColor(Cells, i, j, Width, Height)
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


    ''' <summary>
    ''' 返回六边形网格细胞邻居均色
    ''' </summary>
    Private Function GetAroundColor(ByRef cellArr(,) As ICell, x As Integer, y As Integer, w As Integer, h As Integer) As Color
        Static xArrayL() As Integer = {-1, 0, 1, 0, -1, -1}
        Static yArrayL() As Integer = {-1, -1, 0, 1, 1, 0}
        Static xArrayR() As Integer = {0, 1, 1, 1, 0, -1}
        Static yArrayR() As Integer = {-1, -1, 0, 1, 1, 0}
        Dim dx, dy, ResultValue As Integer
        Dim a, r, g, b As Integer
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
                    a += cellArr(dx, dy).Color.A
                    r += cellArr(dx, dy).Color.R
                    g += cellArr(dx, dy).Color.G
                    b += cellArr(dx, dy).Color.B
                End If
            End If
        Next

        If ResultValue > 0 Then
            a = CInt(a / ResultValue)
            r = CalcRGB(r, ResultValue)
            g = CalcRGB(g, ResultValue)
            b = CalcRGB(b, ResultValue)
        End If
        Return Color.FromArgb(CByte(a), CByte(r), CByte(g), CByte(b))
    End Function
    Private Function CalcRGB(value As Integer, res As Integer) As Integer
        value = CInt(value / res) + Rnd.Next(1， 10) - 5
        If value < 0 Then value = 0
        If value > 255 Then value = 255
        Return value
    End Function
End Class
