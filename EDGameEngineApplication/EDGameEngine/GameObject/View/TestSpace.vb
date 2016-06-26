Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class TestSpace
    Const WAlKERNUM = 5 '游走对象数量
    Const BLOCKERNUM = 20 '固定对象数量
    Const SPEED = 6000 '游走对象的灵活度（推荐1000~10000）
    Dim mySpace As BaseSpace
    Dim rnd As New Random
    Dim MouseX, MouseY As Double
    Dim isKeyDown As Integer
    Sub Start(ActualWidth#, ActualHeight#)
        mySpace = New BaseSpace(New Size(ActualWidth, ActualHeight))
        Dim rnd As New Random
        For i = 0 To WAlKERNUM - 1
            mySpace.AddWalker(rnd.Next(0, ActualWidth), rnd.Next(0, ActualHeight))
            mySpace.WalkerList(i).Mass = rnd.Next(10, 30)
            mySpace.WalkerList(i).StepUpon = mySpace.WalkerList(i).Mass
            mySpace.WalkerList(i).myColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
        Next
        For i = 0 To BLOCKERNUM - 1
            mySpace.AddBlocker(rnd.Next(ActualWidth), rnd.Next(ActualHeight))
            mySpace.BlockerList(i).Mass = rnd.Next(10, 80)
            mySpace.BlockerList(i).myColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
            If rnd.Next(2) > 1 Then mySpace.BlockerList(i).IsAdd = -1
        Next
    End Sub
    Sub Draw(ds As CanvasDrawingSession)
        For Each subWalker In mySpace.WalkerList
            subWalker.Acceleration = New Vector2((MouseX - subWalker.Location.X) / SPEED * rnd.NextDouble * subWalker.Mass, (MouseY - subWalker.Location.Y) / SPEED * rnd.NextDouble * subWalker.Mass)
            subWalker.Move()
        Next
        For Each SubBlocker In mySpace.BlockerList
            If (SubBlocker.Mass > 80 AndAlso SubBlocker.IsAdd = 1) OrElse (SubBlocker.Mass < 5 AndAlso SubBlocker.IsAdd = -1) Then SubBlocker.IsAdd = -SubBlocker.IsAdd
            SubBlocker.Mass += SubBlocker.IsAdd * rnd.Next(1, 20) / 50
        Next
        mySpace.DrawSpace(ds)
    End Sub
    Sub OnMouseMove(pos As Point)
        MouseX = pos.X
        MouseY = pos.Y
    End Sub
End Class
