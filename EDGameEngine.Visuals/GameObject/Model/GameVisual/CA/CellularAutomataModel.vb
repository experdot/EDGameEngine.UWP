Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class CellularAutomataModel
    Inherits GameBody
    Implements ICellularAutomata
    Public Property Cells As ICell(,) Implements ICellularAutomata.Cells
    ''' <summary>
    ''' 原图
    ''' </summary>
    Public Property Image As CanvasBitmap

    Public Property Width As Integer = 300
    Public Property Height As Integer = 200
    Public Property Size As Integer = 1

    Dim colorArr As Color()

    Public Overrides Sub StartEx()
        Width = CInt(Image.Bounds.Width)
        Height = CInt(Image.Bounds.Height)
        colorArr = Image.GetPixelColors

        Rect = New Rect(0, 0, Width * Size, Height * Size)
        ReDim Cells(Width - 1, Height - 1)
        For i = 0 To Width - 1
            For j = 0 To Height - 1
                If Rnd.NextDouble > 0.618 Then
                    Cells(i, j) = New Cell With {.Location = New Vector2(i * Size, j * Size),
                                                 .Color = Color.FromArgb(255, CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256))),
                                                 .Size = Size}
                    Cells(i, j).Color = colorArr(CInt(j * Image.Bounds.Width + i))
                    'Cells(i, j).Color = Colors.Black
                Else
                    Cells(i, j) = Nothing
                End If
            Next
        Next
        'GameComponents.Effects.Add(New ShadowEffect)
        'GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height), .Opacity = 0.5})
    End Sub

    Public Overrides Sub UpdateEx()
        Static bc As Integer = 0
        bc += 1
        Debug.WriteLine(bc)
        Static count As Integer = 0
        If count < 32 Then
            count += 1
            Exit Sub
        Else
            count = 0
        End If

        Dim carr(8) As Integer
        Dim generation(Width - 1, Height - 1) As ICell
        For i = 0 To Width - 1
            For j = 0 To Height - 1
                Dim temp As Integer = GetAroundValue(Cells, i, j, Width, Height)
                carr(temp) += 1
                If Cells(i, j) Is Nothing Then
                    If temp = 3 Then
                        generation(i, j) = New Cell With {.Location = New Vector2(i * Size, j * Size), .Size = Size}
                        generation(i, j).Color = GetAroundColor(Cells, i, j, Width, Height)
                    End If
                Else
                    If temp = 2 OrElse temp = 3 Then
                        generation(i, j) = Cells(i, j)
                        If generation(i, j).Color.A > 10 Then
                            Dim b As Color = generation(i, j).Color
                            generation(i, j).Color = Color.FromArgb(CByte(b.A - 10), b.R, b.G, b.B)
                        End If
                    End If
                End If
            Next
        Next
        Cells = generation
    End Sub

    ''' <summary>
    ''' 返回细胞权值
    ''' </summary>
    Private Function GetAroundValue(ByRef cellArr(,) As ICell, x As Integer, y As Integer, w As Integer, h As Integer) As Integer
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

    ''' <summary>
    ''' 返回权值颜色
    ''' </summary>
    Private Function GetAroundColor(ByRef cellArr(,) As ICell, x As Integer, y As Integer, w As Integer, h As Integer) As Color
        Static xArray() As Integer = {-1, 0, 1, 1, 1, 0, -1, -1}
        Static yArray() As Integer = {-1, -1, -1, 0, 1, 1, 1, 0}
        Dim dx, dy, ResultValue As Integer
        Dim a, r, g, b As Integer
        For i = 0 To 7
            dx = x + xArray(i)
            dy = y + yArray(i)
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
        'r += colorArr(CInt(y * Image.Bounds.Width + x)).R
        'g += colorArr(CInt(y * Image.Bounds.Width + x)).G
        'b += colorArr(CInt(y * Image.Bounds.Width + x)).B
        'ResultValue += 1
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
