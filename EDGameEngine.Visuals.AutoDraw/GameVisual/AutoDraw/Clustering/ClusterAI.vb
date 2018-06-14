Imports System.Numerics
Imports Windows.UI
Imports EDGameEngine.Core.Utilities
Imports EDGameEngine.Visuals.AutoDraw
''' <summary>
''' 聚类AI
''' </summary>
Public Class ClusterAI
    Implements IVertexWithLayerProvider
    Public Property IsOver As Boolean Implements IVertexWithLayerProvider.IsOver
    ''' <summary>
    ''' 线条集
    ''' </summary>
    Public Property Lines As New List(Of ILine)
    ''' <summary>
    ''' 层集
    ''' </summary>
    Public Property Hierarchies As New List(Of IHierarchy)
    ''' <summary>
    ''' 最大层数
    ''' </summary>
    Public Property MaxRank As Integer

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(pixels As PixelData, Optional maxRank As Integer = 10)
        Me.MaxRank = maxRank
        Dim start As DateTime = DateTime.Now
        Debug.WriteLine("Initialize Start")
        Hierarchies.Add(GridHierarchy.CreateFromPixels(pixels))
        Debug.WriteLine($"Initialize Over,TimeConsuming:{DateTime.Now - start}")
        Debug.WriteLine($"Generation Start")
        For i = 0 To maxRank - 1
            start = DateTime.Now
            Hierarchies.Add(Hierarchies.Last.Generate())
            Debug.WriteLine($"Total:{maxRank},Current:{i + 1},Time Consuming:{DateTime.Now - start},Hierarchy[{Hierarchies.Last.ToString()}]")
        Next
        Debug.WriteLine($"Pixels:{pixels.Colors.Length}")


        Dim converter As New CircleConverter(maxRank)

        '快速涂抹
        Dim current As Integer = maxRank - 1
        Dim index As Integer = 1 'maxRank - 3
        For i = maxRank - 1 To index Step -1
            If Hierarchies(i).Clusters.Count > 0 Then
                Lines.AddRange(converter.Convert(Hierarchies(i)))
            End If
            current = i
        Next

        '迭代细节
        'For i = current To 0 Step -1
        '    If Hierarchies(i).Clusters.Count > 0 Then
        '        Lines.AddRange(GenerateLinesQuality(Hierarchies(i)))
        '        Exit For
        '    End If
        'Next
    End Sub

    Public Function NextPoint() As VertexWithLayer Implements IVertexWithLayerProvider.NextPoint
        Static LineIndex As Integer = 0
        Static PointIndex As Integer = -1
        If LineIndex < Lines.Count Then
            For i = LineIndex To Lines.Count - 1
                PointIndex += 1
                If PointIndex < Lines(i).Points.Count Then
                    Return Lines(i).Points(PointIndex)
                Else
                    PointIndex = -1
                    LineIndex += 1
                End If
            Next
        End If
        IsOver = True
        Return Nothing
    End Function
End Class
