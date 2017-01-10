Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
''' <summary>
''' 元胞自动机的视图
''' </summary>
Public Class CelluarAutomataView
    Inherits TypedGameView(Of ICellularAutomata)
    Public Sub New(Target As ICellularAutomata)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        For Each SubCell In Target.Cells
            If SubCell IsNot Nothing Then
                Dim offset = SubCell.Size / 2
                drawingSession.FillRectangle(New Rect(SubCell.Location.X - offset, SubCell.Location.Y - offset, SubCell.Size, SubCell.Size), SubCell.Color)
            End If
        Next
    End Sub
End Class
