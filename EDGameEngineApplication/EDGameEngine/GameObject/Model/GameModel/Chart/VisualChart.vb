Imports EDGameEngine
Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 表示一个可视化图表
''' </summary>
Public Class VisualChart
    Inherits GameVisualModel
    Public Overrides Property Presenter As GameView = New ChartView(Me)
    Public DataList As New List(Of List(Of Single))
    Public Shared Rnd As New Random
    Public ColorList() As Color = {Colors.Yellow, Colors.Blue, Colors.White}
    Public LabelList() As String = {"X: ", "Y: ", "Z: "}
    Dim TempSingle As Single
    Public Sub New(loc As Vector2)
        Location = loc
        For i = 0 To 2
            DataList.Add(New List(Of Single))
            DataList(i).Add(0)
            DataList(i).Add(0)
        Next
    End Sub
    Public Overrides Sub Update()
        TempSingle = (TempSingle + 0.05) Mod Math.PI * 2
        For i = 0 To 2
            DataList(i).Add(Math.Sin(TempSingle + i) * Math.Cos(TempSingle + i) * 200)
            If DataList(i).Count * 2 > 100 * 0.6 Then
                DataList(i).RemoveAt(0)
            End If
        Next
    End Sub
End Class
