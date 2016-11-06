Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI
Public Class SpriteView
    Inherits TypedGameView(Of Sprite)
    Public Sub New(Target As Sprite)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        If Target.Image IsNot Nothing Then
            DrawingSession.DrawImage(CType(Target.Image, ICanvasImage))
        End If
    End Sub
End Class

