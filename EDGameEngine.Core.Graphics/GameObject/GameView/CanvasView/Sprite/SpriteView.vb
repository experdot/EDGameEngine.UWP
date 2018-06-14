Imports Microsoft.Graphics.Canvas
''' <summary>
''' 贴图物体的视图
''' </summary>
Public Class SpriteView
    Inherits TypedCanvasView(Of ISprite)
    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        If Target.Image IsNot Nothing Then
            session.DrawImage(Target.Image)
        End If
    End Sub
End Class

