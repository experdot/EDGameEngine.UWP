Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
''' <summary>
''' 粒子系统视图
''' </summary>
Public Class ParticalsView
    Inherits TypedGameView(Of IParticals)
    Public Sub New(Target As IParticals)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        For Each SubPartical In Target.Particals
            drawingSession.FillCircle(SubPartical.Location, SubPartical.Size, SubPartical.Color)
        Next
    End Sub
End Class
