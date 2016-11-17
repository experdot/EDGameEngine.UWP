Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 可视化指针的视图
''' </summary>
Public Class PointerView
    Inherits TypedGameView(Of Pointer)
    Public Sub New(Target As Pointer)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Static col As Color = Colors.Black
        Dim index As Integer = 1
        For Each SubVec In Target.LocQueue
            col.A = CByte(CInt(255 * (index / Target.LocQueue.Count)))
            DrawingSession.FillCircle(SubVec, CSng(10 * (index / Target.LocQueue.Count)), col)
            index += 1
        Next
    End Sub
End Class
