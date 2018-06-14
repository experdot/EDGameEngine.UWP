Imports System.Numerics
Imports EDGameEngine.Core.Graphics
Imports Windows.UI
''' <summary>
''' 由网格表示的层
''' </summary>
Public Class GridHierarchy
    Inherits HierarchyBase
    ''' <summary>
    ''' 网格
    ''' </summary>
    Public Property Grid As List(Of Cluster)(,)
    ''' <summary>
    ''' 宽度
    ''' </summary>
    Public Property Width As Integer
    ''' <summary>
    ''' 高度
    ''' </summary>
    Public Property Height As Integer
    ''' <summary>
    ''' 网格大小
    ''' </summary>
    Public Property Size As Single

    'Private Shared OffsetX() As Integer = {0, -1, 0, 1, 1, 1, 0, -1, -1}
    'Private Shared OffsetY() As Integer = {0, -1, -1, -1, 0, 1, 1, 1, 0}
    Private Shared OffsetX() As Integer = {0, -1, 1, 0, 0, -1, 1, -1, 1}
    Private Shared OffsetY() As Integer = {0, 0, 0, -1, 1, -1, 1, 1, -1}

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(w As Integer, h As Integer, size As Single)
        w = Math.Max(w, 1)
        h = Math.Max(h, 1)
        Me.Width = w
        Me.Height = h
        Me.Size = size
        ReDim Grid(w - 1, h - 1)
        For i = 0 To w - 1
            For j = 0 To h - 1
                Grid(i, j) = New List(Of Cluster)
            Next
        Next
    End Sub

    ''' <summary>
    ''' 由指定的<see cref="PixelData"/>对象创建一个实例
    ''' </summary>
    Public Shared Function CreateFromPixels(pixels As PixelData) As GridHierarchy
        Dim result As New GridHierarchy(pixels.Width, pixels.Height, 1)
        For x = 0 To pixels.Width - 1
            For y = 0 To pixels.Height - 1
                Dim dx, dy As Integer
                Dim direction As New Vector2
                Dim color As Color = pixels.Colors(y * pixels.Width + x)
                Dim gray = BitmapPixelHelper.GetColorAverage(color)
                For k = 1 To 8
                    dx = x + OffsetX(k)
                    dy = y + OffsetY(k)
                    If (dx >= 0 AndAlso dy >= 0 AndAlso dx < pixels.Width AndAlso dy < pixels.Height) Then
                        Dim offsetColor = pixels.Colors(dy * pixels.Width + dx)
                        Dim radius = gray - BitmapPixelHelper.GetColorAverage(offsetColor)
                        direction += New Vector2(OffsetX(k), OffsetY(k)) * radius
                    End If
                Next
                Dim cluster As New Cluster With
                {
                    .Position = New Vector2(x, y),
                    .Color = color,
                    .Direction = direction
                }
                result.Grid(x, y).Add(cluster)
                result.Clusters.Add(cluster)
                'result.AddCluster(cluster)'该方法存在性能问题
            Next
        Next
        Return result
    End Function

    Public Shared IgnoreCount As Integer
    Public Overrides Function Generate() As IHierarchy
        Dim rate As Single = 4.0F
        Dim newSize As Single = Me.Size * rate
        Dim result As New GridHierarchy(CInt(Math.Ceiling(Me.Width / rate) + 1), CInt(Math.Ceiling(Me.Height / rate) + 1), newSize) With {.Rank = Me.Rank + 1}

        '合并为新簇
        For Each cluster In Clusters
            Dim similar As Cluster = cluster.GetMostSimilarCluster(GetNeighbours(cluster)).First
            If similar IsNot Nothing Then
                If cluster.Parent Is Nothing AndAlso similar.Parent Is Nothing Then
                    result.Clusters.Add(Cluster.Combine(cluster, similar))
                    'result.AddCluster(Cluster.Combine(SubCluster, similar), False) '该方法存在性能问题
                Else
                    Cluster.Combine(cluster, similar)
                End If
            Else
                IgnoreCount += 1
                Dim parent As New Cluster
                parent.Children.Add(cluster)
                result.Clusters.Add(parent)
            End If
        Next
        '设置属性
        For Each cluster In result.Clusters
            cluster.Position = cluster.GetAveragePosition()
            cluster.Color = cluster.GetAverageColor()
            cluster.Direction = cluster.GetAverageDirection()
        Next
        '分配至网格
        For Each cluster In result.Clusters
            Dim p As Vector2 = cluster.Position
            Dim x As Integer = CInt(p.X / result.Size)
            Dim y As Integer = CInt(p.Y / result.Size)
            result.Grid(x, y).Add(cluster)
        Next
        Return result
    End Function

    Public Overrides Function ToString() As String
        Return $"Rank:{Rank}Clusters.Count:{Clusters.Count}"
    End Function

    Private Function GetNeighbours(cluster As Cluster) As List(Of Cluster)
        Dim result As New List(Of Cluster)
        Dim xBound As Integer = Grid.GetUpperBound(0)
        Dim yBound As Integer = Grid.GetUpperBound(1)
        Dim dx, dy As Integer
        Dim x As Integer = CInt(cluster.Position.X / Size)
        Dim y As Integer = CInt(cluster.Position.Y / Size)
        For i = 0 To 8
            dx = x + OffsetX(i)
            dy = y + OffsetY(i)
            If (dx >= 0 AndAlso dy >= 0 AndAlso dx <= xBound AndAlso dy <= yBound) Then
                result.AddRange(Grid(dx, dy))
            Else
                Continue For
            End If
        Next
        result.Remove(cluster)
        Return result
    End Function
End Class
