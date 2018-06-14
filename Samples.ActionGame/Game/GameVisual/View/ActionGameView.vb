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

        For Each SubBlock In Target.Mission.Blocks
            If SubBlock Is Nothing Then Continue For
            Dim temp = SubBlock.Location * Target.Scale
            Dim size = New Vector2(CSng(SubBlock.Collide.Rect.Width), CSng(SubBlock.Collide.Rect.Height)) * Target.Scale
            Dim destRect As Rect = New Rect(temp.X - size.X / 2, temp.Y - size.Y / 2, size.X, size.Y)
            Dim mat = Matrix3x2.CreateRotation(SubBlock.Rotation, New Vector2(temp.X, temp.Y))
            session.Transform = mat
            session.DrawImage(imageResource.GetResource(SubBlock.Image.Value), destRect, SrcRect)
        Next
        For Each SubCharacter In Target.Mission.Characters
            If SubCharacter Is Nothing Then Continue For
            Dim temp = SubCharacter.Location * Target.Scale
            Dim size = New Vector2(CSng(SubCharacter.Collide.Rect.Width), CSng(SubCharacter.Collide.Rect.Height)) * Target.Scale
            Dim destRect As Rect = New Rect(temp.X - size.X / 2, temp.Y - size.Y / 2, size.X, size.Y)
            Dim mat = Matrix3x2.CreateRotation(SubCharacter.Rotation, New Vector2(temp.X, temp.Y))
            session.Transform = mat
            session.DrawImage(imageResource.GetResource(SubCharacter.Image.Value), destRect, SrcRect)
        Next
    End Sub

End Class
