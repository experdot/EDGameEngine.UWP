Public Class LineHelper
    ''' <summary>
    ''' 计算长度
    ''' </summary>
    Public Shared Sub Resize(line As ILine, split As Integer)
        Dim temp As New List(Of VertexWithLayer)
        If split > 0 Then
            For i = 0 To line.Points.Count - 1 Step split
                temp.Add(line.Points(i))
            Next
            If temp.Count > 0 Then
                If temp.Count > 1 Then
                    For i = 0 To temp.Count - 2
                        temp(i).NextPoint = temp(i + 1)
                    Next
                End If
                temp.Last.NextPoint = temp.Last
            End If
            line.Points.Clear()
            line.Points.AddRange(temp)
        End If
    End Sub
End Class
