Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class ActionGameView
    Inherits TypedGameView(Of ActionGameModel)
    Public Sub New(Target As ActionGameModel)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Static SrcRect As Rect = New Rect(0, 0, 128, 128)
        For Each SubBlock In Target.Mission.Blocks
            If SubBlock Is Nothing Then Continue For
            Dim temp = SubBlock.Location * Target.Scale
            Dim destRect As Rect = New Rect(temp.X, temp.Y, Target.Scale, Target.Scale)
            drawingSession.DrawImage(Target.Scene.ImageManager.GetResource(SubBlock.Image.Value), destRect, SrcRect)
        Next

        For Each SubCharacter In Target.Mission.Characters
            If SubCharacter Is Nothing Then Continue For
            Dim temp = SubCharacter.Location * Target.Scale
            Dim destRect As Rect = New Rect(temp.X, temp.Y, Target.Scale, Target.Scale)
            Dim mat = Matrix3x2.CreateRotation(SubCharacter.Rotation, New Vector2(temp.X + Target.Scale / 2, temp.Y + Target.Scale / 2))
            drawingSession.Transform = mat
            drawingSession.DrawImage(Target.Scene.ImageManager.GetResource(SubCharacter.Image.Value), destRect, SrcRect)

        Next
    End Sub



End Class
