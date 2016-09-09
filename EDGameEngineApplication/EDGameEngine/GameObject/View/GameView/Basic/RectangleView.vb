﻿Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class RectangleView
    Inherits TypedGameView(Of VisualRectangle)
    Public Sub New(Target As VisualRectangle)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession)
            Using Dl = cmdList.CreateDrawingSession
                DrawRectangle(Dl)
            End Using
            DrawingSession.DrawImage(cmdList)
        End Using
    End Sub
    Public Sub DrawRectangle(DS As CanvasDrawingSession)
        DS.FillRectangle(Target.Rect, Colors.Black)
    End Sub
End Class