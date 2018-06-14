Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Utilities
Imports EDGameEngine.Visuals.AutoDraw
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 线条画管理器
''' </summary>
Public Class DrawingManager
    Implements IVertexWithLayerProvider
    Public Property IsOver As Boolean = False Implements IVertexWithLayerProvider.IsOver

    ''' <summary>
    ''' 线条画集合
    ''' </summary>
    Public Property Drawings As List(Of Drawing)
    ''' <summary>
    ''' 图像宽度
    ''' </summary>
    Public Property Width As Integer
    ''' <summary>
    ''' 图像高度
    ''' </summary>
    Public Property Height As Integer
    ''' <summary>
    ''' 是否快速识别模式
    ''' </summary>
    Public Property IsFastMode As Boolean = True
    ''' <summary>
    ''' 是否重置
    ''' </summary>
    Dim IsReset As Boolean = True
    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New()
        Drawings = New List(Of Drawing)
    End Sub

    Public Function NextPoint() As VertexWithLayer Implements IVertexWithLayerProvider.NextPoint
        If IsFastMode Then
            Return NextPointFast()
        Else
            Return NextPointQuality()
        End If
    End Function


    ''' <summary>
    ''' 由指定的图像初始化
    ''' </summary>
    Public Sub InitFromImage(image As CanvasBitmap, count As Integer)
        Width = CInt(image.Bounds.Width)
        Height = CInt(image.Bounds.Height)
        Dim sizes As Single() = {38, 22, 14, 8, 5.0, 2.8, 1.6, 1}
        Dim alphas As Byte() = {90, 120, 140, 155, 165, 170, 175}
        Dim noises As Integer() = {40, 30, 20, 10, 6, 4, 2}
        Dim splits As Integer() = {3, 4, 5, 6, 7, 8, 8}
        Dim lengths As Integer() = {40, 30, 25, 20, 15, 12, 10}
        Dim averages As Boolean() = {True, False, False, False, False, False, False}
        'Dim averages As Boolean() = {True, True, True, True, True, True, True, True}
        'Dim averages As Boolean() = {False, False, False, False, False, False, False, False}
        Drawings.Clear()

        '过渡图层
        For i = 0 To count - 2
            Dim pixel As PixelData = GetGaussianPixelData(image, CInt((8 - i) / 2))
            Drawings.Add(New Drawing(pixel, i + 3, i, ScanMode.Rect, False) With {.PenAlpha = CInt(alphas(i)), .PenSize = sizes(i)})
            Drawings(i).Denoising(2 + CInt(noises(i) / 5))
            Drawings(i).UpdatePointsSizeOfLine(lengths(i))
            Drawings(i).Denoising(1)
            Drawings(i).UpdatePointsColorOfLine(averages(i))
        Next
        '最终图层
        Dim tempPixels As New PixelData(image.GetPixelColors, CInt(image.Bounds.Width), CInt(image.Bounds.Height))
        Drawings.Add(New Drawing(tempPixels, 9, count - 1, ScanMode.Rect, False) With {.PenAlpha = 255, .PenSize = 1})
    End Sub
    ''' <summary>
    ''' 重置
    ''' </summary>
    Public Sub Reset()
        IsReset = True
    End Sub
    ''' <summary>
    ''' 返回速度优先的下一个点
    ''' </summary>
    Public Function NextPointFast() As VertexWithLayer
        Static Index0, Index1, Index2 As Integer
        If IsReset Then
            Index0 = 0
            Index1 = 0
            Index2 = 0
            IsOver = False
            IsReset = False
        End If
        If IsOver Then
            Return Nothing
        End If
        While (Index2 >= Drawings(Index0).Lines(Index1).Points.Count)
            Index2 = 0
            Index1 += 1
            While (Index1 >= Drawings(Index0).Lines.Count)
                Index1 = 0
                Index0 += 1
                If Index0 >= Drawings.Count Then
                    IsOver = True
                    Return Nothing
                End If
            End While
        End While
        Dim point As VertexWithLayer = Drawings(Index0).Lines(Index1).Points(Index2)
        point.UserSize = point.Size * Drawings(point.LayerIndex).PenSize
        Dim color As Color = point.Color
        color.A = CByte(Drawings(point.LayerIndex).PenAlpha)
        point.UserColor = color
        Index2 += 1
        Return point
    End Function
    ''' <summary>
    ''' 返回质量优先的下一个点
    ''' </summary>
    Public Function NextPointQuality() As VertexWithLayer
        Static Max As Single = If(Width > Height, Width, Height)
        Static radius As Single = CSng(Max / 20)
        Static count As Integer = CInt((Max / radius) * (Max / radius) * 3)
        Static Collections As New List(Of List(Of Line))
        Static IsCreate As Boolean = True
        If IsCreate Then
            IsCreate = False
            For i = 0 To count - 1
                Collections.Add(NextLinesInCircle(radius))
                Debug.WriteLine($"已完成{i + 1}个，完成度{Math.Round((i + 1) / count * 100, 2)}%")
            Next
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
        If IsReset Then
            Index0 = 0
            Index1 = 0
            Index2 = 0
            IsOver = False
            IsReset = False
        End If
        If IsOver Then
            Return Nothing
        End If
        While (Index1 >= Collections(Index0).Count)
            Index1 = 0
            Collections.RemoveAt(Index0)
            Index0 = CInt(Math.Abs(RandomHelper.NextNorm(-Collections.Count + 1, Collections.Count - 1)) * 0.15F)
            If Index0 >= Collections.Count Then
                IsOver = True
                Return Nothing
            End If
        End While
        While (Index2 >= Collections(Index0).Item(Index1).Points.Count)
            Index2 = 0
            Index1 += 1
            While (Index1 >= Collections(Index0).Count)
                Index1 = 0
                Collections.RemoveAt(Index0)
                Index0 = CInt(Math.Abs(RandomHelper.NextNorm(-Collections.Count + 1, Collections.Count)) * 0.15F)
                If Index0 = 0 AndAlso Collections.Count = 1 Then
                    IsOver = IsOver
                End If
                If Index0 >= Collections.Count Then
                    IsOver = True
                    Return Nothing
                End If
            End While
        End While

        Dim point As VertexWithLayer = Collections(Index0).Item(Index1).Points(Index2)
        point.UserSize = point.Size * Drawings(point.LayerIndex).PenSize
        Dim color As Color = point.Color
        color.A = CByte(Drawings(point.LayerIndex).PenAlpha)
        point.UserColor = color
        Index2 += 1
        Return point
    End Function
    ''' <summary>
    ''' 返回参考图层的线段,矩形扫描
    ''' </summary>
    Private Function NextLinesInRect(radius As Single) As List(Of Line)
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
                result.AddRange(Drawings(i).GetLinesByLocation(New Vector2(x, y), CSng((DistanceMax - i * Distance) * (1 - (i * i) / DCount))))
            Next
        End If
        Return result
    End Function
    ''' <summary>
    ''' 返回参考图层的线段,圆形扫描
    ''' </summary>
    Private Function NextLinesInCircle(radius As Single) As List(Of Line)
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
                    result.AddRange(Drawings(i).GetLinesByLocation(New Vector2(dx, dy), CSng((DistanceMax - i * Distance) * (1 - (i / Drawings.Count)))))
                Next
                StartR = r
                StartTheat += (1 / r) * radius * 0.38F
                Return result
            End While
            StartTheat = 0
        Next
        Return result
    End Function

    ''' <summary>
    ''' 返回指定图像高斯模糊处理后的像素数据
    ''' </summary>
    Private Function GetGaussianPixelData(image As CanvasBitmap, Optional amount As Integer = 3) As PixelData
        Dim result As PixelData
        Using render = New CanvasRenderTarget(image.Device, Width, Height, 96)
            Using session = render.CreateDrawingSession
                Using gaussian = New Effects.GaussianBlurEffect With {.Source = image, .BlurAmount = amount}
                    session.Clear(Colors.Transparent)
                    session.DrawImage(gaussian)
                End Using
            End Using
            result = New PixelData(render.GetPixelColors, CInt(image.Size.Width), CInt(image.Size.Height))
        End Using
        Return result
    End Function
End Class
