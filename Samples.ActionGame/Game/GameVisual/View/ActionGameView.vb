Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class ActionGameView
    Inherits TypedCanvasView(Of ActionGameModel)
    Public Overrides Sub OnDraw(session As CanvasDrawingSession)
        Static SrcRect As Rect = New Rect(0, 0, 128, 128)
        Dim imageResource As ImageResource = CType(Target.Scene, IObjectWithImageResource).ImageResource

        For Each block In Target.Mission.Blocks
            If block Is Nothing Then Continue For
            Dim temp = block.Location * Target.Scale
            Dim size = New Vector2(CSng(block.Collide.Rect.Width), CSng(block.Collide.Rect.Height)) * Target.Scale
            Dim destRect As Rect = New Rect(temp.X - size.X / 2, temp.Y - size.Y / 2, size.X, size.Y)
            Dim mat = Matrix3x2.CreateRotation(block.Rotation, New Vector2(temp.X, temp.Y))
            session.Transform = mat
            session.DrawImage(imageResource.GetResource(block.Image.Value), destRect, SrcRect)
        Next
        For Each character In Target.Mission.Characters
            If character Is Nothing Then Continue For
            Dim temp = character.Location * Target.Scale
            Dim size = New Vector2(CSng(character.Collide.Rect.Width), CSng(character.Collide.Rect.Height)) * Target.Scale
            Dim destRect As Rect = New Rect(temp.X - size.X / 2, temp.Y - size.Y / 2, size.X, size.Y)
            Dim mat = Matrix3x2.CreateRotation(character.Rotation, New Vector2(temp.X, temp.Y))
            session.Transform = mat
            session.DrawImage(imageResource.GetResource(character.Image.Value), destRect, SrcRect)
        Next
    End Sub

End Class
