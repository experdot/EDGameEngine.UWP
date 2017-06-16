Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 线条画管理器
''' </summary>
Public Class DrawingManager
    ''' <summary>
    ''' 线条画集合
    ''' </summary>
    Public Property Drawings As List(Of Drawing)

    Public Property Width As Integer
    Public Property Height As Integer

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        Drawings = New List(Of Drawing)
    End Sub
    ''' <summary>
    ''' 由指定的图像初始化
    ''' </summary>
    Public Sub InitFromImage(image As CanvasBitmap, count As Integer)
        Width = CInt(image.Bounds.Width)
        Height = CInt(image.Bounds.Height)
        Dim sizes As Single() = {16.851, 12.234, 6.617, 3.618, 2, 0.618, 0.38}
        Dim alphas As Byte() = {90, 130, 160, 190, 210, 240, 250}
        'Dim alphas As Byte() = {100, 125, 145, 160, 170, 175, 180}
        Dim noises As Integer() = {70, 50, 30, 20, 10, 5, 2}
        Dim splits As Integer() = {3, 4, 5, 6, 7, 8, 8}

        Drawings.Clear()
        For i = 0 To count - 2
            Dim pixel As PixelData = GetGaussianPixelData(image, CInt((8 - i) / 2))
            Drawings.Add(New Drawing(pixel, i + 3, i, ScanMode.Circle) With {.PenAlpha = CInt(alphas(i) / 16), .PenSize = sizes(i) / 2})
            Drawings(i).Denoising(CInt(noises(i) / 1.6))
            Drawings(i).UpdatePointsSizeOfLine()
            Drawings(i).UpdatePointsColorOfLine()
        Next
        Dim tempPixels As New PixelData(image.GetPixelColors, CInt(image.Bounds.Width), CInt(image.Bounds.Height))
        Drawings.Add(New Drawing(tempPixels, 9, count - 1, ScanMode.Circle) With {.PenAlpha = 255, .PenSize = 1})
    End Sub

    Private Function GetGaussianPixelData(image As CanvasBitmap, Optional amount As Integer = 3) As PixelData
        Dim result As PixelData
        Using render = New CanvasRenderTarget(image.Device, Width, Height, 96)
            Using ds = render.CreateDrawingSession
                Using gaussian = New Effects.GaussianBlurEffect With {.Source = image, .BlurAmount = amount}
                    ds.Clear(Colors.Transparent)
                    ds.DrawImage(gaussian)
                End Using
            End Using
            result = New PixelData(render.GetPixelColors, CInt(image.Size.Width), CInt(image.Size.Height))
        End Using
        Return result
    End Function

    ''' <summary>
    ''' 返回快速的下一个点
    ''' </summary>
    Public Function NextPointFast() As PointWithLayer
        Static Index0, Index1, Index2 As Integer
        Static IsOver As Boolean
        If IsOver Then
            Return New PointWithLayer() With {.Color = Colors.Transparent, .Position = Vector2.Zero, .Size = 1}
        End If
        While (Index2 >= Drawings(Index0).Lines(Index1).Points.Count)
            Index2 = 0
            Index1 += 1
            While (Index1 >= Drawings(Index0).Lines.Count)
                Index1 = 0
                Index0 += 1
                If Index0 >= Drawings.Count Then
                    IsOver = True
                    Return New PointWithLayer() With {.Color = Colors.Transparent, .Position = Vector2.Zero, .Size = 0}
                End If
            End While
        End While
        Dim tp As PointWithLayer = Drawings(Index0).Lines(Index1).Points(Index2)
        Dim tc As Color = tp.Color
        tc.A = CByte(Drawings(Index0).PenAlpha)
        Dim size As Single = tp.Size * Drawings(Index0).PenSize
        Index2 += 1
        Return New PointWithLayer() With {.Color = tc, .Position = tp.Position, .Size = size, .LayerIndex = tp.LayerIndex}
    End Function
    ''' <summary>
    ''' 返回高质量的下一个点
    ''' </summary>
    Public Function NextPointQuality() As PointWithLayer
        Static Max As Single = If(Width > Height, Width, Height)
        Static radius As Single = CSng(Max / 20)
        Static count As Integer = CInt((Max / radius) * (Max / radius)) * 2
        Static Collections As New List(Of List(Of Line))
        Static IsCreate As Boolean = True
        If IsCreate Then
            IsCreate = False
            For i = 0 To count - 1
                Collections.Add(NextLinesInCircle(radius))
                Debug.WriteLine($"已完成{i + 1}个，完成度{Math.Round((i + 1) / count * 100, 2)}%")
            Next

            'Dim templine1, templine2, templine3 As New Line
            'For i = 0 To 0
            '    templine1.Points.Add(New PointWithLayer() With {.Color = Colors.Black, .Position = New Vector2(i + 50, i + 50)})
            'Next
            'For i = 0 To 100
            '    templine2.Points.Add(New PointWithLayer() With {.Color = Colors.Black, .Position = New Vector2(i + 100, i + 50)})
            'Next
            'For i = 0 To 200
            '    templine3.Points.Add(New PointWithLayer() With {.Color = Colors.Black, .Position = New Vector2(i + 150, i + 50)})
            'Next
            'templine1.CalcSize()
            'templine2.CalcSize()
            'templine3.CalcSize()
            'Collections.Add(New List(Of Line) From {templine1, templine2, templine3})



            Collections.RemoveAll(Function(lines As List(Of Line))
                                      Return lines.Count = 0
                                  End Function)
            Collections.ForEach(Sub(lines As List(Of Line))
                                    lines.RemoveAll(Function(line As Line)
                                                        Return line.Points.Count = 0
                                                    End Function)
                                End Sub)
        End If

        Static Index0, Index1, Index2 As Integer
        Static IsOver As Boolean
        If IsOver Then
            Return New PointWithLayer() With {.Color = Colors.Transparent, .Position = Vector2.Zero, .Size = 1}
        End If
        While (Index1 >= Collections(Index0).Count)
            Index1 = 0
            Collections.RemoveAt(Index0)
            Index0 = CInt(Math.Abs(RandomHelper.NextNorm(-Collections.Count + 1, Collections.Count - 1)) * 0.15F)
            'Index0 += 1
            If Index0 >= Collections.Count Then
                IsOver = True
                Return New PointWithLayer() With {.Color = Colors.Transparent, .Position = Vector2.Zero, .Size = 0}
            End If
        End While
        While (Index2 >= Collections(Index0).Item(Index1).Points.Count)
            Index2 = 0
            Index1 += 1
            While (Index1 >= Collections(Index0).Count)
                Index1 = 0
                Collections.RemoveAt(Index0)
                Index0 = CInt(Math.Abs(RandomHelper.NextNorm(-Collections.Count + 1, Collections.Count)) * 0.15F)
                'Index0 += 1
                If Index0 = 0 AndAlso Collections.Count = 1 Then
                    IsOver = IsOver
                End If
                If Index0 >= Collections.Count Then
                    IsOver = True
                    Return New PointWithLayer() With {.Color = Colors.Transparent, .Position = Vector2.Zero, .Size = 0}
                End If
            End While
        End While

        Dim tp As PointWithLayer = Collections(Index0).Item(Index1).Points(Index2)
        Dim tc As Color = tp.Color
        tc.A = CByte(Drawings(tp.LayerIndex).PenAlpha)
        Dim size As Single = tp.Size * Drawings(tp.LayerIndex).PenSize
        Index2 += 1
        Return New PointWithLayer() With {.Color = tc, .Position = tp.Position, .Size = size, .LayerIndex = tp.LayerIndex}

    End Function
    ''' <summary>
    ''' 返回参考图层的线段,矩形扫描
    ''' </summary>
    Public Function NextLines(radius As Single) As List(Of Line)
        Static x As Single = 0
        Static y As Single = 0
        Static DistanceMax As Single = New Vector2(Width, Height).Length
        Static Distance As Single = DistanceMax / Drawings.Count
        Static DCount As Single = Drawings.Count * Drawings.Count
        Dim result As New List(Of Line)
        If x < Width Then
            y = y + radius
            If y >= Height Then
                y = 0
                x += radius
            End If
            For i = 0 To Drawings.Count - 1
                result.AddRange(Drawings(i).NextLinesByLocation(New Vector2(x, y), CSng((DistanceMax - i * Distance) * (1 - (i * i) / DCount))))
            Next
        End If
        Return result
    End Function
    ''' <summary>
    ''' 返回参考图层的线段,圆形扫描
    ''' </summary>
    Public Function NextLinesInCircle(radius As Single) As List(Of Line)
        Static xCount As Integer = Width - 1
        Static yCount As Integer = Height - 1
        Static CP As New Vector2(CSng(xCount / 2), CSng(yCount / 2))
        Static DistanceMax As Single = New Vector2(Width, Height).Length / 2
        Static Distance As Single = DistanceMax / Drawings.Count
        Static DCount As Single = Drawings.Count * Drawings.Count
        Static StartR As Single = 0
        Static StartTheat As Single

        Dim result As New List(Of Line)

        For r As Single = StartR To If(xCount > yCount, xCount, yCount) Step radius * 0.38F

            While StartTheat < Math.PI * 2
                Dim dx As Integer = CInt(CP.X + r * Math.Cos(StartTheat))
                Dim dy As Integer = CInt(CP.Y + r * Math.Sin(StartTheat))
                For i = 0 To Drawings.Count - 1
                    result.AddRange(Drawings(i).NextLinesByLocation(New Vector2(dx, dy), CSng((DistanceMax - i * Distance) * (1 - (i / Drawings.Count)))))
                Next
                StartR = r
                StartTheat += (1 / r) * radius * 0.38F
                Return result
            End While
            StartTheat = 0
        Next
        Return result
    End Function
End Class
