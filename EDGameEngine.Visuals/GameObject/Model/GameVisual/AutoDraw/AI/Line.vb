Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 线条
''' </summary>
Public Class Line
    ''' <summary>
    ''' 点集
    ''' </summary>
    Public Property Points As New List(Of PointWithLayer)
    ''' <summary>
    ''' 位置
    ''' </summary>
    Public Property Location As Vector2

    ''' <summary>
    ''' 计算画笔大小
    ''' </summary>
    Public Sub CalcSize()
        If Points.Count > 0 Then
            Dim center As Integer = CInt(Points.Count / 2)
            Dim distance As Single
            For i = 0 To Points.Count - 1
                distance = CSng(center - Math.Abs(center - i))
                Points(i).Size = CSng(Math.Log(Math.E + distance) * Math.Log10(10 + Points.Count))
            Next
        End If
    End Sub
    ''' <summary>
    ''' 计算画笔颜色
    ''' </summary>
    Public Sub CalcAverageColor()
        If Points.Count > 0 Then
            Dim R, G, B As Integer
            For Each SubPoint In Points
                R += SubPoint.Color.R
                G += SubPoint.Color.G
                B += SubPoint.Color.B
            Next
            Dim tempCol As Color = Color.FromArgb(255, CByte(R / Points.Count),
                                                       CByte(G / Points.Count),
                                                       CByte(B / Points.Count))
            For Each SubPoint In Points
                SubPoint.Color = tempCol
            Next
        End If
    End Sub
    ''' <summary>
    ''' 计算线条位置
    ''' </summary>
    Public Sub CalcLocation()
        If Points.Count > 0 Then
            Dim x As Single = 0
            Dim y As Single = 0
            For i = 0 To Points.Count - 1
                x += Points(i).Position.X
                y += Points(i).Position.Y
            Next
            Me.Location = New Vector2(x / Points.Count, y / Points.Count)
        End If
    End Sub

    ''' <summary>
    ''' 是否与指定的位置向近
    ''' </summary>
    Public Function IsNear(loc As Vector2, distance As Single) As Boolean
        If (loc - Location).Length <= distance Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' 更新点的层索引
    ''' </summary>
    Public Sub UpdateLayerIndex(index As Integer)
        If Points.Count > 0 Then
            For i = 0 To Points.Count - 1
                Points(i).LayerIndex = index
            Next
        End If
    End Sub


End Class
