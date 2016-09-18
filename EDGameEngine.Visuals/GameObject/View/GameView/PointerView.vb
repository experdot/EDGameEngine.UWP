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
        For i = 0 To Target.LocQueue.Count - 1
            Dim subVec = Target.LocQueue(i)
            DrawingSession.DrawLine(Target.Location, SubVec, Colors.Black)
        Next
    End Sub
End Class
