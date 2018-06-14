''' <summary>
''' 容器地图，用于记录容器中碎片的空间位置
''' </summary>
Public Class FragmentContainerMap
    ''' <summary>
    ''' 宽度
    ''' </summary>
    Public Property Width As Integer
    ''' <summary>
    ''' 高度
    ''' </summary>
    Public Property Height As Integer
    ''' <summary>
    ''' 单元格集合
    ''' </summary>
    Public Property Cells As List(Of MapCell)
    ''' <summary>
    ''' 地图信息
    ''' </summary>
    Public ReadOnly Property MapData As Boolean(,)
    ''' <summary>
    ''' 获取或设置指定位置的值
    ''' </summary>
    Public Property Value(x As Integer, y As Integer) As Boolean
        Get
            Return MapData(Width + x, Height + y)
        End Get
        Set(value As Boolean)
            MapData(Width + x, Height + y) = value
        End Set
    End Property
    ''' <summary>
    ''' 由指定的长度和高度创建并初始化一个碎片容器地图对象
    ''' </summary>
    Public Sub New(w As Integer, h As Integer)
        Width = w
        Height = h
        Cells = New List(Of MapCell)
        ReDim MapData(w - 1, h - 1)
    End Sub
    ''' <summary>
    ''' 计算地图单元
    ''' </summary>
    Public Sub CalcCell(ByRef fragments As List(Of Fragment))
        Dim xArray As Integer() = {-1, 1, 0, 0}
        Dim yArray As Integer() = {0, 0, -1, 1}
        Cells.Clear()
        For x = 1 To Width - 2
            For y = 1 To Height - 2
                If MapData(x, y) = False Then
                    For i = 0 To 3
                        If MapData(x + xArray(i), y + yArray(i)) = True Then
                            Cells.Add(New MapCell(x - Width, y - Height,
                                           MapData(x + xArray(0), y + yArray(0)),
                                           MapData(x + xArray(1), y + yArray(1)),
                                           MapData(x + xArray(2), y + yArray(2)),
                                           MapData(x + xArray(3), y + yArray(3))))
                            Exit For
                        End If
                    Next
                Else
                    Dim count As Integer = 0
                    For i = 0 To 3
                        If MapData(x + xArray(i), y + yArray(i)) = True Then
                            count += 1
                        End If
                    Next
                    If count > 3 Then
                        Cells.Add(New MapCell(x - Width, y - Height,
                          MapData(x + xArray(0), y + yArray(0)),
                          MapData(x + xArray(1), y + yArray(1)),
                          MapData(x + xArray(2), y + yArray(2)),
                          MapData(x + xArray(3), y + yArray(3))))
                        For Each fragment In fragments
                            If fragment.Location.X = Cells.Last.Location.X And fragment.Location.Y = Cells.Last.Location.Y Then
                                Cells.Last.Fragment = fragment
                            End If
                        Next
                    End If
                End If
            Next
        Next
    End Sub
End Class
